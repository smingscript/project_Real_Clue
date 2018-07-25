using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RealClue
{
//    class AreaCalculator : WorldMap
//    {
//        public void GetMovablePoints()
//        {
//            
//            //주사위 생성
//            Random random = new Random();
//
//            int blackDice = random.Next(1, 7);
//            int whiteDice = random.Next(1, 7);
//            //두 주사위의 합
//            int diceValue = blackDice + whiteDice;
//            Console.WriteLine($"{blackDice}, {whiteDice}");
//            Console.WriteLine("두 주사위 합은: {0}", diceValue);
//            //이동가능범위
//            int V = diceValue / 2;
//            int lotation = 0;
//            List<int> L = new List<int>();
//            ////이동가능범위 공식계산
//            for (int i = 0; i <= V; i++)
//            {
//                if (diceValue - 2 * i > 0)
//                {
//                    lotation = diceValue - 2 * i;
//                    L.Add(lotation);
//                    //if (Equals(L[i],map.GetValue(Math.Abs(Point.Y), Math.Abs(Point.X))))
//                        foreach (Point map in Map)
//                        {
//                            if (L[i] == Math.Abs(Point.X) + Math.Abs(Point.Y))
//                                Console.WriteLine("Ok");
//
//                        }
//                }
//                else
//                {
//                    break;
//                }
//            }
//
//            Console.WriteLine("");
//            Console.WriteLine("List");
//            foreach (int lotationList in L)
//            {
//                Console.WriteLine(lotationList);
//            }
//            Console.WriteLine($"갯수 : {L.Count}개");
//            Console.WriteLine("");
//        }
//
//        private List<Point> ExceptRooms(List<Point> movablePoints, WorldMap map)
//        {
//            throw new Exception();
//        }
//    }
}