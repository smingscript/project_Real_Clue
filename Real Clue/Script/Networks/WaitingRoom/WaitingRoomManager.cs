using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace RealClue
{
    public class WaitingRoomManager : Photon.PunBehaviour, IPunObservable
    {

        //public GameObject mainPlayer;
        //public WaitingRoomManager Instance;
        public string PlayerName { get; private set; }
        private PhotonView PhotonView;
        private int PlayersInGame = 0;
        private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
        private GameObject[] spawnPoints;

        #region Photon Messages

        private void Start()
        {
            //TODO 방장이 방을 만들면, Model에서 방의 인원수에 맞게 순서리스트를 생성한다.
            //
            //PhotonView = GetComponent<PhotonView>();
            //if(Instance == null)

            //DontDestroyOnLoad(this);

            PhotonView = GetComponent<PhotonView>();
            PlayerName = PhotonNetwork.playerName;

            PhotonNetwork.sendRate = 60;
            PhotonNetwork.sendRateOnSerialize = 30;

            //SceneManager.sceneLoaded += OnSceneFinishedLoading;

        }

        private void Update()
        {
            //TODO 시작 Countdown
            //if (isTurn)
            //{
            //    time -= Time.deltaTime;
            //    if (time <= 0)
            //    {
            //        NetworkManager.Instance.AlterTurns();
            //    }
            //}
        }

        public void onStartButtonClicked()
        {
            //TODO Countdown 시작한다
            print("Start the game");


            SceneManager.LoadScene("Main");
        }


        #region Depricated
        //void OnPlayerDisconnected(NetworkPlayer player)
        //{
        //    Debug.Log("Clean up after player " + player);
        //    Network.RemoveRPCs(player);
        //    Network.DestroyPlayerObjects(player);
        //}


        //private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
        //{
        //    //if (scene.name == "WaitingRoom")
        //    //    SpawnPlayer();
        //}

        //private void SpawnPlayer()
        //{
        //    GameObject[] spawnPoinsts = GameObject.FindGameObjectsWithTag("SpawnPoint");

        //    if(PhotonNetwork.playerList.Length == 1)
        //        PhotonNetwork.Instantiate(mainPlayer.name, spawnPoinsts[0].transform.position,
        //        mainPlayer.transform.rotation, 0);
        //}

        #region depricated loadScene
        //void LoadArena()
        //{
        //    if ( ! PhotonNetwork.isMasterClient ) 
        //    {
        //        Debug.LogError( "PhotonNetwork : Trying to Load a level but we are not the master Client" );
        //    }
        //    Debug.Log( "PhotonNetwork : Loading Level : " + PhotonNetwork.room.playerCount );

        //    if(PhotonNetwork.room.playerCount < 2)
        //     PhotonNetwork.LoadLevel("Lobby2");
        //}



        public override void OnPhotonPlayerConnected(PhotonPlayer other)
        {
            //SetRandomCharacter();

            //Debug.Log("OnPhotonPlayerConnected() " + other.name); // not seen if you're the player connecting

            ////if (PhotonNetwork.isMasterClient)
            ////{
            ////    Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected
            ////}

            //if (PhotonNetwork.isMasterClient)
            //    MasterLoadedGame();
            //else
            //    NonMasterLoadedGame();
        }

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

        //public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
        //{
        //    Debug.Log("OnPhotonPlayerDisconnected() " + other.name); // seen when other disconnects

        //    if (PhotonNetwork.isMasterClient)
        //    {
        //        Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


        //    }
        //}
        #endregion

        /*
        #region PlayerNetwork Part
        public static PlayerNetwork Instance;

        public string PlayerName { get; private set; }
        private PhotonView PhotonView;
        private int PlayersInGame = 0;
        private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
        private PlayerMovement CurrentPlayer;

        private GameObject[] spawnPoints;

        // Use this for initialization
        private void Start()
        {

            if (Instance == null)
                Instance = this;

            //DontDestroyOnLoad(this);

            PhotonView = GetComponent<PhotonView>();
            PlayerName = PhotonNetwork.playerName;

            PhotonNetwork.sendRate = 60;
            PhotonNetwork.sendRateOnSerialize = 30;

            SceneManager.sceneLoaded += OnSceneFinishedLoading;

        }

        /// <summary>
        /// WaitingRoom과 MainGameRoom 씬이 Load될 때 인스턴스 생성 시 공통으로 사용될 수 있는 부분!!
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
        {


            if (scene.name == "WaitingRoom")
            {
                //WaitingRoom일 때만 불린다.
                SetRandomCharacter();

                if (PhotonNetwork.isMasterClient)
                    MasterLoadedGame();
                else
                    NonMasterLoadedGame();
            }

            //if (scene.name == "MainGame")
            //{
            //    if (PhotonNetwork.isMasterClient)
            //        MasterLoadedGame();
            //    else
            //        NonMasterLoadedGame();
            //}
        }

        private void MasterLoadedGame()
        {
            PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
            PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
        }

        private void NonMasterLoadedGame()
        {
            PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        }

        /// <summary>
        /// 새로운 게임 신으로 모든 클라이언트를 동시에 접속 시킬 떄 사용
        /// </summary>
        [PunRPC]
        private void RPC_LoadGameOthers()
        {
            PhotonNetwork.LoadLevel("WaitingRoom");
        }

        [PunRPC]
        private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
        {
            //PlayerManagement.Instance.AddPlayerStats(photonPlayer);

            PlayersInGame++;

            PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);

            //if (PlayersInGame == PhotonNetwork.playerList.Length)
            //{
            //    print("All players are in the game scene.");
            //    PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
            //}
        }

        [PunRPC]
        private void RPC_CreatePlayer()
        {

            spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            GameObject spawnPoint = null;


            string playerCharacter = (string)PhotonNetwork.player.CustomProperties["Character"];

            foreach (var point in spawnPoints)
            {
                if (point.name.Contains(playerCharacter))
                    spawnPoint = point;
            }

            PhotonNetwork.Instantiate(Path.Combine("PlayerPrefab", playerCharacter), spawnPoint.transform.position,
                spawnPoint.transform.rotation, 0);

            //if (PhotonNetwork.playerList.Length == 1)
            //    PhotonNetwork.Instantiate(Path.Combine("PlayerPrefab", (string)PhotonNetwork.player.CustomProperties["Character"]), spawnPoinsts[0].transform.position,
            //    spawnPoinsts[0].transform.rotation, 0);

        }

        private void SetRandomCharacter()
        {
            if (PhotonNetwork.connected)
            {
                int maxPlayer = PhotonNetwork.room.MaxPlayers;
                m_playerCustomProperties["Character"] = PlayerMaker.Instance.SetRandomPlayer(maxPlayer).PlayerId.ToString();
                PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);
            }
        }

        //private void OnDestroy()
        //{
        //    if (PhotonView.isMine)
        //        PhotonNetwork.Destroy(photonView);
        //}
        //#endregion



        #endregion
    */
    }
    #endregion


    #endregion
}