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

        private void Start()
        {

            PhotonView = GetComponent<PhotonView>();
            PlayerName = PhotonNetwork.playerName;

            PhotonNetwork.sendRate = 60;
            PhotonNetwork.sendRateOnSerialize = 30;

        }

        private void Update()
        {

        }

        public void onStartButtonClicked()
        {
            //TODO Countdown 시작한다
            print("Start the game");


            SceneManager.LoadScene("Main");
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer other)
        {

        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

        }
    }
}