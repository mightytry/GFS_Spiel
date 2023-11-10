using GFS_Spiel.MainGameScripts.EnemyPlayers;
using Newtonsoft.Json.Linq;
using StunTools;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static GFS_Spiel.MainGameScripts.Field;

namespace GFS_Spiel.MainGameScripts
{
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
        public bool IsPlayerReady { get; private set; } = false;

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
            IsPlayerReady = true;
            enemyPlayer.OnPlayerReady();
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
                _ = enemyPlayer.OnEnemyFieldHit(enemyField.PlayingField[e.ColumnIndex - 1, e.RowIndex - 1].Position).ContinueWith(x =>
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

            // Holt sich alle Schiffe die noch nicht getroffen wurden und gibt seine nicht getroffenen Schiffe an den Gegner
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
        /// Platziere alle Schiffe zufällig und aktualisiere die Anzahl der Schiffe in ShipGrid
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