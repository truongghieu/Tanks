using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
namespace Complete
{
public class DiePanel : MonoBehaviour
{
    public static DiePanel instance;

    public GameObject diePanel;

    void Awake()
    {
        instance = this;
    }


    public void ShowDiePanel()
    {
        diePanel.SetActive(true);
    }

    public void OnClick_Respawn()
    {
        diePanel.SetActive(false);
        GameManager.instance.ReSpawn();

    }

    public void OnClick_Exit()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("_Loading");
    }


}
}