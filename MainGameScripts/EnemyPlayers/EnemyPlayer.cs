using static GFS_Spiel.MainGameScripts.Field;
using static GFS_Spiel.MainGameScripts.FieldHandler;

namespace GFS_Spiel.MainGameScripts.EnemyPlayers
{
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
        /// Gibt alle Positionen der Schiffe zurück, die noch nicht vom Spieler getroffen wurden
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
