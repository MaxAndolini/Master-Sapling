using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Vector3[] spawnPoints;

    public GameObject[] enemyPrefab;
    public bool flag;

    // Update is called once per frame
    private void Update()
    {
        // Initialize the position of the 5 points
        spawnPoints[0] = new Vector3(98.0f, 102.0f, 130.0f);
        spawnPoints[1] = new Vector3(102.5f, 102.0f, 130.0f);
        spawnPoints[2] = new Vector3(108.0f, 102.0f, 130.0f);
        spawnPoints[3] = new Vector3(114.0f, 102.0f, 130.0f);
        spawnPoints[4] = new Vector3(119.0f, 102.0f, 130.0f);
        
        Timer();
        StopCoroutine(WaitForHit(0));
    }

    private IEnumerator WaitForHit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        var randEnemy = Random.Range(0, enemyPrefab.Length);
        var randSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab[randEnemy], spawnPoints[randSpawnPoint], transform.rotation);
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