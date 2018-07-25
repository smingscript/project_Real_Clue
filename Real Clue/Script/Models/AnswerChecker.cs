using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealClue
{
    public class AnswerChecker
    {
        public Player GetWinner(Player player)
        {
            var answerList = Deck.Instance.AnswerCards.OrderBy(x => x.CardType).ToList();
            var candidateList = player.CandidatedCards.OrderBy(x => x.CardType).ToList();

            for (int i = 0; i < 3; i++)
            {
                if (answerList[i] != candidateList[i])
                    MakeLoser(player);
                    break;
            }
            return player;
        }
        
        public void MakeLoser(Player player)
        {
            player.PlayerStatus = PlayerStatus.Loser;
        }
    }
}