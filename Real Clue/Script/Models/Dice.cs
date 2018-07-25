using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealClue
{
    class Dice
    {
        public static int Roll()
        {
            Random random = new Random();

            int blackDice = random.Next(1, 7);
            int whiteDice = random.Next(1, 7);

            int sum = blackDice + whiteDice;

            Console.WriteLine("주사위의 합은 {0}", Dice.Roll());

            return sum;

        }
    }
}
