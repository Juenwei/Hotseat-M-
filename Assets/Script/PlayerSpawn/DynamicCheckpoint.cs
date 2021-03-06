using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCheckpoint : MonoBehaviour,IActivation
{
    [SerializeField]private Transform spawnPoint;
    [SerializeField]private GameObject checkPointDetector;
    [SerializeField]private Material ActivatedColor;
    private bool isCheckActivated = false;

    public void DoActivate()
    {
        if(!isCheckActivated)
        {
            isCheckActivated = true;
            checkPointDetector.GetComponent<MeshRenderer>().material = ActivatedColor;

            GameManager.instance.UpdateSpawnPoint(spawnPoint);
        }
        

    }

}
