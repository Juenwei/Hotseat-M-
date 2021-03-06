using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralContent : MonoBehaviour
{
    [SerializeField] private List<GameObject> pillarPrefabs;
    [SerializeField] private GameObject breakingPathPrefabs;
    [SerializeField] private Transform roomTranform,generatePrefabPoint,contentContainer;
    [SerializeField] private int totalAmountGenerate;
    [SerializeField] private Vector3 forwardOffset = new Vector3(0, 0, 4.3f) , rotationSetup;
    private int index,lastIndex=0;
    private GameObject nextObject,targetObject;

    // Start is called before the first frame update
    void Start()
    {
        generatePrefabPoint.eulerAngles = roomTranform.eulerAngles;
        generatePrefabPoint.eulerAngles += rotationSetup;
        for (int i = 1; i <= totalAmountGenerate; i++)
        {
            if ((i % 2) == 0)
            {
                //Genererate Pillar
                do
                {
                    index = Random.Range(0, pillarPrefabs.Count);
                    nextObject = pillarPrefabs[index];
                } while (index == lastIndex);
                lastIndex = index;
            }
            else
            {
                //Generate Floor
                nextObject = breakingPathPrefabs;
            }

            //Instantiate prefabs
            targetObject = Instantiate(nextObject, generatePrefabPoint.position, generatePrefabPoint.rotation);
            targetObject.transform.SetParent(contentContainer.transform);

            //Change Posistion
            generatePrefabPoint.position += generatePrefabPoint.TransformDirection(forwardOffset);


        }


    }

    
}
