using System;
using Microsoft.Win32.SafeHandles;

namespace RealClue
{
    public enum Tile
    {
        Empty = 0,
        Block,
        Room
    }

    public enum Direction
    {
        Up,
        Right,
        Down, 
        Left
    }
    
    public struct Point
    {
        public const int Delimeter = 100;

        internal Point(int x, int y, Tile tile = Tile.Empty)
        {
            X = x;
            Y = y;
            Tile = tile;
            Distance = 0;
            _mark = (char) 0;
        }

        public static implicit operator Point(int value)
        {
            return Map.Instance[value];
        }

        public int X { get;  }
        
        public int Y { get;  }
        
        public Tile Tile { get; internal set; }

        /// <summary>
        /// 특정 점으로부터의 거리
        /// </summary>
        public int Distance { get; internal set; }

        private char _mark;

        public int Value => X * Delimeter + Y;

        public static Point Invlid = new Point(-1, -1);

        public Point GetNeighbor(Direction direction)
        {
                switch (direction)
                {
                    case Direction.Up:
                        return Up;
                    case Direction.Down:
                        return Down;
                    case Direction.Left:
                        return Left;
                    case Direction.Right:
                        return Right;
                    default:
                        throw new Exception();
                }        
        }

        #region neighbors
        public Point Up
        {
            get
            {
                if (Y == Map.MaxY - 1)
                    return Invlid;
                else
                    return Map.Instance[X, Y + 1];
            }
        }

        public Point Down
        {
            get
            {
                if (Y == 0)
                    return Invlid;
                else
                    return Map.Instance[X, Y - 1];
            }
        }

        public Point Left
        {
            get
            {
                if (X == 0)
                    return Invlid;
                else
                    return Map.Instance[X - 1, Y];
            }
        }

        public Point Right
        {
            get
            {
                if (X == Map.MaxX - 1)
                    return Invlid;
                else
                    return Map.Instance[X + 1, Y];
            }
        }
        #endregion

        #region equals
        public override bool Equals(object obj)
        {
            Point other = (Point) obj;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
        
        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
        #endregion

        public override string ToString()
        {
            return $"({X:X},{Y:X}) [{Distance}]";
        }

        internal char Mark
        {
            get
            {
                if (_mark != 0)
                    return _mark;
                
                switch (Tile)
                {
                        case Tile.Empty:
                            return ' ';
                        case Tile.Block:
                            return 'X';
                        case Tile.Room:
                            return 'R';
                        default:
                            throw new Exception();
                }
            }
            set { _mark = value; }
        }
    }
}