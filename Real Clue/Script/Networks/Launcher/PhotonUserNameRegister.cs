using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonUserNameRegister : MonoBehaviour {

    private bool alreadyRegister = false;

    public bool removePrefs = false;

    public InputField usernameInput;

    public GameObject objectParent;

    public GameObject createButton;

    /*
     * how to use PhotonNetwork.playerName
     * 
     * public Text playerName;
     * 
     * 
     * private void Awake()
     *{
     *  playerName.text = PhotonNetwork.playerName
     *  
     *  if(!photonView.isMIne)
     *  {
     *      playerName.text = photonView.owner.name;
     * 
     */

    private void Awake()
    {
        if (removePrefs)
            PlayerPrefs.SetInt("registered", 0);

        if(PlayerPrefs.GetInt("registered") == 0)
            CheckRegister();
    }

    void CheckRegister()
    {
        if(!alreadyRegister)
        {
            objectParent.SetActive(true);
        }
    }

    public void UsernameInputChange()
    {
        if (usernameInput.text.Length >= 2)
            createButton.SetActive(true);
        else
            createButton.SetActive(false);
    }

    public void CreateUserName()
    {
        PhotonNetwork.playerName = usernameInput.text;
        PlayerPrefs.SetInt("registered", 1);
    }
}
