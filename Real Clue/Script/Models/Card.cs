using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealClue
{
    public class Card
    {
        public Card(CardId cardId, CardType cardType)
        {
            CardId = cardId;
            CardType = cardType;
        }

        public CardId CardId { get; private set; }
        public CardType CardType { get; private set; }
    }
}
