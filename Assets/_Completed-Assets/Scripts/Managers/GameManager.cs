using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;


namespace Complete
{
    public class GameManager : MonoBehaviour
    {   
        public static GameManager instance;
        public int m_ScoreToWin = 5;            // The number of rounds a single player has to win to win the game.
        public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
        public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
        public CameraControl m_CameraControl;       // Reference to the CameraControl script for control during different phases.
        public Text m_MessageText;                  // Reference to the overlay Text to display winning text, etc.
        public GameObject m_TankPrefab;             // Reference to the prefab the players will control.
        public TankManager[] m_Tanks;               // A collection of managers for enabling and disabling different aspects of the tanks.
        public EnemyManager m_EnemyManager;
        
        private int m_RoundNumber;                  // Which round the game is currently on.
        private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
        private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
        private TankManager m_RoundWinner;          // Reference to the winner of the current round.  Used to make an announcement of who won.
        private TankManager m_GameWinner;           // Reference to the winner of the game.  Used to make an announcement of who won.

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            // Create the delays so they only have to be made once.
            m_StartWait = new WaitForSeconds (m_StartDelay);
            m_EndWait = new WaitForSeconds (m_EndDelay);
            EnemyManager.OnRoundNumberChanged += FadeOutText;
            SpawnAllTanks();
            SetCameraTargets();
            m_MessageText.text = "Try To Eliminate Other Players!";
            StartCoroutine(FadeOutText());
            // StartCoroutine(CheckScore());

            // Once the tanks have been created and the camera is using them as targets, start the game.
            // StartCoroutine (GameLoop ());
        }
         public void ReSpawn(int e = 2){
            SpawnAllTanks();
            SetCameraTargets();
            m_MessageText.text = "Try To Eliminate Other Players!";
            StartCoroutine(FadeOutText()); 
         }

        private void SpawnAllTanks()
        {
            // For all the tanks...
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                // ... create them, set their player number and references needed for control.
                // m_Tanks[i].m_Instance =
                //     Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
                // m_Tanks[i].m_PlayerNumber = i + 1;
                // m_Tanks[i].Setup();
                m_Tanks[i].m_Instance = PhotonNetwork.Instantiate(m_TankPrefab.name, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation);
                m_Tanks[i].Setup();
            }
        }

        

        private IEnumerator CheckScore()
        {
            // check number of player in room
            while (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                yield return null;
            }
            SceneManager.LoadScene("_Lobby");
        }


        private void SetCameraTargets()
        {
          
            // Create a collection of transforms the same size as the number of tanks.
            Transform[] targets = new Transform[m_Tanks.Length];

            // For each of these transforms...
            for (int i = 0; i < targets.Length; i++)
            {
                // ... set it to the appropriate tank transform.
                targets[i] = m_Tanks[i].m_Instance.transform;
            }

            // These are the targets the camera should follow.
            m_CameraControl.m_Targets = targets;
            
        }

        
        // This function is used to turn all the tanks back on and reset their positions and properties.
        public void ResetAllTanks()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                Destroy(m_Tanks[i].m_Instance);
            }
            
        }


        private void EnableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].EnableControl();
            }
        }


        private void DisableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].DisableControl();
            }
        }
        

        public void FadeOutText(int e){
            StartCoroutine(FadeOutText());
        }

        private IEnumerator FadeOutText()
        {
            while (m_MessageText.color.a > 0)
            {
                m_MessageText.color = new Color(m_MessageText.color.r, m_MessageText.color.g, m_MessageText.color.b, m_MessageText.color.a - Time.deltaTime/5);
                yield return null;
            }
            m_MessageText.text = "";
            m_MessageText.color = new Color(m_MessageText.color.r, m_MessageText.color.g, m_MessageText.color.b, 1);
        }

    }
}