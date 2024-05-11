using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Complete
{
public class GameManager_2 : MonoBehaviour
{
    public GameObject m_TankPrefab;
    public CameraControl cam;
    

    private void Start()
    {
        Instantiate(m_TankPrefab, new Vector3(0, 0, 0), Quaternion.identity);    
        cam.m_Targets = new Transform[1]; 
        cam.m_Targets[0] = m_TankPrefab.transform;
    }
}
}