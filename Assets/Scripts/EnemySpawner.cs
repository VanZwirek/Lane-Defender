using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private float respawnTime;
   
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(respawnTime);
        Instantiate(Enemy, new Vector3(0, 0, 0), Quaternion.identity);
        StartCoroutine(SpawnEnemy());
    }
}