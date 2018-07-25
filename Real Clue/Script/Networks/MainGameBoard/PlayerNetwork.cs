using RealClue;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNetwork : Photon.PunBehaviour
{
    public static PlayerNetwork Instance;
    public GameObject CardPrefab;
    public GameObject CardPanel;

    private PhotonView photonView;
    private GameObject gameUI;
    public GameObject text; 

    private void Awake()
    {
        Instance = this;
        SerializeHelper.Register();

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
        PhotonNetwork.OnEventCall += this.OnSetupPlayerDeckEvent;

        photonView = GetComponent<PhotonView>();
        gameUI = GameObject.Find("GameUI");

        PhotonPlayer[] players = PhotonNetwork.playerList;

        InstantiatePlayerModel(players);
    }

    private void InstantiatePlayerModel(PhotonPlayer[] players)
    {
        List<string> playerInString = new List<string>();

        //PlayerMaker에서 참여한 플레이어 캐릭터만큼의 Player Model을 생성한다.
        foreach (PhotonPlayer player in players)
        {
            playerInString.Add(player.CustomProperties["Character"].ToString());
        }

        PlayerMaker.Instance.SetPlayers(playerInString); //Initialize Player Model List
    }

    //master에 의해서 불린다
    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            print("Game Started");

            //master가 Card Deck을 생성한다
            if (PhotonNetwork.isMasterClient)
                SetPlayerCards(PhotonNetwork.player);
        }
    }

    private void SetPlayerCards(PhotonPlayer photonPlayer)
    {
        #region Raising Event parameters
        byte evCode = 0;
        bool reliable = true;
        Dictionary<int, byte[]> playerCardsSet = new Dictionary<int, byte[]>();
        #endregion

        print("Master client is drawing player cards");

        //카드가 배분된 Player Class의 리스트가 반환된다.
        List<Player> playerModelList = Deck.Instance.Draw();

        //Client에게 전달할 Dictionary 생성(List의 Serialize는 SerializeHelper에서 PhotonPeer에 등록했다)
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            var playerDict = playerModelList
                        .Where(x => x.PlayerId.ToString() == player.CustomProperties["Character"].ToString())
                        .ToDictionary(x => player.ID, x => x.GameCards.SelectMany(BitConverter.GetBytes).ToArray());
            playerCardsSet.Add(playerDict.Select(x => x.Key).First(), playerDict.Select(x => x.Value).First());
        }

        var option = new RaiseEventOptions()
        {
            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.All,
        };

        //모든 Client에게 카드 UI를 생성하도록 이벤트를 호출한다.
        PhotonNetwork.RaiseEvent(evCode, playerCardsSet, true, option);
    }

    //Client가 각자 Card UI를 구성하는 이벤트
    void OnSetupPlayerDeckEvent(byte eventcode, object content, int senderid)
    {
        Dictionary<int, byte[]> playerCardsSet = content as Dictionary<int, byte[]>;

        bool index = playerCardsSet.Any(x => x.Key == PhotonNetwork.player.ID);
        if (index)
        {
            //PlayerCards playerCards = new PlayerCards(playerCardsSet[index].Keys.FirstOrDefault(), playerCardsSet[index].Values.FirstOrDefault());
            byte[] playerCards = playerCardsSet[PhotonNetwork.player.ID];
            int[] newplayerCards = ShrinkToByteArray(playerCards);

            //이 부분이 prefab생성으로 바꾸기
            List<GameObject> CardImages = new List<GameObject>();
            for (int cardCount = 0; cardCount < newplayerCards.Count(); cardCount++)
            {
                GameObject cardObj = Instantiate(CardPrefab);
                cardObj.GetComponent<Image>().sprite = Resources.Load<Sprite>($"CardImages/{newplayerCards[cardCount].ToString()}");
                cardObj.transform.SetParent(CardPanel.transform, false);
                CardImages.Add(cardObj);
            }
        }
    }

    private int[] ShrinkToByteArray(byte[] playerCards)
    {
        var arrayToReturn = new int[playerCards.Length / 4];

        for (int i = 0; i < playerCards.Length; i += 4)
        {
            arrayToReturn[i / 4] = BitConverter.ToInt32(playerCards, i);
        }

        return arrayToReturn;
    }
}

//public class PlayerCards
//{
//    public PlayerCards(PhotonPlayer photonPlayer, List<int> cards)
//    {
//        PhotonPlayer = photonPlayer;
//        Cards = cards;
//    }

//    public readonly PhotonPlayer PhotonPlayer;
//    public readonly List<int> Cards;
//}
