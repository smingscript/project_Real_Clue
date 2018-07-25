using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Photon.PunBehaviour
{
    #region outlets

	public PhotonLogLevel LogLevel = PhotonLogLevel.Informational;
	[Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
	public byte MaxPlayersPerRoom = 4;
	
	[Tooltip("The Ui Panel to let the user enter name, connect and play")]
	public GameObject controlPanel;
	
	[Tooltip("The UI Label to inform the user that the connection is in progress")]
	public GameObject progressLabel;
	
    #endregion

    #region fields

	private string _gameVersion = "1";
	
	bool isConnecting;
	
    #endregion

    #region messages

	void Awake()
	{
		PhotonNetwork.logLevel = LogLevel;

		//Critical: we don't join the lobby. There is no need to join a lobby to get the list of rooms.
		PhotonNetwork.autoJoinLobby = true;
		
		//Critical: this make sure we can use PhotnNetwork.LoadLevel() on the master client
		//and all clinets in the same room sync their level automatically
		PhotonNetwork.automaticallySyncScene = true;

        PhotonNetwork.autoCleanUpPlayerObjects = true;
	}

	void Start()
	{
		progressLabel.SetActive(false);
		controlPanel.SetActive(true);
	}
	
    #endregion	

    #region methods

	/// <summary>
	/// Start the connection process. 
	/// - If already connected, we attempt joining a random room
	/// - if not yet connected, Connect this application instance to Photon Cloud Network
	/// </summary>
	public void Connect()
	{
		isConnecting = true;
		
		progressLabel.SetActive(true);
		controlPanel.SetActive(false);
		
		if (PhotonNetwork.connected)
		{
			// #Critical we need at this point to attempt joining a Random Room.
			// If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
			PhotonNetwork.JoinLobby();
		}
		else
		{
			// #Critical, we must first and foremost connect to Photon Online Server.
			PhotonNetwork.ConnectUsingSettings(_gameVersion);
		}
	}
	
    #endregion

	#region Photon.PunBehaviour Callbacks

	public override void OnConnectedToMaster()
	{
		if (isConnecting)
		{
            //PhotonNetwork.JoinRandomRoom();
            PhotonNetwork.ConnectUsingSettings("v1.0");
            Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
		}
		
	}

	public void OnFailedConnectToPhoton()
	{
		progressLabel.SetActive(false);
		controlPanel.SetActive(true);
		Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
	}

    //public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
    //{
    //	Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

    //	// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
    //	PhotonNetwork.CreateRoom(null, new RoomOptions() { maxPlayers = MaxPlayersPerRoom }, null);
    //}

    public override void OnJoinedLobby()
	{
        PhotonNetwork.LoadLevel("Lobby2");
        // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.automaticallySyncScene to sync our instance scene.
        //if (PhotonNetwork.room.playerCount == 1)
        //{
        //	Debug.Log("We load the 'Room for 1' ");

        //	// #Critical
        //	// Load the Room Level. 
        //	PhotonNetwork.LoadLevel("Lobby2");
        //}

        Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");


    }

	#endregion
}
