using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealClue
{
    public class Room
    {
        public Room(RoomId roomId)
        {
            RoomId = roomId;
        }

        public RoomId RoomId { get; private set; }

        private Player _player;

        /// <summary>
        /// 플레이어가 방에 입장하면 후보 카드를 가져온다, 플레이어가 입장한 방이 클루 룸인지 확인한다.
        /// </summary>
        /// <param name="_player"></param>
        public void PlayerEntered(Player _player)
        {
            _player.GetCandidatedCards(RoomId, Deck.Instance.Cards);

            CheckClueRoom();

            //TODO 다음 반박자를 가져오도록 하는 대상을 구해야 한다.
            Player disprovingPlayer = _player.GetNextDisprovingPlayer(3);
        }

        public void CheckClueRoom()
        {
            AnswerChecker answerChecker = new AnswerChecker();

            if (RoomId == RoomId.Clue)
                answerChecker.GetWinner(_player);
        }

        //#region EnteredPlayer event things for C# 3.0
        //public event EventHandler<EnteredPlayerEventArgs> EnteredPlayer;

        //protected virtual void OnEnteredPlayer(EnteredPlayerEventArgs e)
        //{
        //    if (EnteredPlayer != null)
        //        EnteredPlayer(this, e);
        //}

        //private EnteredPlayerEventArgs OnEnteredPlayer(Player player)
        //{
        //    EnteredPlayerEventArgs args = new EnteredPlayerEventArgs(player);
        //    OnEnteredPlayer(args);

        //    return args;
        //}

        //private EnteredPlayerEventArgs OnEnteredPlayerForOut()
        //{
        //    EnteredPlayerEventArgs args = new EnteredPlayerEventArgs();
        //    OnEnteredPlayer(args);

        //    return args;
        //}

        //public class EnteredPlayerEventArgs : EventArgs
        //{
        //    public Player Player { get; set; }

        //    public EnteredPlayerEventArgs()
        //    {
        //    }

        //    public EnteredPlayerEventArgs(Player player)
        //    {
        //        Player = player;
        //    }
        //}
        //#endregion
    }
}
