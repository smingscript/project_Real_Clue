using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealClue
{
    [Serializable]
    public class Player
    {
        public Player(PlayerId playerId)
        {
            PlayerId = playerId;
            GameCards = new List<int>();
            PlayerStatus = PlayerStatus.OnPlaying;
        }

        public PlayerStatus PlayerStatus { get; set; }

        public PlayerId PlayerId { get; private set; }

        public List<int> GameCards { get; private set; }

        //private Stack<Card> playerCards;

        public bool AddCards(Stack<Card> cards)
        {
            GameCards.Add((int)cards.Pop().CardId); //Card Stack에서 한 장씩 뽑아서 넣는다(int)

            if (cards.Count == 0)
                return true;

            return false;
        }

        public List<Card> CandidatedCards { get; private set; }

        /// <summary>
        /// Player가 방에 들어갔을 때 추리할 수 있는 카드를 가져온다.
        /// </summary>
        /// <param name="roomId">해당 턴의 플레이어가 들어간 방의 Id</param>
        /// <returns></returns>
        public List<Card> GetCandidatedCards(RoomId roomId, List<Card> cards)
        {
            var room = RoomFactory.Instance[roomId];

            List<Card> candidatedCards = new List<Card>();
            candidatedCards.AddRange(cards.FindAll(x => x.CardType == CardType.Suspect || x.CardType == CardType.Weapon));
            candidatedCards.AddRange(cards.FindAll(x => x.CardType == CardType.Place && (int)x.CardId == (int)room.RoomId));

            return CandidatedCards = candidatedCards;
        }

        /// <summary>
        /// 플레이어가 이동할 칸을 선택했을 때 호출되는 메소드
        /// </summary>
        public void Move()
        {
            throw new NotImplementedException();
        }

        public int TurnNo { get; internal set; }

        /// <summary>
        /// 해당 턴의 플레이어가 자신의 턴을 끝낼 때 호출되는 메소드
        /// </summary>
        public void EndTurn()
        {
            //GameDirector에게 자신의 턴이 끝났다고 알린다
            GameDirector.Instance.EndTurn();
            throw new NotImplementedException();
        }

        /// <summary>
        /// 추리한 카드에 대해서 반박을 할 플레이어를 턴의 반대 방향으로 돌아가면서 가져온다.
        /// 반박할 순서로 정렬된 플레이어의 리스트에서 해당 반박 플레이어의 인덱스를 통해서 플레이어를 가져온다.
        /// </summary>
        /// <param name="turn">반박할 플레이어의 인덱스</param>
        /// <returns></returns>
        public Player GetNextDisprovingPlayer(int turn)
        {
            //TODO 다음 반박자를 가져오도록 인덱스 바꾸는 로직 필요
            int index = 1;
            return PlayerMaker.Instance.SetDisprovingPlayersTurn(this.TurnNo)[turn];
            index++;


        }
    }
}
