using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{

    public static CreateAndJoinRoom instance;
    public GameObject m_RoomListingPrefab;
    public GameObject m_RoomListingContainer;

    public void Awake()
    {
        instance = this;
    }

    public InputField createRoomInput;
    public InputField joinRoomInput;





    public void OnClick_CreateRoom()
    {
        if (createRoomInput.text.Length > 0)
        {
            PhotonNetwork.CreateRoom(createRoomInput.text);
        }
    }

    public void OnClick_JoinRoom()
    {
        if (joinRoomInput.text.Length > 0)
        {
            PhotonNetwork.JoinRoom(joinRoomInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        if(PhotonNetwork.CurrentRoom.Name == "Singleplayer")
        {
            PhotonNetwork.LoadLevel("_SinglePlayer");
        }
        else
        {
            PhotonNetwork.LoadLevel("_Game");
        }
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room: " + message);
        // Handle room creation failure
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room: " + message);
        // Handle room joining failure
    }

    // check if new room is created and list it
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            // remove room from list
            if (room.RemovedFromList)
            {
                foreach (Transform child in m_RoomListingContainer.transform)
                {
                    Destroy(child.gameObject);
                }
            }
            else
            {
                GameObject roomListing = Instantiate(m_RoomListingPrefab, m_RoomListingContainer.transform);
                roomListing.GetComponent<RoomListingButton>().SetRoom(room.Name);
            }
        }
    }

    public void Singleplayer()
    {
        PhotonNetwork.CreateRoom("Singleplayer");
    }
    

}
