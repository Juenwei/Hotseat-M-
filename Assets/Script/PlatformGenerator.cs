using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
   
    [SerializeField]
    private GameObject platformPrefab;

    void Awake()
    {


        Instantiate(platformPrefab, new Vector3(3, 3.4f, 16), Quaternion.identity);
        Instantiate(platformPrefab, new Vector3(3, 3.4f, 18.2f), Quaternion.identity);
        Instantiate(platformPrefab, new Vector3(3, 3.4f, 20.4f), Quaternion.identity);
        Instantiate(platformPrefab, new Vector3(3, 3.4f, 26.5f), Quaternion.identity);

    }

    

    IEnumerator SpawnPlatform(Vector3 spawnPoint)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab, spawnPoint, platformPrefab.transform.rotation);
    }

    public void SpawningPlatform(Vector3 spawnPoint)
    {
        StartCoroutine(SpawnPlatform(spawnPoint));
    }
}
