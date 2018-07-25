using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;

public class GameTurnCore : PunBehaviour, IPunTurnManagerCallbacks
{
    private PunTurnManager turnManager;

    private void Start()
    {
        this.turnManager = this.gameObject.AddComponent<PunTurnManager>();
        this.turnManager.TurnManagerListener = this;
        this.turnManager.TurnDuration = 5f;
    }

    private void Update()
    {
        
    }

    public void OnPlayerFinished(PhotonPlayer player, int turn, object move)
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerMove(PhotonPlayer player, int turn, object move)
    {
        throw new System.NotImplementedException();
    }

    public void OnTurnBegins(int turn)
    {
        throw new System.NotImplementedException();
    }

    public void OnTurnCompleted(int turn)
    {
        throw new System.NotImplementedException();
    }

    public void OnTurnTimeEnds(int turn)
    {
        throw new System.NotImplementedException();
    }
}
