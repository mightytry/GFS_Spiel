using StunTools;
using StunTools.Tools;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GFS_Spiel
{
    public partial class LobbyScreen : Form
    {
        // Icons:
        // <a href="https://www.flaticon.com/free-icons/user" title="user icons">User icons created by Freepik - Flaticon</a>
        // <a href="https://www.flaticon.com/free-icons/ai" title="ai icons">Ai icons created by Icongeek26 - Flaticon</a>


        private ShipGrid shipGrid;
        private bool isHost;

        /// <summary>
        /// Der Socket der für die Verbindung genutzt wird
        /// </summary>
        private TcpSocket Socket { get; set; }
        /// <summary>
        /// Der Client Sobald eine Verbindung über den Socket hergestellt wurde
        /// </summary>
        private TcpClient? Client { get; set; }

        /// <summary>
        /// CancellationTokenSource um den Verbindungsaufbau abzubrechen falls der Benutzer sich mit einem anderen Code verbinden möchte
        /// </summary>
        private CancellationTokenSource ConnectCancel = new();

        public LobbyScreen(bool _isHost)
        {
            InitializeComponent();

            Socket = new();

            isHost = _isHost;
            // Erstellt ein neues ShipGrid wo nur der Host Anzahl der Schiffe ändern kann
            shipGrid = new ShipGrid(ShipsInfoGrid, isHost);

            // Ändert das Design erstmal damit der ChangeDesignBT
            // das gegenteilige Design hat um ihn hervorzuheben
            Theme.ChangeMode();
            ChangeDesignBT_Click(null, null);
            //---------------------------------------------------

            if (!isHost)
            {
                OponentIMG.Image = Properties.Resources.user;
                OponentNameLB.Text = "Du";
                PlayerNameLB.Text = "Gegner";
                StartBT.Visible = false;
            }
        }

        /// <summary>
        /// Sobald das Form geladen hat wird der Code vom Internet abgerufen und angezeigt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LobbyScreen_Load(object sender, EventArgs e)
        {
            await Socket.UpdateCode();
            OnCodeChange();
        }

        /// <summary>
        /// Versucht eine Verbindung mit dem Client mit dem Code aus dem CodeInputTB herzustellen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ConnectBT_Click(object sender, EventArgs e)
        {
            // Wenn bereits einmal versucht wurde eine Verbindung herzustellen, wird diese abgebrochen
            // Falls bereits eine Verbindung besteht, wird abgebrochen
            if (Client is not null)
            {
                if (Client.Connected) return;
                ConnectCancel.Cancel();
                Client.Close();
            }
            string code = CodeInputTB.Text;

            Invoke(() => StatusLB.Text = "Wird Verbunden");
            ConnectCancel = new();

            // Versucht eine Verbindung mit dem Code herzustellen bis es dur einen neuen Versuch abgebrochen wird oder eine Verbindung hergestellt wurde
            try { Client = await Socket.Connenct(Compressor.UnZip(code)).WaitAsync(ConnectCancel.Token); }
            catch (TaskCanceledException) { Invoke(() => StatusLB.Text = "Verbindung fehlgeschlagen"); return; }

            // Der host muss das gegner Bild und den Namen ändern -> da davor KI
            if (isHost)
            {
                OponentIMG.Image = Properties.Resources.user;
                OponentNameLB.Text = "Gegner";
            }

            Invoke(() => StatusLB.Text = "Verbunden");

            
            if (isHost)
            {
                // Der Host sendet seine Schifftypen (ShipGridEntry) an den Client sobald sich die Anzahl der Schiffe ändert
                await Client!.SendData(shipGrid.Ships.Values.ToArray());
                shipGrid.AmountChangedEvent += async (a, b) => await Client!.SendData(b);
            }
            else
            {
                // Der Client wartet auf Nachrichten vom Host
                _ = Client?.Receive().ContinueWith((message) => callback(message.Result));
            }

        }
        /// <summary>
        /// Wenn eine eue Nachricht vom Host kommt, wird diese Funktion aufgerufen
        /// </summary>
        /// <param name="message"></param>
        private void callback(StunTools.Message message)
        {
            // Wenn die Nachricht ein String ist und der String "START" ist, wird das Spiel gestartet
            if (typeof(string) == message.ObjectType)
            {
                if (message.GetData<string>() == "START")
                {
                    startGame();
                    return;
                }
            }
            else
            {
                // Sonst ist die Nachricht ein IEnumerable<ShipGridEntry>, also werden die Schifftypen aktualisiert
                shipGrid.SetEntries(message.GetData<IEnumerable<ShipGridEntry>>()!);
            }
            // starte eine neue Task die auf die nächste Nachricht wartet
            _ = Client?.Receive().ContinueWith((message) => callback(message.Result));
        }

        /// <summary>
        /// Sobald der Code sich ändert wird diese Funktion aufgerufen um den Code anzuzeigen
        /// </summary>
        internal void OnCodeChange()
        {
            Invoke(() => CodeLB.Text = Socket.Code ?? Compressor.Zip(Socket.LocalEndPoint!));
            Invoke(() => PortLB.Text = Socket.LocalEndPoint?.Port.ToString());
        }

        /// <summary>
        /// Kopiert den Code in die Zwischenablage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyCodeBT_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CodeLB.Text);
        }

        /// <summary>
        /// Startet das eigentliche Spiel
        /// </summary>
        private void startGame()
        {
            BeginInvoke(() =>
            {
                // Wenn das Spiel gestartet wird, wird das LobbyScreen versteckt und das MainGame geöffnet
                Hide();
                new MainGame(Client!, shipGrid.Ships.Values.ToArray()).ShowDialog();
                // Nachdem das Maingame geschlossen wurde, wird das LobbyScreen geschlossen
                Close();
            });
        }
        /// <summary>
        /// Startet das Spiel wenn der StartBT geklickt wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartBT_Click(object sender, EventArgs e)
        {
            if (Client is not null && Client.Connected)
                await Client.SendData("START");
            startGame();
        }

        /// <summary>
        /// Ändert das Design des aktuellen Forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeDesignBT_Click(object? sender, EventArgs? e)
        {
            // Speichert die Farben des Buttons im aktuellen Design
            Color btBackColor = Theme.GetThemeColor(ChangeDesignBT.BackColor);
            Color btForeColor = Theme.GetThemeColor(ChangeDesignBT.ForeColor);
            // Wechselt das Design
            Theme.ChangeMode();
            Theme.RefreshTheme(this);
            ChangeDesignBT.Text = Theme.IsDarkMode() ? "Light Mode" : "Dark Mode";
            // Setzt die Farben des Buttons auf die Farben des alten Designs um das andere Design zu zeigen
            ChangeDesignBT.BackColor = btBackColor;
            ChangeDesignBT.ForeColor = btForeColor;
        }
    }
}
