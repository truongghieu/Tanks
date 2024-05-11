using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class connectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server");
        PhotonNetwork.JoinLobby();
    }
    
    // public override void OnJoinedLobby()
    // {
    //     Debug.Log("Joined lobby");
    //     // join lobby
    //     SceneManagement.LoadScene("_Lobby");
       
    // }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        // join lobby
        SceneManager.LoadScene("_Lobby");
       
    }
}
