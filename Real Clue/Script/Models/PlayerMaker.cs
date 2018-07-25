using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealClue
{
    //[Serializable]
    public class PlayerMaker
    {
        #region singleton

        private static Random random;

        private static PlayerMaker _instance;

        public static PlayerMaker Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new PlayerMaker();
                return _instance;
            }
        }

        private PlayerMaker()
        {
            randomNumbers = new List<int>();
            Players = new List<Player>();
            InitialPlayers();
        }

        #endregion

        public List<Player> Players { get; private set; }
        //public List<Player> DisprovingPlayersTurn { get; private set; }

        private void InitialPlayers()
        {
            List<Player> playerList = new List<Player>();

            playerList.Add(new Player(PlayerId.Scarlet));
            playerList.Add(new Player(PlayerId.Mustard));
            playerList.Add(new Player(PlayerId.White));
            playerList.Add(new Player(PlayerId.Green));
            playerList.Add(new Player(PlayerId.Peacock));
            playerList.Add(new Player(PlayerId.Plum));

            Players = playerList;
        }
        
        public void SetPlayers(List<string> playerInString)
        {
            List<Player> playerList = new List<Player>();

            foreach (string playerString in playerInString)
            {
                playerList.Add(new Player((PlayerId)Enum.Parse(typeof(PlayerId), playerString)));
            }

            Players = playerList;
        }

        public List<int> randomNumbers;

        /// <summary>
        /// 캐릭터 프리팹을 랜덤으로 인스턴스화하도록 클라이언트에 할당한다.
        /// </summary>
        /// <param name="maxPlayers">방의 최대 인원 내에서 랜덤한 인덱스를 만든다.</param>
        /// <returns></returns>
        public Player SetRandomPlayer(int maxPlayers)
        {
            if (random == null)
                random = new Random();

            int number = random.Next(6);

            while (randomNumbers.Contains(number))
            {
                number = random.Next(6);
                //randomNumbers.Add(number);
            }

            randomNumbers.Add(number);

            return Players[number];
        }

        /// <summary>
        /// 추리를 반박할 순서에 따라 정렬된 플레이어의 리스트를 반환하는 메소드
        /// </summary>
        /// <param name="playerTurn">지정된 순서에서 현재 턴인 플레이어의 인덱스</param>
        /// <returns></returns>
        public List<Player> SetDisprovingPlayersTurn(int playerTurn)
        {
            return Players.Take(playerTurn).Reverse().Concat(Players.Skip(playerTurn).Reverse()).ToList();
        }

        public bool IsPlayerOn(Point point)
        {
            return false;
        }

        public HashSet<Point> PointsOfPlayrs { get; } = new HashSet<Point>();

        public void AddPlayerLocation(Point point)
        {
            PointsOfPlayrs.Add(point);
        }
    }
}
