using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IDeath deathTarget = collision.gameObject.GetComponent<IDeath>();
        if(deathTarget!=null)
        {
            deathTarget.DoDeath();
        }
    }
}
