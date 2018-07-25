
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    public static MainCanvasManager Instance;
    
    [SerializeField] private LobbyCanvas _lobbyCanvas;

    public LobbyCanvas LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }

    private static System.Random random;

    private void Awake()
    {
        Instance = this;
    }

    public void OnClickMakeRoom()
    {
        GameObject roomSettingPanel = transform.Find("RoomSettingPanel").gameObject;
        GameObject lobbyUI = transform.Find("LobbyUI").gameObject;
        roomSettingPanel.SetActive(true);
        lobbyUI.SetActive(false);
    }

    #region QuickPlayButton 부분
    //TODO master client로 지정되지 않는 문제 해결 필요
    public void OnClickQuickPlayButton()
    {
        if (PhotonNetwork.connected)
            PhotonNetwork.JoinRandomRoom();
    }

    private void OnPhotonRandomJoinFailed()
    {
        if (random == null)
            random = new System.Random();

        string roomName = "Random_Room" + random.Next(100);

        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 6 };

        if (PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default))
        {
            print(roomName + " Successfully Created.");
        }
        else
        {
            print("Create Room failed to sent.");
        }
    }
    #endregion
}