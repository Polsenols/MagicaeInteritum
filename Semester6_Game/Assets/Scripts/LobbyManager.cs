using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : Photon.PunBehaviour
{

    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;
    public Transform[] spawnPos;
    public Texture2D mouseTexture;
    public Button startBtn;
    public Text waitText;
    #region Photon Messages


    #endregion


    #region Public Methods

    public void Start()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            startBtn.gameObject.SetActive(false);
            waitText.gameObject.SetActive(true);
        }
        else
        {
            startBtn.enabled = true;
        }
        Cursor.SetCursor(mouseTexture, Vector2.zero, CursorMode.Auto);
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 20;
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (PhotonNetwork.inRoom)
            {
                Debug.Log("We are Instantiating LocalPlayer from Game");
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                GameObject go = (GameObject)PhotonNetwork.Instantiate(this.playerPrefab.name, SpawnPos(), Quaternion.identity, 0);
                SetupPlayer(go);
            }
        }
    }

    Vector3 SpawnPos()
    {
        return spawnPos[PhotonNetwork.player.ID - 1].position;
    }

    public void StartGame()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
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
            //charMan.photonView.RPC("AddPlayers", PhotonTargets.AllBuffered);
        }
    }


    #endregion

    #region Photon Messages


    #endregion
}