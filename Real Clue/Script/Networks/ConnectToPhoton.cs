using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToPhoton : MonoBehaviour
{
    public GameObject LobbyScreen;

    private string username = "";
    private bool connecting = false;
    private string error = null;

    private void Start()
    {
        //플레이어가 입력한 사용자명을 로드한다
        username = PlayerPrefs.GetString("UserName", "");
    }

    public void OnGUI()
    {
        //접속 중
        if(connecting)
        {
            GUILayout.Label("Connecting...");
            return;
        }

        //오류가 발생하면 표시한다.
        if(error != null)
        {
            GUILayout.Label("Failed to connect:" + error);
            return;
        }

        //사용자 자신의 사용자명을 입력하게 한다.
        GUILayout.Label("Username");
        username = GUILayout.TextField(username, GUILayout.Width(200f));

        if(GUILayout.Button("Connect"))
        {
            //다음 번을 위해 사용자명을 기억한다
            PlayerPrefs.SetString("Username", username);

            //접속 중
            connecting = true;

            //사용자 명을 설정하고, 포톤에 접속한다.
            PhotonNetwork.playerName = username;
            PhotonNetwork.ConnectUsingSettings("v1.0");
        }
    }

    void OnJoinedLobby()
    {
        //로비에 참여했으므로, 로비 화면을 보여준다.

        connecting = false;
        gameObject.SetActive(false);
        //LobbyScreen.SetActive(true);
        PhotonNetwork.LoadLevel("Lobby");
    }

    void OnFailedConnectToPhoton(DisconnectCause cause)
    {
        //접속에 실패했으므로, 표시를 위해 오류를 저장한다.
        connecting = false;
        error = cause.ToString();
    }
}
