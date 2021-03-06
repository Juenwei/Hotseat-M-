using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRoomSetupInfo : MonoBehaviour
{
    [SerializeField] private float corridorheightOffset;
    [SerializeField] private Transform endPointPos,checkPoint;
    [SerializeField] private int roomDifficulty;

    public float GetHeightOffset()
    {
        return corridorheightOffset;
    }
    
    public Transform GetEndTransform()
    {
        return endPointPos;
    }

    public int GetRoomDifficulty()
    {
        return roomDifficulty;
    }

    public Transform GetCheckPointTransform()
    {
        return checkPoint;
    }
}
