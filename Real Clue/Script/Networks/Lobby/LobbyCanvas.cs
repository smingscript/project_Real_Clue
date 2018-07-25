using UnityEngine;
using UnityEngine.UI;
using RealClue;

public class LobbyCanvas : MonoBehaviour
{
    [SerializeField] private RoomLayoutGroup _roomLayoutGroup;
    public RoomLayoutGroup RoomLayoutGroup
    {
        get { return _roomLayoutGroup; }
    }

    /// <summary>
    /// Join 버튼을 누를 떄 발생하는 이벤트
    /// </summary>
    /// <param name="roomName"></param>
    public void OnClickJoinRoom(string roomName)
    {
        print("joining the room");
        
        if (!PhotonNetwork.JoinRoom(roomName))
            print("Join room failed.");
    }

    
}
