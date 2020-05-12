using ExitGames.Client.Photon;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class MultiplayerHandler : MonoBehaviourPunCallbacks
{
    private static string version = "V" + 0.1;
    public GameObject spawnPlayer;

    private bool isSpawned = false;

    public void Connect()
    {
        DontDestroyOnLoad(gameObject);
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = "";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = version;

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.JoinRandomRoom();

        PhotonNetwork.SendRate = 128;
        PhotonNetwork.SerializationRate = 128;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        RoomOptions options = new RoomOptions();
        TypedLobby lobbyType = new TypedLobby("Room1", LobbyType.Default);
        PhotonNetwork.JoinOrCreateRoom("Room1", options, lobbyType);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);

        Debug.Log(cause);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel(1);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1 && isSpawned == false)
        {
            Transform spawn = SpawnPlayer();

            GameObject player = PhotonNetwork.Instantiate("MP", spawn.position, Quaternion.identity);

            isSpawned = true;
        }
    }

    public Transform SpawnPlayer()
    {
        Spawn[] spawns = FindObjectsOfType<Spawn>();

        int i = Random.Range(0, spawns.Length);

        return spawns[i].transform;
    }
}
