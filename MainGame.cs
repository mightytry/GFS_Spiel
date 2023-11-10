using GFS_Spiel.MainGameScripts;
using GFS_Spiel.MainGameScripts.EnemyPlayers;
using StunTools;

namespace GFS_Spiel
{
    public partial class MainGame : Form
    {
        // Hier wird viel Invoke verwendet, da die Methoden von anderen Threads aufgerufen werden können dadurch das hier teilweise auf netzwerkdaten gewartet wird
        // Durch Invoke wird sichergestellt, dass die Methode auf dem UI Thread ausgeführt wird, denn nur der UI Thread kann das UI verändern

        /// <summary>
        /// Die Logik des Spiels
        /// </summary>
        private FieldHandler fieldHandler;

#pragma warning disable CS8618 // Weil nicht alle Felder direkt in dem Konstruktor gesetzt werden, sondern auch in der Methode genShips
        public MainGame(TcpClient? connection, ShipGridEntry[] shipPlacementConfig)
#pragma warning restore CS8618 
        {
            InitializeComponent();
            Start(shipPlacementConfig, connection);

            // Ändert das Design erstmal damit der ChangeDesignBT
            // das gegenteilige Design hat um ihn hervorzuheben
            Theme.ChangeMode();
            ChangeDesignBT_Click(null, null);
            //---------------------------------------------------
        }
        /// <summary>
        /// Erstellt die spielwichtigen Objekte
        /// </summary>
        /// <param name="shipPlacementConfig"></param>
        /// <param name="connection"></param>
        public void Start(ShipGridEntry[] shipPlacementConfig, TcpClient? connection)
        {
            // Erstelle die Felder
            Field playerField = new Field(PlayerFieldGrid);
            Field enemyField = new Field(EnemyFieldGrid);

            // Erstelle die Schiffgrids
            ShipGrid playerShipGrid = new ShipGrid(PlayerShipsInfoGrid);
            ShipGrid enemyShipGrid = new ShipGrid(EnemyShipsInfoGrid);

            playerShipGrid.SetEntries(shipPlacementConfig);
            enemyShipGrid.SetEntries(shipPlacementConfig);

            // Erstelle den FieldHandler
            fieldHandler = new FieldHandler(playerField, enemyField, playerShipGrid, enemyShipGrid, this, connection);
        }

        /// <summary>
        /// Zeigt an, dass der Gegner bereit ist
        /// </summary>
        public void EnemyReady()
        {
            Invoke(() => EnemyInfoLB.Text = "Bereit");
        }

        /// <summary>
        /// Zeigt an, ob man gewonnen oder verloren hat
        /// </summary>
        /// <param name="won">Wenn true dann gewonnen</param>
        public void EndGame(bool won)
        {
            BeginInvoke(() => MessageBox.Show($"Du hast dieses Match {(won ? "GEWONNEN" : "VERLOREN")}!\nKehre nun zum Startbildschirm zurück!", "END"));
        }

        /// <summary>
        /// Setzt den Text des TurnArrows und der Info Labels
        /// </summary>
        /// <param name="turn">Wenn true dann ist der Spieler am Zug</param>
        public void SetTurn(bool turn)
        {
            Action a;
            if (turn)
            {
                a = () =>
                {
                    TurnArrowTB.Text = "<-";
                    PlayerInfoLB.Text = "Am Zug";
                    EnemyInfoLB.Text = "Wartet auf Dich";
                };
            }
            else
            {
                a = () =>
                {
                    TurnArrowTB.Text = "->";
                    PlayerInfoLB.Text = "Wartet auf Gegner";
                    EnemyInfoLB.Text = "Am Zug";
                };
            }

            if (InvokeRequired)
                Invoke(a);
            else
                a();
        }

        /// <summary>
        /// Wenn das Form geladen hat, werden alle Auswahlen zurückgesetzt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            fieldHandler.Loaded();
        }
        /// <summary>
        /// Das Playerfeld wird angezeigt oder versteckt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hideBoard_CheckedChanged(object sender, EventArgs e)
        {
            fieldHandler.HidePlayerField(((CheckBox)sender).Checked);
        }

        /// <summary>
        /// Überprüft ob alles passt und setzt dann den Spieler auf Bereit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadyBT_Click(object sender, EventArgs e)
        {
            // Wenn der Spieler bereit ist, wird geprüft ob alle Schiffe platziert sind
            if (!fieldHandler.PlayerShipAmountEmpty())
            {
                MessageBox.Show("Es sind noch nicht alle oder zu viele Schiffe platziert!");
                return;
            }

            fieldHandler.Ready();

            // Der Button und das Controlcenter werden versteckt
            ReadyBT.Visible = false;
            FieldControllGB.Visible = false;


            PlayerInfoLB.Text = "Bereit";
        }

        /// <summary>
        /// Setzt die übrigen Schiffe zufällig
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompleteBT_Click(object sender, EventArgs e)
        {
            fieldHandler.RandomShips();
        }

        /// <summary>
        /// Löscht das ganze Spielfeld
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBT_Click(object sender, EventArgs e)
        {
            fieldHandler.ClearPlayer();
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
            fieldHandler.ChangeTheme();
            Theme.ChangeMode();
            Theme.RefreshTheme(this);
            ChangeDesignBT.Text = Theme.IsDarkMode() ? "Light Mode" : "Dark Mode";
            // Setzt die Farben des Buttons auf die Farben des alten Designs um das andere Design zu zeigen
            ChangeDesignBT.BackColor = btBackColor;
            ChangeDesignBT.ForeColor = btForeColor;
        }
    }
}