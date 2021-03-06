using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour,IRunnableWall
{
    public bool DisableGravity()
    {
        return false;
    }

    public bool EnableGravity()
    {
        return true;
    }
}
