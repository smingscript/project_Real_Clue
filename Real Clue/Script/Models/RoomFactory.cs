using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealClue
{
    public class RoomFactory
    {
        #region
        private static RoomFactory _instance;

        public static RoomFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RoomFactory();
                return _instance;
            }
        }

        private RoomFactory()
        {
            var list = new List<Room>();
            list.Add(new Room(RoomId.Yard));
            list.Add(new Room(RoomId.GameRoom));
            list.Add(new Room(RoomId.Library));
            list.Add(new Room(RoomId.DiningRoom));
            list.Add(new Room(RoomId.Garage));
            list.Add(new Room(RoomId.Hall));
            list.Add(new Room(RoomId.Kitchen));
            list.Add(new Room(RoomId.BedRoom));
            list.Add(new Room(RoomId.BathRoom));

            _rooms = list.ToDictionary(x => x.RoomId, x => x);
        }
        #endregion

        /// <summary>
        /// Key(RoomId), Value(Room)을 한 쌍으로 갖는 Dictionary
        /// </summary>
        private Dictionary<RoomId, Room> _rooms { get; set; }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="roomId">열거형 RoomId(9개)</param>
        /// <returns></returns>
        public Room this[RoomId roomId]
        {
            get { return _rooms[roomId]; }
        }
    }
}
