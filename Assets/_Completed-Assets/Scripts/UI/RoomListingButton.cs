using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomListingButton : MonoBehaviour
{
    public TextMeshProUGUI roomNameText;

    public void SetRoom(string roomName)
    {
        roomNameText.text = roomName;
    }

    public void select()
    {
        Debug.Log("Selected room: " + roomNameText.text);
        CreateAndJoinRoom.instance.joinRoomInput.text = roomNameText.text;
    }

}
