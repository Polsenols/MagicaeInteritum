using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Photon.PunBehaviour
{

    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;
    public Transform spawnPos;
    public float spawnHeight = 1;
    public float spawnRadius = 1;
    #region Photon Messages


    #endregion


    #region Public Methods

    public void Start()
    {
        PhotonNetwork.sendRate = 45;
        PhotonNetwork.sendRateOnSerialize = 35;
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.Log("We are Instantiating LocalPlayer from Game");
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            GameObject go = (GameObject)PhotonNetwork.Instantiate(this.playerPrefab.name, SpawnPos(), Quaternion.identity, 0);
            SetupPlayer(go);
        }
    }

    Vector3 SpawnPos()
    {
        Vector3 randomPos = spawnPos.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = spawnHeight;
        return randomPos;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    #endregion

    #region Private Methods
    void SetupPlayer(GameObject player)
    {
        if (player != null)
        {
            CharacterManager_NET charMan = player.GetComponent<CharacterManager_NET>();
            charMan.photonView.RPC("SetID", PhotonTargets.AllBuffered, PhotonNetwork.player.ID);
            charMan.photonView.RPC("SetName", PhotonTargets.AllBuffered, PhotonNetwork.player.NickName);
            charMan.photonView.RPC("AddPlayers", PhotonTargets.AllBuffered);
        }
    }

    void LoadArena()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.Log("PhotonNetwork : Loading Game");
        PhotonNetwork.LoadLevel("Game");
    }


    #endregion

    #region Photon Messages


    public override void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


            //LoadArena();
        }
    }


    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName); // seen when other disconnects


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


            LoadArena();
        }
    }


    #endregion
}