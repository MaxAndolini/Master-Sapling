using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    private Vector3[] spawnPoints = new Vector3[5];

    public GameObject[] enemyPrefab;
    public bool flag;

    private void Start()
    {
        // Initialize the position of the 5 points
        spawnPoints[0] = new Vector3(98.0f, 100.4f, 130.0f);
        spawnPoints[1] = new Vector3(102.5f, 100.4f, 130.0f);
        spawnPoints[2] = new Vector3(108.0f, 100.4f, 130.0f);
        spawnPoints[3] = new Vector3(114.0f, 100.4f, 130.0f);
        spawnPoints[4] = new Vector3(119.0f, 100.4f, 130.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.Instance.paused)
        {
            Timer();
            StopCoroutine(WaitForHit(0));
        }
    }

    private IEnumerator WaitForHit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        var randEnemy = Random.Range(0, enemyPrefab.Length);
        var randSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab[randEnemy], spawnPoints[randSpawnPoint], Quaternion.Euler(new Vector3(0, 180, 0)));
        flag = false;
    }

    public void Timer()
    {
        if (!flag)
        {
            flag = true;
            StartCoroutine(WaitForHit(5f));
        }
    }
}