using GFS_Spiel.MainGameScripts;
using StunTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GFS_Spiel.MainGameScripts.Field;
using static GFS_Spiel.MainGameScripts.FieldHandler;

namespace GFS_Spiel.MainGameScripts.EnemyPlayers
{
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