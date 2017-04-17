using System;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : Photon.PunBehaviour
{
    #region Public Variables
    public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    public byte MaxPlayersPerRoom = 5;
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    public GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    public GameObject progressLabel;
    public Dropdown dropdown;
    public Text roomNameInput;
    public string roomName;
    public Button[] roomButton;

    private Text[] roomText;
    private RoomInfo[] roomsList;
    #endregion

    #region Private Variables
    /// <summary>
    /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
    /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
    /// Typically this is used for the OnConnectedToMaster() callback.
    /// </summary>
    bool isConnecting;
    /// <summary>
    /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
    /// </summary>
    string _gameVersion = "1";

    #endregion

    #region MonoBehaviour CallBacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        roomText = new Text[roomButton.Length];
        SetRoomText();
        // #NotImportant
        // Force Full LogLevel
        PhotonNetwork.logLevel = Loglevel;

        // #Critical
        // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
        PhotonNetwork.autoJoinLobby = true;

        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.automaticallySyncScene = true;
    }

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
    {
        ConnectLobby();
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    void SetRoomText()
    {
        for (int i = 0; i < roomButton.Length; i++)
        {
            roomText[i] = roomButton[i].GetComponentInChildren<Text>();
        }
    }

    #endregion


    #region Public Methods

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
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.connected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
            if (roomName != null && roomName.Length > 0)
            {
                Debug.Log("Joining room with name: " + roomName);
                PhotonNetwork.JoinRoom(roomName);
            }
            else
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
        }
    }

    public void ConnectLobby()
    {
        if (PhotonNetwork.connectionState != ConnectionState.Disconnected)
        {
            return;
        }

        try
        {
            PhotonNetwork.ConnectUsingSettings("1.0");
        }
        catch
        {
            Debug.LogWarning("Couldn't connect to server");
        }
    }

    #endregion


    #region Photon.PunBehaviour CallBacks


    public override void OnConnectedToMaster()
    {
        // we don't want to do anything if we are not attempting to join a room. 
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (isConnecting)
        {
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()
            if (roomName != null && roomName.Length > 0)
            {
                PhotonNetwork.JoinRoom(roomName);
            }
            else
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
        Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");


    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public void SetRoomName(string value)
    {
        roomName = value;
    }


    public override void OnDisconnectedFromPhoton()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public void SetRoomPlayerCount()
    {
        MaxPlayersPerRoom = (byte)(dropdown.value + 1);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
        Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }

    #endregion

    void Update()
    {
        if (PhotonNetwork.room == null)
        {
            for (int i = 0; i < roomButton.Length; i++)
            {
                if (roomsList != null)
                {
                    if (roomsList.Length > i)
                    {
                        roomButton[i].interactable = true;
                        roomText[i].text = roomsList[i].Name;
                    }
                }
                else
                {
                    roomButton[i].interactable = false;
                    roomText[i].text = "Empty";
                }
            }
        }
    }

    public void JoinOpenRoom(int index)
    {
        if (roomsList[index] != null)
        {
            PhotonNetwork.JoinRoom(roomText[index].text);
        }
    }

    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
    }

}