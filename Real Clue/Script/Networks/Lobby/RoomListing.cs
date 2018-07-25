using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField] 
    private Text _roomNameText;
    public Text RoomNameText
    {
        get { return _roomNameText; }
    }

    [SerializeField]
    private Text _roomPlayersNumberText;

    private Button button;
    public Text RoomPlayersNumberText
    {
        get { return _roomPlayersNumberText; }
    }

    public string RoomName { get; private set; }
    public string PlayersNumber { get; private set; }
    
    //public bool Updated { get; set; }

    private void Start()
    {
        GameObject lobbyCanvasObj = MainCanvasManager.Instance.LobbyCanvas.gameObject;
        if (lobbyCanvasObj == null)
            return;

        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();

        button = transform.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(RoomNameText.text));
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }

    public void SetNumberOfOccupants(RoomInfo room)
    {
        PlayersNumber = string.Format("{0} / {1}", room.PlayerCount, room.MaxPlayers);
        RoomPlayersNumberText.text = PlayersNumber;
    }
}
