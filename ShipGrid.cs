using GFS_Spiel.MainGameScripts;
using Newtonsoft.Json;
using System.Diagnostics;
using static GFS_Spiel.MainGameScripts.Field;

namespace GFS_Spiel
{
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
