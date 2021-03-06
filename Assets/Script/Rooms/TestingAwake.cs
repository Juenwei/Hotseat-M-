using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingAwake : MonoBehaviour
{
    [SerializeField] private GameObject CornerPrefabs, lastRoom;
    private Vector3 lastEndpoint=Vector3.zero;
    private void Awake()
    {
        for(int i=0;i<5;i++)
        {
            lastRoom = Instantiate(CornerPrefabs, lastEndpoint, Quaternion.identity);
            lastEndpoint = lastRoom.GetComponent<CornerUnitContentSetup>().getEndpointVector();
        }
    }
}
