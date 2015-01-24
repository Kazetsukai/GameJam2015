using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// This script automatically connects to Photon (using the settings file), 
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class ConnectAndJoinRandom : Photon.MonoBehaviour
{
    /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
    public bool AutoConnect = true;

    public byte Version = 1;

    /// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
    private bool ConnectInUpdate = true;

    private bool _countingDown = false;
    private float _countDown = 5;

    public GameObject PlayerObject;

    public virtual void Start()
    {
        PhotonNetwork.autoJoinLobby = false;    // we join randomly. always. no need to join a lobby to get the list of rooms.
        PhotonNetwork.automaticallySyncScene = true;
        DontDestroyOnLoad(this);
    }

    public virtual void Update()
    {
        if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
        {
            Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(Version + "."+Application.loadedLevel);
        }

        if (Application.loadedLevel == 0)
        {
            if (PhotonNetwork.isMasterClient && PhotonNetwork.inRoom && PhotonNetwork.playerList.Length > 1)
            {
                _countingDown = true;
            }

            if (_countingDown)
            {
                Debug.Log(_countDown + " seconds left");
                _countDown -= Time.deltaTime;
            }

            if (_countDown < 0)
            {
                PhotonNetwork.LoadLevel("Main");
            }
        }
    }

    // to react to events "connected" and (expected) error "failed to join random room", we implement some methods. PhotonNetworkingMessage lists all available methods!

    public virtual void OnConnectedToMaster()
    {
        if (PhotonNetwork.networkingPeer.AvailableRegions != null) Debug.LogWarning("List of available regions counts " + PhotonNetwork.networkingPeer.AvailableRegions.Count + ". First: " + PhotonNetwork.networkingPeer.AvailableRegions[0] + " \t Current Region: " + PhotonNetwork.networkingPeer.CloudRegion);
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { maxPlayers = 6 }, null);
    }

    // the following methods are implemented to give you some context. re-implement them as needed.

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom()
    {
        //Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");

        Debug.Log("Woop woop");

        var player = PhotonNetwork.Instantiate(PlayerObject.name, Vector3.zero, Quaternion.identity, 0);
        player.GetComponent<PersonController>().Player = true;
        player.GetPhotonView().RPC("SetRemote", PhotonTargets.OthersBuffered, null);
        
    }

    public void OnJoinedLobby()
    {
        //Debug.Log("OnJoinedLobby(). Use a GUI to show existing rooms available in PhotonNetwork.GetRoomList().");
    }

    void OnLevelWasLoaded(int level)
    {
        var player = PhotonNetwork.Instantiate(PlayerObject.name, Vector3.zero, Quaternion.identity, 0);
		player.GetComponent<PersonController>().Player = true;
        player.GetPhotonView().RPC("SetRemote", PhotonTargets.OthersBuffered, null);
    }
}