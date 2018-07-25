#region
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace RealClue
{
    public class Map
    {
        #region singleton
        private static Map _instance;

        public static Map Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Map();

                return _instance;
            }
        }

        private Map()
        {
        }

        static Map()
        {
            _directions = (Direction[]) Enum.GetValues(typeof(Direction));
        }
        #endregion

        public void Initialize()
        {
            InitializeCore(10, 15);
            
            // 룸 1
            ChangeTile(Tile.Block, 10, 11, 13, 14, 110, 111);
            ChangeTile(Tile.Room, 12);
            
            // 룸 2
            ChangeTile(Tile.Block, 114, 214, 414);
            ChangeTile(Tile.Room, 314);
            
            // 룸 3
            ChangeTile(Tile.Block, 514, 614, 714, 814, 613, 713,813,512,612,712,812);
            ChangeTile(Tile.Room, 513);
            
            // 룸 4
            ChangeTile(Tile.Block, 914,913,912,910,909,809);
            ChangeTile(Tile.Room, 911);
            
            // 룸 5
            ChangeTile(Tile.Block, 805,806,808,905,906,907,908);
            ChangeTile(Tile.Room, 807);
            
            // 룸 6
            ChangeTile(Tile.Block, 900,901,902,904);
            ChangeTile(Tile.Room, 903);
            
            // 룸 7
            ChangeTile(Tile.Block, 100,200,300,500,600,700,800,101,201,701,801);
            ChangeTile(Tile.Room, 400);
            
            // 룸 8
            ChangeTile(Tile.Block, 0,1,3);
            ChangeTile(Tile.Room, 2);
            
            // 룸 9
            ChangeTile(Tile.Block, 4,5,7,8,9);
            ChangeTile(Tile.Room, 6);
            
            // 클루 룸
            ChangeTile(Tile.Block, 305, 307, 405, 406, 407, 506, 605, 607);
            ChangeTile(Tile.Room, 306, 505, 507, 606);
        }

        private void InitializeCore(int maxX = 10, int maxY = 15)
        {
            MaxX = maxX;
            MaxY = maxY;

            _points = new Point[MaxX, MaxY];
            
            for (int i = 0; i < MaxX; i++)
            for (int j = 0; j < MaxY; j++)
                _points[i, j] = new Point(i, j);
        }

        public void InitializeForTest()
        {
            InitializeCore(3, 3);
            
            ChangeTile(Tile.Room, 2);
            ChangeTile(Tile.Block, 100);
        }

        private void ChangeTile(Tile tile, params int[] values)
        {
            foreach (var value in values)
                _points[value / Point.Delimeter, value % Point.Delimeter].Tile = tile;
        }

        public static int MaxX { get; set; }
        public static int MaxY { get; set; }

        private Point[,] _points;
        private static Direction[] _directions;

        public Point this[int x, int y] => _points[x, y];
        public Point this[int value] => _points[value / Point.Delimeter, value % Point.Delimeter];
        
        public List<Point> GetMovableArea(int x, int y, int dice)
        {
            return GetMovableArea(Instance[x, y], dice);
        }

        public List<Point> GetMovableArea(Point point, int dice)
        {
            for (int i = 0; i < MaxX; i++)
            for (int j = 0; j < MaxY; j++)
                _points[i, j].Mark = (char) 0;
            _points[point.X, point.Y].Mark = 'S';
            
            HashSet<Point> points = new HashSet<Point>();

            MoveToNext(point, dice, points, 1);

            var pointsOfPlayers = PlayerMaker.Instance.PointsOfPlayrs;

            var pointsToReturn = points
                .Except(new HashSet<Point> {point}) // 자기 자신은 제거
                .Except(pointsOfPlayers) // 플레이어가 있는 점은 제거
                .Where(x => x.Distance == dice || x.Tile == Tile.Room) // 거리가 주사위 만큼 떨어져있어야 함 (방은 예외)
                .OrderBy(x => x.Value)
                .ToList();

            foreach (var p in pointsToReturn)
                _points[p.X, p.Y].Mark = 'O';
            
            return pointsToReturn;
        }

        private void MoveToNext(Point point, int dice, HashSet<Point> points, int distance)
        {
            foreach (Direction direction in _directions)
            {
                Point neighbor = point.GetNeighbor(direction);

                if (neighbor == Point.Invlid)
                    continue;

                if (neighbor.Tile == Tile.Block)
                    continue;

                if (points.Contains(neighbor))
                    continue;

                neighbor.Distance = distance;
                points.Add(neighbor);

                for (int i = 0; i < distance; i++)
                    Console.Write("-");
                Console.WriteLine(neighbor);

                if (dice > distance)
                    MoveToNext(neighbor, dice, points, distance + 1);
            }
        }
    }
}