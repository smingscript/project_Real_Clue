using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateRoom : MonoBehaviour
{
    [SerializeField] 
    private Text _roomName;

    private Text RoomName
    {
        get { return _roomName; }
    }

    public void OnClick_CreateRoom()
    {
        //TODO 방을 만들 때 조건들 변경할 부분
        RoomOptions roomOptions = new RoomOptions() { IsVisible =  true, IsOpen = true, MaxPlayers = 4 };
        
        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
        {
            print("Create Room Successfully sent.");
        }
        else
        {
            print("Create Room failed to sent.");
        }
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("Create Room Failed: " + codeAndMessage[1]);
    }

    private void OnCreatedRoom()
    {
        print(PhotonNetwork.room.Name + " created successfully");
        PhotonNetwork.LoadLevel("WaitingRoom");
    }
}
