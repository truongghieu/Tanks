using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Enemy[] enemies;
    public Text m_notificationText;
    public GameObject[] enemyPrefabs;
    public int m_RoundNumber = 0;
    
    public static Action<int> OnRoundNumberChanged;
    public static Action OnSpawnedEnemies;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    


    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(3f);

        while (m_RoundNumber < enemies.Length)
        {
            enemyPrefabs = new GameObject[enemies[m_RoundNumber].count];
            m_notificationText.text = "Round " + (m_RoundNumber + 1);
            for (int i = 0; i < enemies[m_RoundNumber].count; i++)
            {
                int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[spawnPointIndex];
                enemyPrefabs[i] =  Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                MeshRenderer[] renderers = enemyPrefabs[i].GetComponentsInChildren<MeshRenderer> ();
                for (int j = 0; j < renderers.Length; j++)
                {
                    renderers[j].material.color = enemies[m_RoundNumber].color;
                }
                enemyPrefabs[i].transform.localScale = Vector3.one * enemies[m_RoundNumber].size;

                yield return new WaitForSeconds(enemies[m_RoundNumber].spawnDelay);
            }
            OnRoundNumberChanged?.Invoke(m_RoundNumber + 1);
            m_RoundNumber++;
            yield return new WaitForSeconds(enemies[m_RoundNumber].spawnTime);
        }
    
    
}




[System.Serializable]
public enum EnemyType
{
    Normal,
    Fast,
    Strong
}

[System.Serializable]
public class Enemy
{
    public EnemyType enemyType;
    public int count;
    public float spawnTime;
    public float spawnDelay;
    public Color color;
    public int size;
}
}