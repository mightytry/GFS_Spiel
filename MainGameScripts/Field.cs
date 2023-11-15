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
                FieldGrid.Rows[i].Cells[0].Value = i.ToString();
            }

            // Benenne die Reihen und Zahlen
            for (int i = 1; i < sizex; i++)
            {
                FieldGrid.Rows[0].Cells[i].Value = ((char)(i + 64)).ToString();
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
