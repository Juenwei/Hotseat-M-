using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerUnitContentSetup : MonoBehaviour
{
    [Header("EndPoint Setup")]
    [SerializeField] private Transform oriEndPoint, movableEndPoint;
    [SerializeField] private float angleDirection;

    [Header("Content Setup")]
    [SerializeField] private Transform turningPoint , movableGeneratePoint;
    [SerializeField] private int contentAmount;
    [SerializeField] private List<GameObject> contentPrefabsList;
    [SerializeField] private Transform contentContainer;
    private int[] contentList;
    

    void Awake()
    {
        PlaceEndPoint();
    }

    private void Start()
    {
        
    }

    private void PlaceEndPoint()
    {
        movableEndPoint.position = oriEndPoint.position;
        movableEndPoint.eulerAngles = new Vector3(0, angleDirection, 0);
        movableEndPoint.position += movableEndPoint.TransformDirection(new Vector3(0, 0, 35));
        //reset back
        movableEndPoint.eulerAngles = Vector3.zero;
        //Destroy old endpoint
        Destroy(oriEndPoint.gameObject);

    }

    public Vector3 getEndpointVector()
    {
        return movableEndPoint.position;
    }

    private void GenerateContent()
    {
        //Instantiate(contentPrefabsList[]);
        //for(int i =0;i<contentAmount;i++)
        //{
        //    //Instantiate();
        //}
    }


}
