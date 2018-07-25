using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomLayoutGroup : MonoBehaviour
{
    
    [SerializeField] 
    private GameObject _roomListingPrefab;

    private GameObject RoomListingPrefab
    {
        get { return _roomListingPrefab; }
    }

    private List<RoomListing> _roomListingPanel = new List<RoomListing>();

    public List<RoomListing> RoomListingPanel
    {
        get { return _roomListingPanel; }
    }

    private int _roomListingIndex = 0;

    private RoomInfo[] rooms;

    private Text noRoomText;

    public static RoomLayoutGroup Instance;

    [SerializeField] private LobbyCanvas _lobbyCanvas;

    public LobbyCanvas LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }

    private void Start()
    {
        noRoomText = GetComponentInChildren<Text>();
        Instance = this;
        RefreshRoomList();
    }

    //TODO 방이 생성이 되면 자동으로 새로고침이 되는 기능 필요
    private void OnReceivedRoomListUpdate()
    {
        RoomListUpdate();
    }

    private void RoomListUpdate()
    {
        if (rooms.Length == 0)
        {
            noRoomText.text = "No rooms at the moment.";

            return;
        }

        RoomReceived(rooms[_roomListingIndex]);
    }

    public void RefreshRoomList()
    {
        //새로고침을 눌렀을 떄 잠시 띄워야 한다...
        //TODO 방이 뜨면 비활성되어야 한다
        noRoomText.text = "Loading...";

        rooms = PhotonNetwork.GetRoomList();

        RoomListUpdate();
    }

    private void RoomReceived(RoomInfo room)
    {
        noRoomText.text = "";

        GameObject roomListingObj = Instantiate(_roomListingPrefab);
        roomListingObj.transform.SetParent(transform, false);

        RoomListing roomListing = roomListingObj.GetComponent<RoomListing>();
        roomListing.SetRoomNameText(room.Name);
        roomListing.SetNumberOfOccupants(room);
        _roomListingPanel.Add(roomListing);

        ClearRoomListingPanel();
    }

    private void ClearRoomListingPanel()
    {
        //TODO 다른 Room Listing으로 바뀔 때마다 전의 Room Listing을 지워야 한다
        if (_roomListingPanel.Count > 1)
        {
            Destroy(_roomListingPanel[0].gameObject);
            _roomListingPanel.RemoveAt(0);
        }
        
    }

    //TODO 완전히 리스트를 지우고 새로 불러오는 것
    void ClearRoomList()
    {
        //for (int i = 0; i < roomList.Count; i++)
        //{
        //    Destroy(roomList[i]);
        //}

        //roomList.Clear();
    }


    //인덱스의 순서를 바꾼다.
    public void OnNextButtonClicked()
    {
        if (rooms.Length == 0)
            return;

        _roomListingIndex++;

        if (_roomListingIndex >= rooms.Length)
            _roomListingIndex = 0;

        RoomListUpdate();
    }

    public void OnPrevButtonClicked()
    {
        if (rooms.Length == 0)
            return;

        _roomListingIndex--;

        if (_roomListingIndex < 0)
            _roomListingIndex = rooms.Length - 1;

        RoomListUpdate();
    }
}
