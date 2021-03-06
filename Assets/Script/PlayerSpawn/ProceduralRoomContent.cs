using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRoomContent : MonoBehaviour
{
    [SerializeField]private Transform startP, endP;
    [SerializeField]private List<GameObject> contentPrefabs;
    private int ObstacleMin = 5, ObstacleMax = 7;
    [SerializeField]private Vector3 vectorOffsetFan,vectorOffsetBreakBall;
    
    Vector3 generatePosistion;

    void Start()
    {
        //generatePosistion = startP.transform.position;
        int indexRange = Random.Range(ObstacleMin, ObstacleMax);
        for(int i =0;i<indexRange;i++)
        {
            generatePosistion = Vector3.Lerp(startP.position, endP.position, (i + 1) / indexRange);
        }
    }

    void PickObstacle()
    {
        int index = Random.Range(0, contentPrefabs.Count);
        if(index==0)
        {
            generatePosistion += vectorOffsetBreakBall;
        }
        else
        {
            generatePosistion += vectorOffsetFan;
        }


        Instantiate(contentPrefabs[index],generatePosistion,Quaternion.identity);
    }

   
}
