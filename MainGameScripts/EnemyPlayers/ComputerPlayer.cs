using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using static GFS_Spiel.MainGameScripts.Field;
using static GFS_Spiel.MainGameScripts.FieldHandler;

namespace GFS_Spiel.MainGameScripts.EnemyPlayers
{
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