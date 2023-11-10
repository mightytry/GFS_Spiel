{	//[Program.cs]
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            ApplicationConfiguration.Initialize();
            Application.Run(new SelectScreen());
        }
    }
	/// <summary>
    /// Klasse die, die Farben für die jeweiligen Elemente des Programms enthält.
    /// </summary>
    public static class Theme 
    {
        /// <summary>
        /// Die Farben für den DarkMode
        /// Anfang ist von: https://www.color-hex.com/color-palette/98179
        /// </summary>
        static Color[] DarkModeColors =
        {
            //------------Farbe-----------    ----------Beschreibung-----------   -----Systemfarbe-----
            Color.FromArgb(255, 255, 255),  // Text                             Control
            Color.FromArgb(62, 62, 66),     // Hintergund 4                     ControlDark
            Color.FromArgb(45, 45, 48),     // Hintergund 3                     ControlDarkDark
            Color.FromArgb(37, 37, 38),     // Hintergund 2                     ControlLight
            Color.FromArgb(30, 30, 30),     // Hintergund                       ControlLightLight
            Color.FromArgb(0, 122, 204),    // Kontur                           ControlText     
            Color.FromArgb(22, 22, 50),     // Zu viele Schiffe                 Desktop
            Color.FromArgb(21, 55, 18),     // Richtige Anzahl Schiffe          GrayText
            Color.FromArgb(128, 30, 0),     // Zu wenig Schiffe                 Highlight
            Color.FromArgb(128, 128, 128),  // Schiff                           HighlightText
            Color.FromArgb(128, 0, 0),      // Versenkt                         HotTrack
        };
        /// <summary>
        /// Die Farben für den DarkMode
        /// Anfang ist von: https://www.color-hex.com/color-palette/106748
        /// </summary>
        static Color[] LightModeColors =
        {
            //------------Farbe-----------    ----------Beschreibung-----------   -----Systemfarbe-----
            Color.FromArgb(0, 0, 0),        // Text                             Control
            Color.FromArgb(147,148,165),    // Hintergund 4                     ControlDark
            Color.FromArgb(210,211,219),    // Hintergund 3                     ControlDarkDark
            Color.FromArgb(228,229,241),    // Hintergund 2                     ControlLight
            Color.FromArgb(250,250,250),    // Hintergund                       ControlLightLight
            Color.FromArgb(72,75,106),      // Kontur                           Desktop
            Color.FromArgb(173, 216, 230),  // Zu viele Schiffe                 GrayText
            Color.FromArgb(144, 238, 144),  // Richtige Anzahl Schiffe          Highlight
            Color.FromArgb(255, 182, 193),  // Zu wenig Schiffe                 HighlightText
            Color.FromArgb(128, 128, 128),  // Schiff                           HotTrack
            Color.FromArgb(255, 127, 80),   // Versenkt                         InactiveCaption
        };                                    
        static bool DarkMode = true;

        /// <summary>
        /// Wechselt zwischen DarkMode und LightMode
        /// </summary>
        public static void ChangeMode()
        {
            DarkMode = !DarkMode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true falls das Theme gerade Dunkel ist.</returns>
        public static bool IsDarkMode()
        {
            return DarkMode;
        }

        /// <summary>
        /// Gibt die jeweilige Theme Farbe für die aktuelle Farbe zurück
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Color GetThemeColor(Color type)
        {
            // Es wird überprüft ob die Farbe eine Systemfarbe ist (also im Fromdesigner gesetzte) Farbe ist
            if (type.IsNamedColor)
            {
                // Wenn sie eine Systemfarbe ist, dann wird durch den Wert der Farbe in dem KnownColor Enum die Farbe zurückgegeben
                if (((int)type.ToKnownColor()- 5) >= LightModeColors.Length)
                    return type;
                return DarkMode ? DarkModeColors[(int)type.ToKnownColor() - 5] : LightModeColors[(int)type.ToKnownColor() - 5];
            }
            else
            {
                // Wenn nicht, dann wird der Index der Farbe in dem alten Colorarray gesucht und die jeweilige Farbe, des Indexes im neuen zurückgegeben
                int index = DarkMode ? Array.IndexOf(LightModeColors, type) : Array.IndexOf(DarkModeColors, type);
                if (index >= LightModeColors.Length || index < 0)
                    return type;
                return DarkMode ? DarkModeColors[index] : LightModeColors[index];
            }
        }
        /// <summary>
        /// Aktualisiert das Theme für das aktuelle Form/Control
        /// </summary>
        /// <param name="control"></param>
        public static void RefreshTheme(Control control)
        {
            // Für jedes Control wird die Funktion erneut aufgerufen
            foreach (Control c in control.Controls)
            {
                RefreshTheme(c);
            }
            // Wenn das Control ein DataGridView ist, dann werden die Farben der einzelnen Elemente gesetzt
            if (control.GetType() == typeof(DataGridView))
            {
                DataGridView dg = (DataGridView)control;
                dg.DefaultCellStyle.BackColor = GetThemeColor(dg.DefaultCellStyle.BackColor);
                dg.DefaultCellStyle.ForeColor = GetThemeColor(dg.DefaultCellStyle.ForeColor);
                // Für jede Zelle wird die Hintergrundfarbe und die Vordergrundfarbe gesetzt
                foreach (DataGridViewRow i in dg.Rows)
                {
                    foreach (DataGridViewCell c in i.Cells)
                    {
                        c.Style.ForeColor = GetThemeColor(c.Style.ForeColor);
                        c.Style.BackColor = GetThemeColor(c.Style.BackColor);
                    }
                }

                dg.ColumnHeadersDefaultCellStyle.BackColor = GetThemeColor(dg.ColumnHeadersDefaultCellStyle.BackColor);
                dg.ColumnHeadersDefaultCellStyle.ForeColor = GetThemeColor(dg.ColumnHeadersDefaultCellStyle.ForeColor);
                dg.GridColor = GetThemeColor(dg.GridColor);
                dg.BackgroundColor = GetThemeColor(dg.BackgroundColor);

                // Für jede Spalte wird die Hintergrundfarbe und die Vordergrundfarbe vom Header gesetzt
                foreach (DataGridViewColumn i in dg.Columns)
                {
                    i.HeaderCell.Style.BackColor = GetThemeColor(i.HeaderCell.Style.BackColor);
                    i.HeaderCell.Style.ForeColor = GetThemeColor(i.HeaderCell.Style.ForeColor);
                }
            }
            // Wenn das Control ein Button ist, dann wird die Randfarbe des Buttons gesetzt
            else if (control.GetType() == typeof(Button))
            {
                ((Button)control).FlatAppearance.BorderColor = GetThemeColor(((Button)control).FlatAppearance.BorderColor);
            }
            // Von jedem Control wird die Hintergrundfarbe und die Vordergrundfarbe gesetzt
            control.BackColor = GetThemeColor(control.BackColor);
            control.ForeColor = GetThemeColor(control.ForeColor);
        }
    }
}
{   //[SelectScreen.cs]
	public partial class SelectScreen : Form
    {
        public SelectScreen()
        {
            InitializeComponent();

            // Ändert das Design erstmal damit der ChangeDesignBT
            // das gegenteilige Design hat um ihn hervorzuheben
            Theme.ChangeMode();
            ChangeDesignBT_Click(null, null);
            //---------------------------------------------------
        }

        /// <summary>
        /// Wenn der JoinBT geklickt wird, dann wird ein <see cref="LobbyScreen"/> als <c>nicht Host</c> erstellt und angezeigt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JoinBT_Click(object sender, EventArgs e)
        {
            LobbyScreen ls = new LobbyScreen(false);
            Hide();
            ls.ShowDialog();
            Show();
        }
        /// <summary>
        /// Wenn der JoinBT geklickt wird, dann wird ein <see cref="LobbyScreen"/> als <c>Host</c> erstellt und angezeigt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostBT_Click(object sender, EventArgs e)
        {
            LobbyScreen ls = new LobbyScreen(true);
            Hide();
            ls.ShowDialog();
            Show();
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
{   //[LobbyScreen.cs]
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
            Invoke(() => CodeLB.Text = Socket.Code);
            Invoke(() => PortLB.Text = Socket.LocalEndPoint?.Port.ToString());
        }

        /// <summary>
        /// Kopiert den Code in die Zwischenablage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyCodeBT_Click(object sender, EventArgs e)
        {
            if (Socket.Code != null)
                Clipboard.SetText(Socket.Code);
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
{   //[ShipGrid.cs]
	public class ShipGrid
    {
        // ---------Für Multiplayer---------
        public delegate void AmountChangedEventHandler(object? sender, IEnumerable<ShipGridEntry> entries);
        public event AmountChangedEventHandler? AmountChangedEvent;
        // ---------Für Multiplayer---------

        /// <summary>
        /// Ob die Anzahl der fehlenden Schiffe bearbeitet werden kann
        /// </summary>
        public readonly bool Editable;

        /// <summary>
        /// Ships als <see cref="ShipGridEntry"/> mit der länge des Schifftyps als key
        /// </summary>
        public readonly Dictionary<int, ShipGridEntry> Ships;

        /// <summary>
        /// Das <see cref="DataGridView"/> was dir Table in der UI darstellt
        /// </summary>
        private readonly DataGridView shipsGrid;


#pragma warning disable CS8618 // Weil nicht alle Felder direkt in dem Konstruktor gesetzt werden, sondern auch in der Methode genShips
        public ShipGrid(DataGridView _shipsGrid, bool _editable = false)
#pragma warning restore CS8618
        {
            shipsGrid = _shipsGrid;
            Editable = _editable;

            Ships = new Dictionary<int, ShipGridEntry>();

            shipsGrid.CellMouseEnter += mouseEnter;
            shipsGrid.MouseLeave += mouseLeave;

            if (Editable)
                shipsGrid.CellEndEdit += editEnd;

            genShips();
        }

        /// <summary>
        /// Gibt zurück ob die Anzahl der fehlenden Schiffe 0 ist
        /// </summary>
        /// <returns><see cref="true"/> wenn nichts fehlend</returns>
        public bool ShipAmountEmpty()
        {
            foreach (ShipGridEntry s in Ships.Values)
            {
                if (s.Amount != 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Setzt die Anzahl der fehlenden Schiffe des jeweiligen Schifftypes
        /// </summary>
        /// <param name="shipGridEntries"></param>
        public void SetEntries(IEnumerable<ShipGridEntry> shipGridEntries)
        {
            foreach (ShipGridEntry ship in shipGridEntries)
            {
                Ships[ship.Length].Update(ship);
            }
        }

        /// <summary>
        /// Setzt die Anzahl der fehlenden Schiffe des jeweiligen Schifftypes durch die Länge der Links
        /// </summary>
        /// <param name="links"></param>
        public void UpdateAmount(Link[] links)
        {
            ResetAmount();
            foreach (Link link in links)
            {
                // Falls es ein Schifftyp gibt, das die Länge des Links hat, dann wird die Anzahl der fehlenden Schiffe des Typs um 1 verringert
                if (Ships.ContainsKey(link.Length))
                {
                    Ships[link.Length].Amount--;
                }
            }
        }
        /// <summary>
        /// Setzt den Wert der fehlenden Schiffe auf den amount Wert
        /// </summary>
        /// <param name="amount">Wenn null dann auf den Maximalwert</param>
        public void ResetAmount(int? amount = null)
        {
            foreach (ShipGridEntry ship in Ships.Values)
            {
                ship.ResetAmount(amount);
            }
        }

        /// <summary>
        /// Hover effect für die Zellen
        /// Sobald die Maus über eine Zelle ist wird es als ausgewählt markiert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseEnter(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
                shipsGrid[e.ColumnIndex, e.RowIndex].Selected = true;
        }

        /// <summary>
        /// Ändert die Anzahl der fehlenden Schiffe des jeweiligen Schifftypes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editEnd(object? sender, DataGridViewCellEventArgs e)
        {
            // Wenn die Eingabe eine Zahl ist und zwischen 0 und 10 liegt, dann wird die Anzahl der fehlenden Schiffe des jeweiligen Schifftypes geändert
            if (int.TryParse((string)shipsGrid[e.ColumnIndex, e.RowIndex].Value, out int res) && res <= 10 && res >= 0)
            {
                Ships.Values.ElementAt(e.RowIndex).MaxAmount = res;
                AmountChangedEvent?.Invoke(sender, Ships.Values);
                return;
            }
            // Sonst wird die Anzahl in der UI auf den vorherigen Wert gesetzt
            Ships.Values.ElementAt(e.RowIndex).ResetAmount();
        }

        /// <summary>
        /// Hover effect für die Zellen
        /// Löscht die Auswahl sobald die Maus die Zelle verlässt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseLeave(object? sender, EventArgs e)
        {
            ResetSelection();
        }

        /// <summary>
        /// Löscht die Auswahl
        /// </summary>
        public void ResetSelection()
        {
            shipsGrid.ClearSelection();
        }

        /// <summary>
        /// Gibt die Schiffe als <see cref="int"/> zurück indem die Anzahl der fehlenden Schiffe durch jeweiligen int der Länge des Schiffs dargestellt wird
        /// Also ein Schiff mit der Länge 3 und 2 Schiffen wird als {3, 3} dargestellt
        /// </summary>
        /// <param name="getMissingOnly">Ob nur die Anzahl die noch nicht plaziert wurde zurückgegeben werden soll</param>
        /// <returns></returns>
        public IEnumerable<int> GetShipsAsInt(bool getMissingOnly = false)
        {
            foreach (ShipGridEntry ship in Ships.Values)
            {
                // Entweder wird die Anzahl der fehlenden Schiffe oder die Anzahl der maximalen Schiffe zurückgegeben
                int amount = getMissingOnly ? ship.Amount : ship.MaxAmount;
                for (int i = 0; i < amount; i++)
                {
                    yield return ship.Length;
                }
            }
        }

        /// <summary>
        /// Erstellt eine neue Zeile in der Tabelle und fügt den Schifftype in das <see cref="Ships"/> dict hinzu
        /// </summary>
        /// <param name="name"></param>
        /// <param name="length"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private ShipGridEntry createShiptypeCell(string name, int length, int amount)
        {
            DataGridViewRow row = shipsGrid.Rows[shipsGrid.Rows.Add(name, length, amount)];
            ShipGridEntry ship = new ShipGridEntry(name, length, amount, row);
            Ships.Add(length, ship);
            return ship;
        }

        /// <summary>
        /// Generiert/Formatiert die Tabelle und erstellt alle <see cref="ShipGridEntry"/>s
        /// </summary>
        private void genShips()
        {
            // Erstelle die Spalten
            shipsGrid.Columns.Add("Column0", "Schiff");
            shipsGrid.Columns.Add("Column1", "Länge");
            shipsGrid.Columns.Add("Column2", "Fehlend");

            // Macht das man nicht sortieren kann
            shipsGrid.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            shipsGrid.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            shipsGrid.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            // Erlaubt nur die Bearbeitung der Anzahl der fehlenden Schiffe wenn Editable true ist
            shipsGrid.Columns[0].ReadOnly = true;
            shipsGrid.Columns[1].ReadOnly = true;
            shipsGrid.Columns[2].ReadOnly = !Editable;

            #region Ships
            createShiptypeCell("Atomschiff", 6, 0);
            createShiptypeCell("Schlachtschiff", 5, 1);
            createShiptypeCell("Kreuzer", 4, 2);
            createShiptypeCell("Zerstörer", 3, 3);
            createShiptypeCell("U-Boot", 2, 4);
            createShiptypeCell("Jetski", 1, 0);
            #endregion

            // Setze die Höhe der Zeilen auf die Höhe der Tabelle durch die Anzahl der Einträge
            int headerHeight = shipsGrid.ColumnHeadersHeight;
            for(int i = 0; i < Ships.Count; i++)
            {
                shipsGrid.Rows[i].Height = (shipsGrid.Size.Height - headerHeight) / Ships.Count;
            }
        }
    }
    /// <summary>
    /// Die Datenebene für eine Reihe in <see cref="DataGridView"/>
    /// </summary>
    public class ShipGridEntry
    {
        public string Name { get => name; set { name = value; updateVariable(name, 0); } }
        public int Length { get => length; set { length = value; updateVariable(length, 1); } }

        /// <summary>
        /// Anzahl der fehlenden Schiffe
        /// </summary>
        public int Amount { get => amount; set { amount = value; updateVariable(amount, 2); updateColor(amount, 2); } }

        /// <summary>
        /// Anzahl der exestierenden Schiffe
        /// </summary>
        public int MaxAmount { get => maxAmount; set { maxAmount = value; Amount = value; } }

        private string name;
        private int length;
        private int amount;
        private int maxAmount;

        /// <summary>
        /// Die Zeile in der Tabelle die die Daten in der UI darstellt
        /// Könnte null sein da es wenn das <see cref="ShipGridEntry"/> über JsonConvert.DeserializeObject (Netzwerk) erstellt wird, die Variable nicht gesetzt wird
        /// </summary>
        [JsonIgnore]
        private readonly DataGridViewRow? entryRow;

        public ShipGridEntry(string _name, int _length, int maxAmount, DataGridViewRow _entryRow)
        {
            entryRow = _entryRow;
            name = _name;
            length = _length;
            MaxAmount = maxAmount;
        }

        /// <summary>
        /// Aktualisiert die Daten auf die selben werte eines anderen <see cref="ShipGridEntry"/>
        /// </summary>
        /// <param name="shipGridEntry"></param>
        public void Update(ShipGridEntry shipGridEntry)
        {
            Name = shipGridEntry.Name;
            Length = shipGridEntry.Length;
            Amount = shipGridEntry.Amount;
            MaxAmount = shipGridEntry.MaxAmount;
        }

        /// <summary>
        /// Setzt den Amount auf den value Wert
        /// </summary>
        /// <param name="amount">Wenn null dann auf den Maximalwert</param>
        public void ResetAmount(int? value = null)
        {
            if (value == null) Amount = MaxAmount;
            else Amount = value.Value;
        }

        /// <summary>
        /// Aktualisiert die Variable in der UI
        /// </summary>
        /// <param name="value">Neuer wert</param>
        /// <param name="cell">Reihe</param>
        private void updateVariable(object value, int cell)
        {
            // Soll nicht aktualisiert werden wenn die Variable null ist (Es von einem Netzwerk kommt)
            if (entryRow is not null)
                entryRow.Cells[cell].Value = value.ToString();
        }

        /// <summary>
        /// Aktualisiert die Farbe in der UI basierend auf dem Wert
        /// </summary>
        /// <param name="value">amount</param>
        /// <param name="cell">Reihe</param>
        private void updateColor(int value, int cell)
        {
            // Soll nicht aktualisiert werden wenn die Variable null ist (Es von einem Netzwerk kommt)
            if (entryRow is null) return;
            // Holt die korrekte Farbe von Theme basierend auf dem Wert von value
            switch (value)
            {
                case 0:
                    entryRow.Cells[cell].Style.BackColor = Theme.GetThemeColor(SystemColors.GrayText);
                    break;
                case < 0:
                    entryRow.Cells[cell].Style.BackColor = Theme.GetThemeColor(SystemColors.Highlight);
                    break;
                case > 0:
                    entryRow.Cells[cell].Style.BackColor = Theme.GetThemeColor(SystemColors.Desktop);
                    break;
            }

        }
    }
}
{	//[MainGame.cs]
	public partial class MainGame : Form
    {
        // Hier wird viel Invoke verwendet, da die Methoden von anderen Threads aufgerufen werden können dadurch das hier teilweise auf netzwerkdaten gewartet wird
        // Durch Invoke wird sichergestellt, dass die Methode auf dem UI Thread ausgeführt wird, denn nur der UI Thread kann das UI verändern

        /// <summary>
        /// Die Logik des Spiels
        /// </summary>
        public FieldHandler FieldHandler;

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
            FieldHandler = new FieldHandler(playerField, enemyField, playerShipGrid, enemyShipGrid, this, connection);
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
            FieldHandler.Loaded();
        }
        /// <summary>
        /// Das Playerfeld wird angezeigt oder versteckt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hideBoard_CheckedChanged(object sender, EventArgs e)
        {
            FieldHandler.HidePlayerField(((CheckBox)sender).Checked);
        }

        /// <summary>
        /// Überprüft ob alles passt und setzt dann den Spieler auf Bereit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadyBT_Click(object sender, EventArgs e)
        {
            // Wenn der Spieler bereit ist, wird geprüft ob alle Schiffe platziert sind
            if (!FieldHandler.PlayerShipAmountEmpty())
            {
                MessageBox.Show("Es sind noch nicht alle oder zu viele Schiffe platziert!");
                return;
            }

            FieldHandler.Ready();

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
            FieldHandler.RandomShips();
        }

        /// <summary>
        /// Löscht das ganze Spielfeld
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBT_Click(object sender, EventArgs e)
        {
            FieldHandler.ClearPlayer();
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
{   //[MainGameScripts/FieldHandler.cs]
	public class FieldHandler
    {
        // Die Felder
        private Field playerField;
        private Field enemyField;

        // Die Schiffgrids
        private ShipGrid playerShipGrid;
        private ShipGrid enemyShipGrid;

        private MainGame mainGame;

        private EnemyPlayer enemyPlayer;

        public bool GameEnded { get; private set; } = false;
        public bool IsReady { get; private set; } = false;

        private bool mouseDown = false;
        private bool isPlayerTurn;


        public Size FieldSize { get => playerField.FieldSize; }

        /// <summary>
        /// Für die Zufallspalzierung
        /// </summary>
        public IEnumerable<int> FieldConfig { get => playerShipGrid.GetShipsAsInt(); }

        /// <summary>
        /// Ändert alle nötigen Werte für den Spieler, auch in der UI
        /// </summary>
        public bool IsPlayerTurn { get => isPlayerTurn; set { if (GameEnded) return; mainGame.SetTurn(value); enemyField.DoAllowUserinput(value); isPlayerTurn = value;  }  }
        

        public FieldHandler(Field _playerField, Field _enemyField, ShipGrid _playerShipGrid, ShipGrid _enemyShipGrid, MainGame _mainGame, TcpClient? connection)
        {
            playerField = _playerField;
            enemyField = _enemyField;

            playerShipGrid = _playerShipGrid;
            enemyShipGrid = _enemyShipGrid;

            mainGame = _mainGame;

            // Wenn eine Verbindung besteht, dann ist der Gegner ein Netzwerkspieler, ansonsten ein Computerspieler
            if (connection != null && connection.Connected)
                enemyPlayer = new NetworkPlayer(this, connection);
            else
                enemyPlayer = new ComputerPlayer(this);
        }

        /// <summary>
        /// Setzt den Spieler auf Ready
        /// Erlaubt dem Spieler nicht mehr seine Schiffe zu bewegen
        /// Gibt dem Gegner bescheid, dass der Spieler bereit ist
        /// </summary>
        public void Ready()
        {
            playerField.DoAllowUserinput(false);
            IsReady = true;
            enemyPlayer.Ready();
            playerShipGrid.ResetAmount();
            enemyShipGrid.ResetAmount();
        }

        /// <summary>
        /// Überprüft ob alle Schiffe gesetzt wurden
        /// </summary>
        /// <returns>true wenn alle auf dem Feld sind/returns>
        public bool PlayerShipAmountEmpty()
        {
            return playerShipGrid.ShipAmountEmpty();
        }

        /// <summary>
        /// Lösche alle Auswahlmarkierungen
        /// </summary>
        public void ClearFields()
        {
            playerField.ResetSelection();
            enemyField.ResetSelection();

            playerShipGrid.ResetSelection();
            enemyShipGrid.ResetSelection();
        }

        /// <summary>
        /// Setzt alle nötigen Eventlisteners
        /// </summary>
        private void start()
        {
            // Wenn der Spieler auf ein Feld klickt oder mit der Maus drüberfährt
            playerField.FieldGrid.CellMouseDown += (sender, e) => { playerCellHoverPress(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex)); };
            playerField.FieldGrid.CellMouseEnter += playerCellHoverPress;

            // Wenn die Maus gedrückt oder losgelassen wird
            playerField.FieldGrid.MouseDown += (sender, e) => { mouseDown = true; };
            playerField.FieldGrid.MouseUp += (sender, e) => { mouseDown = false; };

            // Wenn der Spieler auf ein Gegnderfeld klickt
            enemyField.FieldGrid.CellMouseClick += enemyCellPress;
            enemyField.HoverEvent += enemyPlayer.OnEnemyFieldHover;
            enemyField.UnHoverEvent += enemyPlayer.OnEnemyFieldUnHover;

            // Damit nicht auf dem Gegnerfeld geklickt werden kann
            enemyField.DoAllowUserinput(false);
        }

        /// <summary>
        /// Wenn das Fenster geladen hat
        /// </summary>
        public void Loaded()
        {
            start();

            // Es werden alle Auswahlen zurückgesetzt
            ClearFields();

            // Für Computer Gegner damit er direkt Ready gehen kann.
            enemyPlayer.OnLoad();
        }
        /// <summary>
        /// Setzt ein Feld auf dem Spielerfeld
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerCellHoverPress(object? sender, DataGridViewCellEventArgs e)
        {
            // Wenn der Spieler auf ein Feld klickt
            // Wenn die Maus gedrückt, das Feld nicht die Zahlen und Buchstaben ist und das Feld geändert wurde
            if ( mouseDown && e.ColumnIndex > 0 && e.RowIndex > 0 && playerField.Update(e.ColumnIndex - 1, e.RowIndex - 1))
            {
                // Aktualisiere die Anzahl der Schiffe in ShipGrid
                playerShipGrid.UpdateAmount(playerField.PlayingField.Links.ToArray());
                playerField.ResetSelection();
            }
        }
        /// <summary>
        /// Setzt ein Feld auf dem Gegnerfeld
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enemyCellPress(object? sender, DataGridViewCellMouseEventArgs e)
        {
            // Wenn der Spieler auf ein Gegnerfeld klickt
            // Wenn der Spieler am Zug ist, das Feld nicht die Zahlen und Buchstaben ist und das Feld getroffen wurde
            if (IsPlayerTurn && e.ColumnIndex > 0 && e.RowIndex > 0 && enemyField.Hit(e.ColumnIndex - 1, e.RowIndex - 1).HasValue)
            {
                // OnChange ist eine Funktion die für die Gegner antworten zuständig ist
                IsPlayerTurn = false;
                // Sende dem Gegner eine Nachricht das ein Feld getroffen wurde
                _ = enemyPlayer.OnEnemyFieldHit(enemyField.PlayingField[e.ColumnIndex - 1, e.RowIndex - 1]).ContinueWith(x =>
                {
                    // Der Gegner antwortet mit einem HitType, was es für ein Treffer war
                    switch (x.Result)
                    {
                        case HitType.HIT:
                            enemyField.Update(e.ColumnIndex - 1, e.RowIndex - 1);
                            break;
                        case HitType.SUNKEN:
                            enemyField.Update(e.ColumnIndex - 1, e.RowIndex - 1);
                            enemyField.ShipSunken(enemyField.PlayingField[e.ColumnIndex - 1, e.RowIndex - 1]);
                            EnemyShipSunken();
                            break;
                        case HitType.ENDGAME:
                            enemyField.Update(e.ColumnIndex - 1, e.RowIndex - 1);
                            enemyField.ShipSunken(enemyField.PlayingField[e.ColumnIndex - 1, e.RowIndex - 1]);
                            EndGame(true);
                            break;
                    }
                });
                enemyField.ResetSelection();
            }
        }

        /// <summary>
        /// Beendet das Spiel und zeigt die übrigen Schiffe des Gegners an
        /// </summary>
        /// <param name="result"></param>
        private async void EndGame(bool result)
        {
            mainGame.EndGame(result);
            GameEnded = true;

            // Holt sich alle Schiffe die noch nicht getroffen wurden
            IEnumerable<Point> res = await enemyPlayer.GetNonhitShips(playerField.PlayingField.GetNonhitShipTiles().Select(t => t.Position));

            // Blinkt die Schiffe, die noch nicht getroffen wurden bis das Fenster geschlossen wird
            while (true)
            {
                foreach (Point p in res)
                    enemyField.Update(p.X, p.Y);
                await Task.Delay(700);
            };
            
        }

        /// <summary>
        /// Plaziere alle Schiffe zufällig und aktualisiere die Anzahl der Schiffe in ShipGrid
        /// </summary>
        public void RandomShips()
        {
            playerField.Random(playerShipGrid.GetShipsAsInt(true));
            playerShipGrid.UpdateAmount(playerField.PlayingField.Links.ToArray());
        }

        #region EnemyPlayer

        /// <summary>
        /// Für den Gegner um einen Treffer zu setzen
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns>Was für ein Treffertyp der Treffer war</returns>
        public HitType PlayerHit(int columnIndex, int rowIndex)
        {
            // Wenn das Feld getroffen wurde
            
            HitType hitType = playerField.Hit(columnIndex, rowIndex).GetValueOrDefault(HitType.HIT);
            if (hitType == HitType.SUNKEN || hitType == HitType.ENDGAME)
            {
                PlayerShipSunken();
            }
            if (hitType == HitType.ENDGAME)
            {
                EndGame(false);
                return hitType;
            }
            IsPlayerTurn = true;

            return hitType;
        }

        /// <summary>
        /// Setzt den Gegner auf Ready
        /// </summary>
        internal void EnemyReady()
        {
            mainGame.EnemyReady();
        }

        /// <summary>
        /// Für Multiplayer
        /// Um anzuzeigen, wo der Gegner gerade mit der Maus ist
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        internal void HoverCellPlayer(int columnIndex, int rowIndex)
        {
            playerField.ResetSelection();
            playerField.AddSelection(columnIndex, rowIndex);
        }

        /// <summary>
        /// Für Multiplayer
        /// Um anzuzeigen, wo der Gegner gerade mit der Maus ist
        /// </summary>
        internal void HoverCellPlayerEnd()
        {
            playerField.ResetSelection();
        }
        #endregion

        /// <summary>
        /// Ein Spielerschiff wurde versenkt
        /// Aktualisiere die Anzahl der Schiffe in PlayerShipGrid
        /// </summary>
        public void PlayerShipSunken()
        {
            // Filtert alle Tiles die getroffen wurden in ein Array
            playerShipGrid.UpdateAmount(playerField.PlayingField.Links.Where(l => l.IsHit).ToArray());
        }

        /// <summary>
        /// Ein Gegnerschiff wurde versenkt
        /// Aktualisiere die Anzahl der Schiffe in EnemyShipGrid
        /// </summary>
        public void EnemyShipSunken()
        {
            // Filtert alle Tiles die getroffen wurden in ein Array
            enemyShipGrid.UpdateAmount(enemyField.PlayingField.Links.Where(l => l.IsHit).ToArray());
        }

        /// <summary>
        /// Löscht das Spielerfeld und setzt die Anzahl der Schiffe in PlayerShipGrid zurück
        /// </summary>
        public void ClearPlayer()
        {
            playerField.Clear();
            playerShipGrid.ResetAmount();
        }

        /// <summary>
        /// Versteckt das Spielerfeld
        /// </summary>
        /// <param name="isChecked">true bedeutet verstecken</param>
        internal void HidePlayerField(bool isChecked)
        {
            // Verstecke das PlayerField
            playerField.FieldGrid.Visible = !isChecked;
        }


        /// <summary>
        /// Muss vor Themenwechsel aufgerufen werden um das Blinken zu stoppen
        /// -> Somit verhindern das etwas nicht die richtige Farbe hat
        /// </summary>
        internal void ChangeTheme()
        {
            playerField.CancelBlink();
            enemyField.CancelBlink();
        }
    }
}
{   //[MainGameScripts/Field.cs]
	using System.Diagnostics;
	using System.Xml.Serialization;

	namespace GFS_Spiel.MainGameScripts
	{
    public class Field
    {
        public PlayingField PlayingField { get; private set; }
        public DataGridView FieldGrid { get; }
        public Size FieldSize { get; }

        private Tile? lastHitTile;

        #region Events
        public delegate void HoverEventHandler(object? sender, Point e);
        public delegate void UnHoverEventHandler(object? sender, Point e);

        public event HoverEventHandler? HoverEvent;
        public event UnHoverEventHandler? UnHoverEvent;
        #endregion

        /// <summary>
        /// CancellationTokenSource um das Blinken des letzten getroffenen Feldes zwischenzustoppen
        /// </summary>
        private CancellationTokenSource cancellationTokenSource = new();

        public Field(DataGridView dataGridView, Size? size = null)
        {
            if (size == null) FieldSize = new Size(10, 10);

            FieldGrid = dataGridView;
            PlayingField = new(FieldSize);

            //Für Hover
            FieldGrid.CellMouseEnter += mouseEnter;
            FieldGrid.CellMouseClick += mouseClick;
            FieldGrid.CellMouseLeave += mouseLeave;

            genField(FieldSize.Width, FieldSize.Height);
            blinkLastHit();
        }

        /// <summary>
        /// Stoppt das Blinken des letzten getroffenen Feldes
        /// </summary>
        public void CancelBlink()
        {
            cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Blinkt das letzte getroffene Feld
        /// </summary>
        private async void blinkLastHit()
        {
            while (true)
            {
                // Wenn das letzte getroffene Feld nicht null ist, dann blinkt es
                if (lastHitTile is not null)
                {
                    // Speichert das DataGridViewCell des letzten getroffenen Feldes
                    DataGridViewCell dc = FieldGrid[lastHitTile.Position.X + 1, lastHitTile.Position.Y + 1];
                    // Speichert die Farbe des letzten getroffenen Feldes
                    Color prevColor = dc.Style.BackColor == Color.Empty ? FieldGrid.DefaultCellStyle.BackColor : dc.Style.BackColor;
                    // Berechnet die neue Farbe
                    // Wenn die Farbe hell ist wird sie dunkler, wenn sie dunkel ist wird sie heller
                    float f = prevColor.GetBrightness() > (Theme.IsDarkMode() ? 0.5f: 0.51f) ? 0.6f : 2f;
                    Color newColor = Theme.IsDarkMode() ? Color.FromArgb(255, Math.Min((int)(prevColor.R * f), 255), prevColor.G, Math.Min((int)(prevColor.B * f), 255)) : Color.FromArgb(255, prevColor.R, Math.Min((int)(prevColor.G * f), 255), prevColor.B);
                    // Setzt die neue Farbe
                    dc.Style.BackColor = newColor;
                    try
                    {
                        // Wartet 500ms oder bis das Blinken abgebrochen wird
                        await Task.Delay(500, cancellationTokenSource.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        // Erstellt eine neue CancellationTokenSource
                        cancellationTokenSource = new();
                    }
                    // Setzt die Farbe zurück
                    if (dc.Style.BackColor == newColor)
                        // Setzt sie nur zurück wenn sie nach den 500ms nicht von was anderem geändert wurde
                        dc.Style.BackColor = prevColor;
                }
                await Task.Delay(500);
            }
        }

        /// <summary>
        /// Ob das Feld für den Spieler anklickbar ist
        /// </summary>
        /// <param name="value"></param>
        public void DoAllowUserinput(bool value)
        {
            FieldGrid.Enabled = value;
        }

        /// <summary>
        /// Wenn die Maus in ein Feld geht, dann wird das Feld als ausgewählt markiert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseEnter(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.RowIndex > 0)
            {
                AddSelection(e.ColumnIndex - 1, e.RowIndex - 1);
            }
                
        }

        /// <summary>
        /// Wenn die Maus aus einem Feld geht, dann wird die Auswahl des Feldes aufgehoben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.RowIndex > 0)
                RemoveSelection(e.ColumnIndex - 1, e.RowIndex - 1);
        }

        /// <summary>
        /// Wenn die Maus aus einem Feld geht, dann wird die Auswahl des Feldes aufgehoben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseLeave(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.RowIndex > 0)
            {
                RemoveSelection(e.ColumnIndex - 1, e.RowIndex - 1);
            }
                
        }

        /// <summary>
        /// Fügt die Auswahl eines Feldes hinzu
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        public void AddSelection(int columnIndex, int rowIndex)
        {
            FieldGrid[columnIndex + 1, rowIndex + 1].Selected = true;
            HoverEvent?.Invoke(this, PlayingField[columnIndex, rowIndex].Position);
        }

        /// <summary>
        /// Löscht die Auswahl eines Feldes
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        public void RemoveSelection(int columnIndex, int rowIndex)
        {
            FieldGrid[columnIndex + 1, rowIndex + 1].Selected = false;
            UnHoverEvent?.Invoke(this, PlayingField[columnIndex, rowIndex].Position);
        }

        /// <summary>
        /// Löscht die Auswahl aller Felder
        /// </summary>
        public void ResetSelection()
        {
            FieldGrid.ClearSelection();
        }

        /// <summary>
        /// Generiert ein Spielfeld mit der gegebenen größe
        /// </summary>
        /// <param name="sizex"></param>
        /// <param name="sizey"></param>
        private void genField(int sizex, int sizey)
        {

            // +1 Wegen Zahlen und Buchstaben
            sizex++;
            sizey++;

            // Generiere alle Spalten und passt die größe der Spalten an
            for (int i = 0; i < sizex; i++)
            {
                FieldGrid.Columns.Add("Column" + i, i.ToString());

                FieldGrid.Columns[i].Width = FieldGrid.Size.Width / sizex;
            }

            // Generiere alle Reihen und passt die größe der Reihen an
            for (int i = 0; i < sizey; i++)
            {
                FieldGrid.Rows.Add();

                FieldGrid.Rows[i].Height = FieldGrid.Size.Height / sizey;
            }

            // Benenne die Reihen und Zahlen
            for (int i = 1; i < sizex; i++)
            {
                FieldGrid.Rows[0].Cells[i].Value = ((char)(i + 64)).ToString();

                FieldGrid.Rows[i].Cells[0].Value = i.ToString();
            }

            // Passe die größe des Felds auf die größe der Spalten und Reihen an
            FieldGrid.Size = new Size((int)(Math.Floor((decimal)FieldGrid.Size.Height / sizex) * sizex + 3), (int)(Math.Floor((decimal)FieldGrid.Size.Width / sizey) * sizey + 3));

            // Mache die 1 Spalte und Reihe Fett
            FieldGrid.Rows[0].DividerHeight = 2;
            FieldGrid.Columns[0].DividerWidth = 2;
        }

        /// <summary>
        /// Wenn der zustand des Feldes sich wechselt (Schiff oder kein Schiff)
        /// Versuche das Feld in PlayerField zu aktualiesiern
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns>true wenn er sich geöndert hat false wenn nicht</returns>
        public bool Update(int columnIndex, int rowIndex)
        {
            Tile t = PlayingField[columnIndex, rowIndex];
            bool? isShip = PlayingField.Update(t);

            if (isShip == null) return false;

            // Wenn das Feld jetzt ein Schiff ist, dann ändere die Farbe
            updateColor(columnIndex, rowIndex, isShip);

            return true;
        }

        /// <summary>
        /// Markiert alle Felder des Schiffes als versenkt
        /// </summary>
        /// <param name="tile"></param>
        public void ShipSunken(Tile tile)
        {
            // Kennzeichne alle Felder des Schiffes als versenkt
            foreach (Tile t in tile.Link!.Tiles)
            {
                updateColor(t.Position.X, t.Position.Y, true, true);
                // Ändert auch die Farbe der Nachbarn, da hier keine Schiffe mehr sein können
                foreach (Tile n in t.GetNeighbours())
                {
                    n.Hit();
                    updateText(n.Position.X, n.Position.Y, true);
                }
            }
        }

        /// <summary>
        /// Aktualisiert die Farbe des Feldes je nachdem was das Feld ist
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="isShip"></param>
        /// <param name="isSunken"></param>
        private void updateColor(int columnIndex, int rowIndex, bool? isShip, bool isSunken = false)
        {
            switch (isShip)
            {
                case true:
                    FieldGrid[columnIndex + 1, rowIndex + 1].Style.BackColor = Theme.GetThemeColor(isSunken ? SystemColors.HotTrack : SystemColors.HighlightText);
                    break;

                case false:
                    FieldGrid[columnIndex + 1, rowIndex + 1].Style.BackColor = FieldGrid.DefaultCellStyle.BackColor;
                    break;
            }
        }
        /// <summary>
        /// Aktualisiert den Text des Feldes je nachdem ob das Feld getroffen wurde oder nicht
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="isShip"></param>
        /// <param name="isSunken"></param>
        private void updateText(int columnIndex, int rowIndex, bool isHit)
        {
            switch (isHit)
            {
                case true:
                    FieldGrid[columnIndex + 1, rowIndex + 1].Value = "X";
                    break;
                case false:
                    FieldGrid[columnIndex + 1, rowIndex + 1].Value = "";
                    break;
            }
        }

        /// <summary>
        /// Versucht das Feld als getroffen zu markieren
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns>
        /// null -> Feld war schon getroffen.
        /// <see cref="HitType"/> gibt an was getroffen wurde
        /// </returns>
        public HitType? Hit(int columnIndex, int rowIndex)
        {
            // Wenn das Feld Getroffen wurde, dann ändere Text

            Tile tile = PlayingField[columnIndex, rowIndex];

            if (tile.IsHit) return null;
            lastHitTile = tile; 
            tile.Hit();

            updateText(columnIndex, rowIndex, true);
            //hitAnimation();

            // Falls nun alle Felder des Schiffes getroffen wurden, dann kennzeichne das Schiff als versenkt
            if (tile.Link != null)
            {
                if (tile.Link.IsHit)
                {
                    ShipSunken(tile);
                    if (PlayingField.AllShipsHit())
                        return HitType.ENDGAME;
                    return HitType.SUNKEN;
                }
                return HitType.HIT;
            }
            return HitType.MISS;
        }

        /// <summary>
        /// Versucht ein zufälliges Feld zu generieren
        /// </summary>
        /// <param name="ships">Schiffe wie in <see cref="ShipGrid.GetShipsAsInt"/></param>
        /// <param name="useCurrent">Ob er versuchen soll die schiffe auf das vorhandene Feld zu plazieren</param>
        /// <returns>Ob es Funktioniert hat</returns>
        public bool Random(IEnumerable<int> ships, bool useCurrent = true)
        {
            // Generiere ein neues Spielfeld mit den gegebenen Schiffen
            PlayingField? result = useCurrent ? PlayingField.Random(ships) : PlayingField.Random(FieldSize, ships);

            // Wenn das Spielfeld nicht generiert werden konnte, dann breche ab
            if (result == null) return false;

            PlayingField = result;

            // Aktualisiere die Farbe der Felder
            for (int i = 0; i < FieldSize.Width; i++)
            {
                for (int j = 0; j < FieldSize.Height; j++)
                {
                    updateColor(i, j, PlayingField[i, j].Link != null);
                }
            }
            return true;
        }

        /// <summary>
        /// Setzt das Spielfeld auf leer zurück
        /// </summary>
        public void Clear()
        {
            // Lösche das Spielfeld
            PlayingField = new PlayingField(FieldSize);

            for (int i = 0; i < FieldSize.Width; i++)
            {
                for (int j = 0; j < FieldSize.Height; j++)
                {
                    updateColor(i, j, false);
                }
            }
        }

        /// <summary>
        /// Gibt an Was getroffen wurde
        /// </summary>
        public enum HitType
        {
            /// <summary>
            /// Ein Schiffteil wurde getroffen
            /// </summary>
            HIT,
            /// <summary>
            /// Das Wasser wurde getroffen
            /// </summary>
            MISS,
            /// <summary>
            /// Ein Schiff wurde versenkt
            /// </summary>
            SUNKEN,
            /// <summary>
            /// Alle Schiffe wurden versenkt
            /// </summary>
            ENDGAME
        }
    }
}

}
{   //[MainGameScripts/PlayingField.cs]
    public class PlayingField
    {
        public readonly Size Size;
        public Tile[,] Tiles { get; }

        public readonly List<Link> Links;

        public PlayingField(Size _size)
        {
            Size = _size;
            Tiles = new Tile[Size.Width, Size.Height];
            Links = new();
            genTiles(Size);
        }

        /// <summary>
        /// Erstellt ein zufälliges Spielfeld
        /// </summary>
        /// <param name="size">Die größe des Feldes</param>
        /// <param name="ships">Die anzahl von den schiffen, wie in ShiipGrid erklärt</param>
        /// <param name="maxAttempts">Wie viele Versuche er machen soll ein Feld zufällig erstellen</param>
        /// <returns></returns>
        public static PlayingField? Random(Size size, IEnumerable<int> ships, int maxAttempts = 10)
        {
            // Erstelle ein neues Spielfeld und versuche zufällig Schiffe zu platzieren
            return new PlayingField(size).Random(ships, maxAttempts);
        }

        /// <summary>
        /// Gibt das <see cref="Tile"/> an der Position zurück
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile this[int x, int y]
        {
            get => Tiles[x, y];
        }

        /// <summary>
        /// Gibt das <see cref="Tile"/> an der Position zurück
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Tile this[Point pos]
        {
            get => Tiles[pos.X, pos.Y];
        }

        /// <summary>
        /// {Left, Bottom, Right, Top} array of distances from the point to the next hit
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int[] DistancesToNext(Point p)
        {
            List<int> distances = new();
            for (int i = -1; i <= 1; i += 2)
            {
                int y;
                for (y = p.Y; y >= 0 && y < Size.Height; y += 1 * i)
                {
                    if (Tiles[p.X, y].IsHit)
                    {
                        distances.Add(Math.Abs(y - p.Y) - 1);
                        break;
                    }
                }
                if (!(y >= 0 && y < Size.Height)) distances.Add(Math.Abs(y - p.Y) - 1);
                int x;
                for (x = p.X; x >= 0 && x < Size.Width; x += 1 * i)
                {
                    if (Tiles[x, p.Y].IsHit)
                    {
                        distances.Add(Math.Abs(x - p.X)-1);
                        break;
                    }
                }
                if (!(x >= 0 && x < Size.Width)) distances.Add(Math.Abs(x - p.X) - 1);
            }
            return distances.ToArray();
        }

        /// <summary>
        /// Gibt alle Tiles zurück, die nicht getroffen wurden und ein Schiff enthalten
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tile> GetNonhitShipTiles()
        {
            return Tiles.Cast<Tile>().Where(t => !t.IsHit && t.Link != null);
        }

        /// <summary>
        /// Gibt ein zufälliges leeres Feld zurück
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        private Tile? randomEmptyTile(Random random)
        {
            // Gehe alle Felder durch und suche ein leeres Feld ohne Nachbarn
            int rx = random.Next(0, Size.Width);
            int ry = random.Next(0, Size.Height);

            for (int i = 0; i < Size.Width; i++)
            {
                for (int j = 0; j < Size.Height; j++)
                {
                    int x = (rx + i) % Size.Width;
                    int y = (ry + j) % Size.Height;

                    if (Tiles[x, y].CountNeighbours() == 0)
                    {
                        return Tiles[x, y];
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Generiert alle Tiles im Spielfeld
        /// </summary>
        /// <param name="size"></param>
        private void genTiles(Size size)
        {
            // Erstelle ein neues Spielfeld
            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    Tiles[i, j] = new Tile(i, j, this);
                }
            }
        }

        /// <summary>
        /// Versucht das Tile zu einem Link hinzuzufügen/entfernen und somit als ein/kein "schiff" zu setzen
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public bool? Update(int columnIndex, int rowIndex)
        {
            // Gibt null zurück, wenn das Feld nicht geLinkt werden kann
            Tile tile = Tiles[columnIndex, rowIndex];
            return Update(tile);
        }

        /// <summary>
        /// Versucht das Tile zu einem Link hinzuzufügen/entfernen und somit als ein/kein "schiff" zu setzen
        /// </summary>
        /// <param name="tile"></param>
        /// <returns>
        /// false   -> wenn es geUnLinkt wurde
        /// true    -> wenn es geLinkt wurde
        /// null    -> wenn es nicht aktualisiert werden kann
        /// </returns>
        public bool? Update(Tile tile)
        {
            Link[]? links = tile.NeighbourLinks().ToArray();

            // Wenn das Feld geLinked ist
            if (tile.Link != null)
            {
                // Entferne das Feld aus dem Link
                Link link = tile.Link;
                tile.Link.Remove(tile);
                tile.Link = null;

                // Wenn der Link leer ist, entferne ihn
                if (link.Length == 0)
                {
                    Links.Remove(link);
                }
                // Wenn das Tile in der Mitte von einem Link ist
                if (links.Length == 2)
                {
                    // Lösche den link für alle Tiles
                    link.Tiles.ForEach((t) =>
                    {
                        t.Link = null;
                    });
                    // Update alle Tiles in dem alten Link
                    link.Tiles.ToList().ForEach((t) => Update(t));

                    // Entferne den alten Link
                    Links.Remove(link);
                }

                tile.Link = null;

                return false;
            }

            // Wenn Feld nicht geLinkt ist
            switch (links.Length)
            {
                //Erstelle neuen Link
                case 0:
                    // Wenn die Nachbarn leer sind, kann das Feld geLinkt werden
                    if (tile.CountNeighbours() != 0) return null;
                    // Erstelle neuen Link und füge hinzu
                    tile.Link = new Link();
                    tile.Link.Add(tile);
                    Links.Add(tile.Link);
                    break;

                //Füge neues Tile zum vorhanden Link hinzu
                case 1:
                    // Wenn es nur ein Nachbar gibt und weniger als 6 im Link sind, kann das Feld geLinkt werden
                    // Und wenn er die gleiche Richtung hat
                    if (tile.CountNeighbours() != 1) return null;
                    if (links[0].Length == 6) return null;
                    // Füge hinzu
                    tile.Link = links[0];
                    links[0].Add(tile);
                    break;

                //Verbinde die Links und füge hinzu
                case 2:
                    // Wenn es nur 2 NachbarTiles gibt
                    if (tile.CountNeighbours() != 2) return null;
                    // Und die Links zusammen nicht länger als 5 sind
                    if (links[0].Length + links[1].Length >= 6) return null;
                    // Und wenn sie die gleiche Richtung haben
                    if (!(links[0].Vertical == null || links[1].Vertical == null || (links[1].Vertical == links[0].Vertical))) return null;
                    // Verbinde die Links
                    links[1].Tiles.ToList().ForEach((t) => { t.Link = links[0]; links[0].Add(t); });
                    Links.Remove(links[1]);
                    // Füge hinzu
                    tile.Link = links[0];
                    tile.Link.Add(tile);
                    break;
            }
            return true;

        }

        /// <summary>
        /// Erstellt eine komplett neue Kopie des Spielfeldes
        /// </summary>
        /// <returns></returns>
        public PlayingField Clone()
        {
            // Clones the PlayingField
            PlayingField clone = new PlayingField(Size);
            // Clone all Tiles
            for (int i = 0; i < Size.Width; i++)
            {
                for (int j = 0; j < Size.Height; j++)
                {
                    if (Tiles[i, j].IsHit)
                        clone.Tiles[i, j].Hit();
                }
            }
            // Clone all Links
            foreach (Link link in Links)
            {
                Link copyLink = new Link();
                foreach (Tile tile in link.Tiles)
                {
                    clone.Tiles[tile.Position.X, tile.Position.Y].Link = copyLink;
                    copyLink.Add(clone.Tiles[tile.Position.X, tile.Position.Y]);
                }
                clone.Links.Add(copyLink);
            }
            return clone;
        }

        /// <summary>
        /// Versucht die Schiffe auf dem Spielfeld zufällig zu platzieren
        /// </summary>
        /// <param name="ships"></param>
        /// <param name="maxAttempts"></param>
        /// <returns></returns>
        public PlayingField? Random(IEnumerable<int> ships, int maxAttempts = 10)
        {
            // Zufällige Reihenfolge der Schiffe
            return TryPlaceShips(ships, this, maxAttempts, new Random());
        }

        /// <summary>
        /// Versucht die Schiffe durch Rekursion auf dem Spielfeld zufällig zu platzieren
        /// </summary>
        /// <param name="shipsRemaining"></param>
        /// <param name="board"></param>
        /// <param name="maxAttempts"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        private PlayingField? TryPlaceShips(IEnumerable<int> shipsRemaining, PlayingField board, int maxAttempts, Random random)
        {
            int? shipToPlace = shipsRemaining.FirstOrDefault();

            // Alle Schiffe platziert -> fertig
            if (shipToPlace == 0)
                return board;


            for (int attempts = 0; attempts < maxAttempts; attempts++)
            {
                PlayingField boardCopy = board.Clone();

                Tile? pos = boardCopy.randomEmptyTile(random);

                // Wenn kein Platz mehr frei ist -> abbrechen
                if (pos == null)
                    return null;

                // Zufällige Orientierung
                int orientation = random.Next(0, 2);

                // Überprüfe ob Platz für Schiff
                bool b = false;
                for (int i = 0; i < shipToPlace; i++)
                {
                    // weil größer werden könnte als sollte
                    int x = pos.Position.X + i * orientation;
                    int y = pos.Position.Y + i * (orientation ^ 1);
                    if (x >= Size.Width || y >= Size.Height)
                    {
                        b = true;
                        break;
                    }
                    Tile t = boardCopy[x, y];
                    // Wenn ein Nachbar, außer das voherige existiert, dann ist kein Platz
                    if (t.CountNeighbours() > 1) { b = true; break; }
                    boardCopy.Update(t);
                }
                // Wenn kein Platz -> nächster Versuch
                if (b) continue;

                // Platz gefunden -> Schiff setzen
                // Nächtste Schiffe versuchen -> Rekursion
                PlayingField? nextBoard = TryPlaceShips(shipsRemaining.Skip(1), boardCopy, maxAttempts, random);
                if (nextBoard != null)
                    return nextBoard;
            }
            return null;
        }

        /// <summary>
        /// Gibt zurück ob alle Schiffe versenkt sind
        /// </summary>
        /// <returns>true wenn alle Schiffe versenkt</returns>
        public bool AllShipsHit()
        {
            // Wenn alle Schiffe versenkt sind
            return Links.All((l) => l.IsHit);
        }
    }

    public class Tile
    {
        public readonly Point Position;

        public Link? Link { get; set; }
        public bool IsHit { get; private set; }

        private PlayingField field;

        public Tile(int x, int y, PlayingField pField, Link? link = null, bool isHit = false)
        {
            Position = new(x, y);

            Link = link;
            IsHit = isHit;
            field = pField;
        }

        /// <summary>
        /// Setzt das Feld als getroffen
        /// </summary>
        public void Hit()
        {
            IsHit = true;
        }
        /// <summary>
        /// Gibt alle Nachbarn zurück, nach dem Schema:
        ///     + + + 
        ///     + x + 
        ///     + + +  
        /// </summary>
        /// <param name="field"></param>
        /// <returns>Die benachbarten Tiles</returns>
        public IEnumerable<Tile> GetNeighbours()
        {
            
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // Wenn das Tile nicht außerhalb des Feldes liegt und nicht das eigene Tile ist
                    if (Position.X + i >= 0 && Position.X + i < field.Size.Width && Position.Y + j >= 0 && Position.Y + j < field.Size.Height && !(j == 0 && i == 0))
                    {
                        yield return field[Position.X + i, Position.Y + j];
                    }
                }
            }
        }
        /// <summary>
        /// Gibt die anzahl der Nachbarn zurück, die ein Link haben
        /// </summary>
        /// <returns></returns>
        public int CountNeighbours()
        {
            return GetNeighbours().Count((t) => t.Link != null);
        }
        /// <summary>
        /// Gibt die Links der Nachbarn zurück
        ///       +   
        ///     + x + 
        ///       +  
        /// Dies ist notwenig um zu überprüfen, wie ein feld geLinkt werden muss (siehe <see cref="PlayingField.Update"/>)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Link> NeighbourLinks()
        {
            foreach (Point p in new Point[] { new Point(0, -1), new Point(0, 1), new Point(-1, 0 ), new Point(1, 0 ) })
            {
                // Wenn das Tile nicht außerhalb des Feldes liegt und und ein Link existiert
                if (Position.X + p.X >= 0 && Position.X + p.X < field.Size.Width && Position.Y + p.Y >= 0 && Position.Y + p.Y < field.Size.Height && field[Position.X + p.X, Position.Y + p.Y].Link != null)
                {
                    yield return field[Position.X + p.X, Position.Y + p.Y].Link!;
                }
            }
        }
    }

    public class Link
    {
        public List<Tile> Tiles { get; private set; }
        public int Length => Tiles.Count;

        /// <summary>
        /// true -> Vertikal
        /// false -> Horizontal
        /// null -> Länge 1
        /// </summary>
        public bool? Vertical => isVertical();

        /// <summary>
        /// Alle Tiles sind getroffen
        /// </summary>
        public bool IsHit => Tiles.All((t) => t.IsHit);

        /// <summary>
        /// Anzahl der getroffenen Tiles
        /// </summary>
        public int HitCount => Tiles.Count((t) => t.IsHit);

        public Link()
        {
            Tiles = new List<Tile>();
        }

        /// <summary>
        /// Tile zum Link hinzufügen
        /// </summary>
        /// <param name="tile"></param>
        public void Add(Tile tile)
        {
            Tiles.Add(tile);
        }

        /// <summary>
        /// Tile aus dem Link entfernen
        /// </summary>
        /// <param name="tile"></param>
        public void Remove(Tile tile)
        {
            Tiles.Remove(tile);
        }

        /// <summary>
        /// true -> Vertikal
        /// false -> Horizontal
        /// null -> Länge 1
        /// </summary>
        /// <returns></returns>
        private bool? isVertical()
        {
            // Wenn länge 1 -> weder noch
            if (Length == 1) return null;

            // Wenn 2 X-Werte gleich sind -> vertikal
            return Tiles[0].Position.X == Tiles[1].Position.X;
        }
    }
}
{   //[MainGameScripts/EnemyPlayers/EnemyPlayer.cs]
    public abstract class EnemyPlayer
    {
        private readonly FieldHandler fieldHandler;

        private protected Size FieldSize { get => fieldHandler.FieldSize; }
        /// <summary>
        /// Siehe <see cref="ShipGrid.GetShipsAsInt(bool)"/>
        /// </summary>
        private protected IEnumerable<int> FieldConfig { get => fieldHandler.FieldConfig; }
        private protected bool IsPlayerTurn { get => fieldHandler.IsPlayerTurn; set => fieldHandler.IsPlayerTurn = value; }
        private protected bool IsPlayerReady { get => fieldHandler.IsPlayerReady; }

        public EnemyPlayer(FieldHandler _fieldHandler)
        {
            fieldHandler = _fieldHandler;
        }
        /// <summary>
        /// Siehe <see cref="FieldHandler.PlayerHit"/>
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Siehe <see cref="FieldHandler.PlayerHit"/></returns>
        private protected HitType HitPlayer(Point point) => fieldHandler.PlayerHit(point.X, point.Y);
        /// <summary>
        /// Siehe <see cref="FieldHandler.PlayerHit"/>
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Siehe <see cref="FieldHandler.PlayerHit"/></returns>
        private protected HitType HitPlayer(int columnIndex, int rowIndex) => fieldHandler.PlayerHit(columnIndex, rowIndex);

        /// <summary>
        /// Funktion die aufgerufen werden muss wenn der Gegner bereit ist
        /// </summary>
        private protected void EnemyReady() => fieldHandler.EnemyReady();

        /// <summary>
        /// Markiert die Zelle auf dem Spielerfeld
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        private protected void HoverCell(int columnIndex, int rowIndex) => fieldHandler.HoverCellPlayer(columnIndex, rowIndex);

        /// <summary>
        /// Löscht die Markierung auf dem Spielerfeld
        /// </summary>
        private protected void HoverCellEnd() => fieldHandler.HoverCellPlayerEnd();

        /// <summary>
        /// Wird vom <see cref="FieldHandler"/> aufgerufen wenn der Spieler bereit ist
        /// </summary>
        public abstract void OnPlayerReady();

        /// <summary>
        /// Wird vom <see cref="FieldHandler"/> aufgerufen wenn das Spiel geladen hat
        /// </summary>
        public abstract void OnLoad();

        /// <summary>
        /// Wird vom <see cref="FieldHandler"/> aufgerufen wenn der Spieler ein Feld getroffen hat
        /// </summary>
        /// <param name="point">Das </param>
        /// <returns></returns>
        public abstract Task<HitType> OnEnemyFieldHit(Point point);

        /// <summary>
        /// Gibt alle Schiffe zurück, die noch nicht vom Spieler getroffen wurden
        /// </summary>
        /// <param name="leftShips">Ein <c>IEnmuerable</c>, von allen übrigen Schiffe des Spielers als <see cref="Point"/></param>
        /// <returns></returns>
        public abstract Task<IEnumerable<Point>> GetNonhitShips(IEnumerable<Point> leftShips);

        /// <summary>
        /// Optional: Wird vom <see cref="FieldHandler"/> aufgerufen wenn der Spieler mit der Maus über eine GegnerZelle geht
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="point"></param>
        public virtual void OnEnemyFieldHover(object? sender, Point point) { return ; }
        /// <summary>
        /// Optional: Wird vom <see cref="FieldHandler"/> aufgerufen wenn der Spieler die GegnerZelle verlassen hat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="point"></param>
        public virtual void OnEnemyFieldUnHover(object? sender, Point point) { return ; }
    }
}
{   //[MainGameScripts/EnemyPlayers/ComputerPlayer.cs]
    internal class ComputerPlayer : EnemyPlayer
    {
        private PlayingField playerPlayingField;
        private PlayingField enemyPlayingField;

        private Tile lastTile;
        private bool lastTileShipSunken;

        private Random random = new Random();

        #pragma warning disable CS8618
        public ComputerPlayer(FieldHandler _fieldHandler) : base(_fieldHandler)
        #pragma warning restore CS8618
        {
            playerPlayingField = new PlayingField(FieldSize);
            enemyPlayingField = PlayingField.Random(FieldSize, FieldConfig)!;
        }

        /// <summary>
        /// Trifft den Spieler und aktualisiert das Spielfeld des playerPlayingFields damit der Computer weis, wo er schon getroffen hat
        /// </summary>
        /// <param name="t"></param>
        private void hitPlayer(Tile t)
        {
            t.Hit();
            switch (HitPlayer(t.Position))
            {
                case HitType.HIT:
                    playerPlayingField.Update(t.Position.X, t.Position.Y);
                    break;
                case HitType.SUNKEN:
                    lastTileShipSunken = true;
                    playerPlayingField.Update(t.Position.X, t.Position.Y);
                    // Alle Felder um das Schiff herum werden als getroffen markiert weil hier kein Schiff mehr sein kann
                    foreach (Tile ti in t.Link!.Tiles)
                    {
                        foreach (Tile n in ti.GetNeighbours())
                        {
                            n.Hit();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Führt einen Zug aus
        /// </summary>
        public async void Move()
        {
            await Task.Delay(random.Next(500, 1500));

            // wenn das letzte Feld ein Schiff war, dann wird das Schiff versucht zu versenken
            if (lastTile != null && lastTile.Link != null && !lastTileShipSunken)
            {
                if (lastTile.Link.HitCount == 1) // Der Computer weiß die Richtung noch nicht da nur ein Segement des Schiffes getroffen
                {
                    // Es wird eines der Felder um das Trefferfeld herum ausgewählt
                    foreach (Point p in new Point[] { new Point(0, -1), new Point(0, 1), new Point(1, 0), new Point(-1, 0) }.OrderBy(p => random.Next()))
                    {
                        // Wenn das Feld außerhalb des Spielfeldes liegt, wird es übersprungen
                        if (!(lastTile.Position.X + p.X >= 0 && lastTile.Position.X + p.X < enemyPlayingField.Size.Width && lastTile.Position.Y + p.Y >= 0 && lastTile.Position.Y + p.Y < enemyPlayingField.Size.Height))
                            continue;

                        // Wenn das Feld schon getroffen wurde, wird es übersprungen
                        Tile t = playerPlayingField[lastTile.Position.X + p.X, lastTile.Position.Y + p.Y];
                        if (t.IsHit) continue;

                        // Das Feld wird getroffen
                        hitPlayer(t);
                        break;
                    }

                }
                else
                {
                    // Es wird ein Feld ausgewählt, das noch nicht getroffen wurde
                    HashSet<Tile> possible = new HashSet<Tile>();
                    foreach (Tile t in lastTile.Link.Tiles.Where(t => t.IsHit)) // Für jedes getroffene Feld
                    {
                        // Es wird für jedes getroffene Feld ein Feld in der Richtung des Schiffes gesucht (+1 und -1)
                        for (int i = -1; i <= 1; i += 2)
                        {
                            int x = t.Position.X + (lastTile.Link.Vertical!.Value ? 0 : i);
                            int y = t.Position.Y + (lastTile.Link.Vertical.Value ? i : 0);

                            // Wenn das Feld außerhalb des Spielfeldes liegt, wird es übersprungen
                            if (!(x >= 0 && x < enemyPlayingField.Size.Width && y >= 0 && y < enemyPlayingField.Size.Height))
                                continue;
                            possible.Add(playerPlayingField[t.Position.X + ((bool)lastTile.Link.Vertical ? 0 : i), t.Position.Y + ((bool)lastTile.Link.Vertical ? i : 0)]);
                        }
                    }

                    // Es wird ein zufälliges Feld von den möglichen Feldern ausgewählt
                    foreach (Tile t in possible.OrderBy(p => random.Next()))
                    {
                        if (t.IsHit) continue;

                        hitPlayer(t);
                        break;
                    }
                }
            }
            else
            {
                // Wenn das letzte Feld kein Schiff war, wird ein zufälliges Feld ausgewählt

                // Alle Felder die noch nicht getroffen wurden werden als mögliche Felder gespeichert
                List<Tile> possibleMoves = playerPlayingField.Tiles.Cast<Tile>().Where((t) => !t.IsHit).ToList();

                // Die Felder werden nach der Entfernung zum nächsten getroffenen Feld sortiert und dann innerhalb der Entfernung versucht das mittigste auszuwählen und zufällig ausgewählt
                lastTile = possibleMoves.OrderBy(p => 
                { 
                    int[] a = playerPlayingField.DistancesToNext(p.Position); 
                    int vertical = a[0] + a[2];
                    int horizontal = a[1] + a[3];
                    return 
                    FieldSize.Width - vertical + 
                    FieldSize.Height - horizontal + 
                    random.NextSingle() + 
                    Math.Abs(a[0] - a[2]) / Math.Max(vertical, 1) + 
                    Math.Abs(a[1] - a[3]) / Math.Max(horizontal, 1); 
                }
                ).ElementAt(0);

                lastTileShipSunken = false;

                hitPlayer(lastTile);
            }
        }

        /// <summary>
        /// Entscheidet ob der Computer anfängt oder nicht
        /// </summary>
        public override void OnPlayerReady()
        {
            // Wenn beide Spieler bereit sind, wird ein zufälliger Spieler ausgewählt, der anfängt
            IsPlayerTurn = new Random().Next(0, 2) == 0;

            // Wenn der Computer anfängt, wird ein Zug gemacht
            if (!IsPlayerTurn)
                Move();
        }

        /// <summary>
        /// Setzt den Computer auf Bereit
        /// </summary>
        public override void OnLoad()
        {
            EnemyReady();
        }

        /// <summary>
        /// Gibt zurück was für ein Treffer der Spieler gemacht hat
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override Task<HitType> OnEnemyFieldHit(Point point)
        {
            // Wenn der Computer an der Reihe ist, wird das Feld getroffen
            Tile t = enemyPlayingField[point];
            t.Hit();

            // Wenn alle Schiffe versenkt wurden, wird das Spiel beendet
            if (enemyPlayingField.Links.All(l => l.IsHit))
                return Task.FromResult(HitType.ENDGAME);

            Move();
            // Wenn das Feld ein Schiff war, wird das Schiff versenkt
            if (t.Link != null)
            {
                if (t.Link.IsHit)
                    return Task.FromResult(HitType.SUNKEN);
                return Task.FromResult(HitType.HIT);
            }
            // Wenn das Feld kein Schiff war, wird es als verfehlt markiert
            return Task.FromResult(HitType.MISS);
        }

        /// <summary>
        /// Gibt alle Positionen der Schiffe zurück, die noch nicht vom Spieler getroffen wurden
        /// </summary>
        /// <param name="leftShips"></param>
        /// <returns></returns>
        public override Task<IEnumerable<Point>> GetNonhitShips(IEnumerable<Point> leftShips)
        {
            return Task.FromResult(enemyPlayingField.GetNonhitShipTiles().Select(x => x.Position));
        }
    }
}
{   //[MainGameScripts/EnemyPlayers/NetworkPlayer.cs]
    public class NetworkPlayer : EnemyPlayer
    {
        private TcpClient connection;
        private bool isEnemyReady = false;

        public NetworkPlayer(FieldHandler fieldHandler, TcpClient _connection) : base(fieldHandler)
        {
            connection = _connection;
        }
        /// <summary>
        /// Callback bevor das Spiel beginnt
        /// </summary>
        /// <param name="message"></param>
        private void callback(StunTools.Message message)
        {
            // Wenn Bool dann ist es der IsPlayerTurn Wert
            if (message.ObjectType == typeof(bool))
            {
                IsPlayerTurn = message.GetData<bool>();
                if (!IsPlayerTurn)
                    // Der nicht spielende Spieler muss auf einen Zug warten
                    _ = connection.Receive().ContinueWith(async (task) => await onPointMessage(task.Result!));
            }
            else
            {
                // Wenn String dann ist es "READY" und der Gegner ist bereit
                isEnemyReady = true;
                EnemyReady();
            }
        }

        /// <summary>
        /// Das Callback für den wartenden Spieler
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task onPointMessage(StunTools.Message message)
        {
            // Wenn die Nachricht ein Point ist, dann wurde der Spieler getroffen und muss zurückmelden was es für ein Treffer war
            if (message.ObjectType == typeof(Point))
            {
                Point point = message.GetData<Point>();

                // Gibt den Treffertyp zurück
                await connection.SendData(HitPlayer(point));
                HoverCellEnd();
            }
            else
            {
                // Wenn die Nachricht ein string ist, dann ist es "Unhover"
                if (message.ObjectType == typeof(string))
                {
                    HoverCellEnd();
                }
                else
                {
                    // Wenn die Nachricht ein Tuple aus String und Point ist, dann ist es "Hover" und die Position der "Hover" Zelle
                    HoverCell(message.GetData<(string, Point)>().Item2.X, message.GetData<(string, Point)>().Item2.Y);
                }

                _ = connection.Receive().ContinueWith(async (task) => await onPointMessage(task.Result!));
            }
        }

        /// <summary>
        /// Wartet auf die nachricht vom Gegner, was für ein Treffer es war
        /// </summary>
        /// <returns></returns>
        private async Task<HitType?> WaitHitTypeMessage()
        {
            StunTools.Message? message = null;
            while (message?.ObjectType != typeof(HitType))
                message = await connection.Receive();
            // Jetzt der Spieler wieder auf einen Zug warten
            _ = connection.Receive().ContinueWith(async (task) => await onPointMessage(task.Result!));
            return message?.GetData<HitType>();
        }

        /// <summary>
        /// Gibt zurück was für ein Treffer der Spieler gemacht hat
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override async Task<HitType> OnEnemyFieldHit(Point point)
        {
            await connection.SendData(point);
            return (await WaitHitTypeMessage()).Value;
        }

        /// <summary>
        /// Sendet dem Gegner das man bereit ist
        /// </summary>
        public override async void OnPlayerReady()
        {
            if (!isEnemyReady)
                // Wenn der Gegner noch nicht bereit ist, wird "READY" gesendet
                await connection.SendData("READY");
            else
            {
                // Wenn der Gegner bereit ist, wird ein zufälliger Spieler ausgewählt, der anfängt und dem Gegner mitgeteilt
                IsPlayerTurn = new Random().Next(0, 2) == 0;
                await connection.SendData(!IsPlayerTurn);
                if (!IsPlayerTurn)
                    _ = connection.Receive().ContinueWith(async (task) => await onPointMessage(task.Result!));
            }
        }

        /// <summary>
        /// Startet den callback bis beide bereit sind
        /// </summary>
        public override void OnLoad()
        {
            _ = connection.Receive().ContinueWith(task => callback(task.Result));
        }

        /// <summary>
        /// Sendet dem gegner die Position der Zelle aud der, der Mauszeiger ist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="point"></param>
        public override async void OnEnemyFieldHover(object? sender, Point point)
        {
            if (IsPlayerTurn)
                await connection.SendData(("Hover", point));
        }

        /// <summary>
        /// Sendet dem gegner das der Mauszeiger nicht mehr auf dem Feld ist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="point"></param>
        public override async void OnEnemyFieldUnHover(object? sender, Point point)
        {
            if (IsPlayerTurn)
                await connection.SendData("Unhover");
        }
        /// <summary>
        /// Gibt alle Positionen der Schiffe zurück, die noch nicht vom Spieler getroffen wurden
        /// Und sendet dem Gegner die Positionen der Schiffe die von ihm noch nicht getroffen wurden
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<Point>> GetNonhitShips(IEnumerable<Point> l)
        {
            await connection.SendData(l);
            return (await connection.Receive())!.GetData<IEnumerable<Point>>()!;
        }
    }
}