using RealClue;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingRoomPlayerNetwork : Photon.MonoBehaviour, IPunObservable
{
    public string PlayerName { get; private set; }
    public GameObject StartButton;
    public GameObject ReadyButton;
    public GameObject UnreadyButton;

    private PhotonView PhotonView;
    private int _playersInGame = 0;
    private ExitGames.Client.Photon.Hashtable _playerCustomProperties = new ExitGames.Client.Photon.Hashtable();

    private GameObject[] _spawnPoints;
    private List<PhotonPlayer> _readyPlayerslist;
    private Dictionary<PhotonPlayer, int> _playerJoinedOrderList;
    private Dictionary<int, GameObject> _playerPrefebsList;

    private void Awake()
    {
        PlayerName = PhotonNetwork.playerName;

        _readyPlayerslist = new List<PhotonPlayer>();

        //player별로 prefeb을 destroy하기 위해 필요
        _playerJoinedOrderList = new Dictionary<PhotonPlayer, int>();
        _playerPrefebsList = new Dictionary<int, GameObject>();

        PhotonView = GetComponent<PhotonView>();

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;
    }

    #region Check for Ready to Start
    private void Update()
    {
        if(PhotonNetwork.connected)
        {
            if (_playersInGame > 1)
                CheckPlayerReady();
            else
                StartButton.GetComponent<Button>().interactable = false;
        }
        
    }

    private bool CheckPlayerReady()
    {
        bool playersReady = false;

        if (_playersInGame == (_readyPlayerslist.Count + 1))
            playersReady = true;

        if (playersReady)
        {
            StartButton.GetComponent<Button>().interactable = true;

            //TODO 턴인 플레이어의 StartGame()을 활성화시킨다.
            //players[iActivePlayer].StartGame();
        }
        else
            StartButton.GetComponent<Button>().interactable = false;

        return playersReady;
    }

    #region Ready Button Event
    /// <summary>
    /// called when remote players click the ready button
    /// </summary>
    public void OnReadyButtonClicked()
    {
        PhotonView.RPC("RPC_ReadyPlayer", PhotonTargets.All);

        ReadyButton.SetActive(false);
        UnreadyButton.SetActive(true);
    }

    public void OnUnreadyButtonClicked()
    {
        PhotonView.RPC("RPC_UnreadyPlayer", PhotonTargets.All);

        ReadyButton.SetActive(true);
        UnreadyButton.SetActive(false);
    }

    [PunRPC]
    private void RPC_ReadyPlayer()
    {
        _readyPlayerslist.Add(PhotonNetwork.player);
    }

    [PunRPC]
    private void RPC_UnreadyPlayer()
    {
        _readyPlayerslist.Remove(PhotonNetwork.player);
    }
    #endregion

    #endregion

    #region Player Joined Room
    /// <summary>
    /// Called by photon whenever you join a room. Master Client/Others
    /// </summary>
    private void OnJoinedRoom()
    {
        print("Entered Waiting Room!");

        SetRandomCharacter();

        if (PhotonNetwork.isMasterClient)
        {
            StartButton.SetActive(true);
            StartButton.GetComponent<Button>().interactable = false;
            MasterLoadedGame(PhotonNetwork.player);
        }
        else
        {
            print(PhotonNetwork.player + " entered.");
            ReadyButton.SetActive(true);
            NonMasterLoadedGame(PhotonNetwork.player);
        }
    }

    private void SetRandomCharacter()
    {
        if (PhotonNetwork.connected)
        {
            int maxPlayer = PhotonNetwork.room.MaxPlayers;

            _playerCustomProperties["Character"] = PlayerMaker.Instance.SetRandomPlayer(maxPlayer).PlayerId.ToString();
            PhotonNetwork.player.SetCustomProperties(_playerCustomProperties);
        }
    }

    private void MasterLoadedGame(PhotonPlayer photonPlayer)
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, photonPlayer);
    }

    private void NonMasterLoadedGame(PhotonPlayer photonPlayer)
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, photonPlayer);
    }

    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        _playersInGame++;

        _playerJoinedOrderList[photonPlayer] = _playersInGame;

        print("Player entered the room: current players: " + (_playersInGame));

        PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.MasterClient, photonPlayer);
    }

    [PunRPC]
    private void RPC_CreatePlayer(PhotonPlayer photonPlayer)
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GameObject spawnPoint = null;

        string playerCharacter = (string)photonPlayer.CustomProperties["Character"];

        foreach (var point in _spawnPoints)
        {
            if (point.name.Contains(playerCharacter))
                spawnPoint = point;
        }

        GameObject obj = PhotonNetwork.Instantiate(Path.Combine("PlayerPrefab", playerCharacter), spawnPoint.transform.position,
        spawnPoint.transform.rotation, 0);
        _playerPrefebsList[_playersInGame] = obj;
    }
    #endregion


    #region Player Disconnected
    public void OnClickLeaveRoom()
    {
        PhotonView.RPC("RPC_PlayerLeftRoom", PhotonTargets.MasterClient, PhotonNetwork.player);
        PhotonNetwork.LeaveRoom();
    }

    private void OnLeftRoom()
    {
        SceneManager.LoadScene(1);
    }

    [PunRPC]
    private void RPC_PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        PhotonView.RPC("RPC_UnreadyPlayer", PhotonTargets.All);
        PhotonView.RPC("RPC_DestroyPlayer", PhotonTargets.MasterClient, photonPlayer);
        _playersInGame--;
        print(photonPlayer + " left the room! " + _playersInGame + " players left");
    }

    [PunRPC]
    private void RPC_DestroyPlayer(PhotonPlayer photonPlayer)
    {
        print("Destroy" + photonPlayer.NickName + ": " + _playerPrefebsList[_playerJoinedOrderList[photonPlayer]]);
        PhotonNetwork.Destroy(_playerPrefebsList[_playerJoinedOrderList[photonPlayer]]);
    }

    //Called by photon whenever the master client is swithced.
    private void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        PlayerMaker.Instance.randomNumbers.Clear();

        //clearing _playerCustomProperties
        //_playerCustomProperties["Character"] = null;

        PhotonNetwork.LeaveRoom();
    }
    #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (UseTransformView)
        //    return;

        //if (stream.isWriting)
        //{
        //    stream.SendNext(transform.position);
        //    stream.SendNext(transform.rotation);
        //}
        //else
        //{
        //    TargetPosition = (Vector3)stream.ReceiveNext();
        //    TargetRotation = (Quaternion)stream.ReceiveNext();
        //}
    }
}
