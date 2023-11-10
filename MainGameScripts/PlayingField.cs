using System.Diagnostics;
using System.Numerics;

namespace GFS_Spiel.MainGameScripts
{
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