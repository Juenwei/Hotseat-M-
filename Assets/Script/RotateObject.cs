using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Transform anchorTransform;
    [SerializeField] private float rotateSpeed;
    private int[] rotateDirection = new int[2] { -1, 1 };
    private int selectedDirection;
    
    private Vector3 currentRotation;

    private void Start()
    {
        selectedDirection = rotateDirection[Random.Range(0, 2)];
    }

    void Update()
    {
        currentRotation = currentRotation + new Vector3(0, selectedDirection, 0) * rotateSpeed * Time.deltaTime;
        currentRotation = new Vector3(currentRotation.x % 360f, currentRotation.y % 360f, currentRotation.z % 360f);
        anchorTransform.eulerAngles = currentRotation;
    }
}
