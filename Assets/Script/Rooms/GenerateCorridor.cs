using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateCorridor : MonoBehaviour
{
    [SerializeField] private GameObject basicPlatPrefabs, fanPrefabs, prefabsToBeGen;
    [SerializeField] private Transform startConnector, endConnector;
    //private void Start()
    //{
    //    SpawnPlatformCorridor(startConnector, endConnector);
    //}

    public void SpawnPlatformCorridor(Transform startP,Transform endP)
    {
        //Get the distance from two point
        float distance = Vector3.Distance(startP.position, endP.position);
        float amountPlaform;
        float platformSize;
        //Debug.Log("Distance : " + distance);
        //Get the size of prefabs
        platformSize = basicPlatPrefabs.transform.GetChild(0).localScale.z + basicPlatPrefabs.transform.GetChild(0).localScale.z / 2;
        //Debug.Log("Prefabs Size : " + platformSize);
        //Get the amount of platform that have need to be generate
        amountPlaform = Convert.ToInt32(distance / platformSize);
        //Debug.Log("Amount of Platform : " + amountPlaform);
        //Generate PLatform based on the amount
        for (int i = 1; i < amountPlaform; i++)
        {
            Vector3 point = Vector3.Lerp(startP.position, endP.position, i / amountPlaform);
            //Debug.Log("Vector Posistion : " + point);
            Instantiate(basicPlatPrefabs, point, Quaternion.identity).transform.parent = transform;
        }
    }

    public void SpawnJumpCorridor(Transform startP, Transform endP)
    {
        
        Vector3 endWithoutY = new Vector3(endP.position.x, startP.position.y, endP.position.z);
        bool platAfterFan = false;
        float heightOfGap = endP.position.y - startP.position.y;
        //Get the distance from two point//Distance might cost performance
        float distance = Vector3.Distance(startP.position, endWithoutY);
        float amountPlaform, platformSize;
        //Debug.Log("Distance : " + distance);
        //Get the size of prefabs
        platformSize = basicPlatPrefabs.transform.GetChild(0).localScale.z + basicPlatPrefabs.transform.GetChild(0).localScale.z / 2;
        //Debug.Log("Prefabs Size : " + platformSize);
        //Get the amount of platform that have need to be generate
        amountPlaform = Convert.ToInt32(distance / platformSize);
        //Debug.Log("Amount of Platform : " + amountPlaform);
        //Generate PLatform based on the amount
        int fanPlatSequence = UnityEngine.Random.Range(2, (int)amountPlaform - 1);
        for (int i = 1; i < amountPlaform; i++)
        {
            Vector3 point = Vector3.Lerp(startP.position, endWithoutY, i / amountPlaform);
            //Debug.Log("Vector Posistion : " + point);
            if(i==fanPlatSequence)
            {
                prefabsToBeGen = fanPrefabs;
                platAfterFan = true;
                
            }
            else if(platAfterFan)
            {
                prefabsToBeGen = basicPlatPrefabs;
                point.y += heightOfGap;
            }
            else
            {
                prefabsToBeGen = basicPlatPrefabs;
            }
            Instantiate(prefabsToBeGen, point, Quaternion.identity).transform.parent = transform;
        }
    }

    
}
