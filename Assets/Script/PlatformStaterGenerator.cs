using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStaterGenerator : MonoBehaviour
{
    private bool countStart;
    private int amountLeft;
    [SerializeField] private GameObject platformPrefab, playerDetector, previousPlat;
    [SerializeField] private float distanceOffset;
    [SerializeField] private int initialAmountGenerate;
    [SerializeField] private Transform generatePoint;
    private List<GameObject> platformObjects=new List<GameObject>();
    private Vector3 initialPoint;
    private float startingTime=5f, currentTime=0f;


    private void Start()
    {
        initialPoint = gameObject.transform.position;
        //setup platform
        generatePlatform();
        //setup amount
        amountLeft = initialAmountGenerate;
        //setup time
        currentTime = startingTime;
    }

    void generatePlatform()
    {
        //Preset the platform
        for(int i=0;i<initialAmountGenerate;i++)
        {
            previousPlat = Instantiate(platformPrefab, generatePoint.position, Quaternion.identity);
            generatePoint.Translate(Vector3.forward * distanceOffset);
            platformObjects.Add(previousPlat);
            previousPlat.transform.SetParent(transform);
        }

        Destroy(generatePoint.gameObject);
    }

    void Update()
    {
        if (countStart)
        {
            if (amountLeft <= 0)
            {
                StartCoroutine(StartToReset());
            }
            else if (currentTime <= 0)
            {
                ResetGenerator();
            }
            TimeCountdown();
        }

    }

    IEnumerator StartToReset()
    {
        yield return new WaitForSeconds(3f);
        ResetGenerator();

    }

    void ResetGenerator()
    {
        //Reset Generator
        amountLeft = initialAmountGenerate;
        currentTime = startingTime;
        countStart = false;

        //Do reset Detector
        playerDetector.GetComponent<DetectPlayer>().ResetDetector();
        //Do reset breaking platform
        for(int i =0;i<initialAmountGenerate;i++)
        {
            platformObjects[i].GetComponent<BreakingPlatformPlus>().EnablePlatform(false);
        }
    }

    void TimeCountdown()
    {
        currentTime -= 1 * Time.deltaTime;
    }

    public float PlayerCollided(int index)
    {
        countStart = true;
        amountLeft--;
        platformObjects[index].GetComponent<BreakingPlatformPlus>().EnablePlatform(true);
        return distanceOffset;
    }

    public List<GameObject> getParentList()
    {
        return platformObjects;
    }

    public int getMaxPlatformAmount()
    {
        return initialAmountGenerate;
    }
    

   
}
