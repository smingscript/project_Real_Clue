using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealClue
{
    public class Deck
    {
        #region singleton
        private static Deck _instance;
 
        public static Deck Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Deck();
                return _instance;
            }
        }

        private Deck()
        {
            Initialize();
        }
        #endregion

        public List<Card> Cards { get; private set; }

        public Stack<Card> PlayerCards { get; private set; }

        public List<Card> AnswerCards { get; private set; }

        public void Initialize()
        {
            List<Card> cardList = new List<Card>();

            cardList.Add(new Card(CardId.Yard, CardType.Place));
            cardList.Add(new Card(CardId.GameRoom, CardType.Place));
            cardList.Add(new Card(CardId.Library, CardType.Place));
            cardList.Add(new Card(CardId.DiningRoom, CardType.Place));
            cardList.Add(new Card(CardId.Garage, CardType.Place));
            cardList.Add(new Card(CardId.Hall, CardType.Place));
            cardList.Add(new Card(CardId.Kitchen, CardType.Place));
            cardList.Add(new Card(CardId.BedRoom, CardType.Place));
            cardList.Add(new Card(CardId.BathRoom, CardType.Place));
            cardList.Add(new Card(CardId.Scarlet, CardType.Suspect));
            cardList.Add(new Card(CardId.Mustard, CardType.Suspect));
            cardList.Add(new Card(CardId.White, CardType.Suspect));
            cardList.Add(new Card(CardId.Green, CardType.Suspect));
            cardList.Add(new Card(CardId.Peacock, CardType.Suspect));
            cardList.Add(new Card(CardId.Plum, CardType.Suspect));
            cardList.Add(new Card(CardId.Knife, CardType.Weapon));
            cardList.Add(new Card(CardId.Pipe, CardType.Weapon));
            cardList.Add(new Card(CardId.Revolver, CardType.Weapon));
            cardList.Add(new Card(CardId.Rope, CardType.Weapon));
            cardList.Add(new Card(CardId.CandleStick, CardType.Weapon));
            cardList.Add(new Card(CardId.Wrench, CardType.Weapon));

            Cards = new List<Card>(cardList.OrderBy(x => Guid.NewGuid()));
        }

        /// <summary>
        /// 최종 정답 카드에 들어갈 카드 3장을 뽑는다.(CardType별로 한 장씩)
        /// </summary>
        private void GetAnswerCards()
        {
            AnswerCards = new List<Card>();
            AnswerCards.AddRange(Cards.GroupBy(x => x.CardType).Select(x => x.First()));
        }

        /// <summary>
        /// 정답카드 3장을 제외한 18장의 카드 중에서 Player의 카드를 설정한다.
        /// </summary>
        private void GetPlayerCards()
        {
            GetAnswerCards();
            PlayerCards = new Stack<Card>(Cards.Except(AnswerCards));
        }

        public List<Player> Draw()
        {
            GetPlayerCards();
            var playerList = PlayerMaker.Instance.Players;

            //i번을 돌면서 플레이어의 GameCards에 카드를 한장씩 분배한다.
            //for (int i = 1; i <= 3; i++)
            //{
            //    foreach (Player player in _playerList)
            //    {
            //        //Player내에서 GameCards에 카드를 분배한다
            //        player.AddCards(PlayerCards);
            //    }
            //}
            bool isFinished = false;
            while(!isFinished)
            {
                foreach (Player player in playerList)
                {
                    if (player.AddCards(PlayerCards))
                    {
                        isFinished = true;
                        break;
                    }
                        
                }
            }
            

            return playerList;
        }
    }
}
