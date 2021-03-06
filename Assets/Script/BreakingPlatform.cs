using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour,IBreakable
{
    private Rigidbody rb;
    bool canReset;
    private Vector3 currentPosisition,currentRotation;

    void Start()
    {
        canReset = true;
        currentRotation = transform.eulerAngles;
        currentPosisition = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    
    public void BreakObject()
    {
        StartCoroutine(DropPlatform());
        EnablePlatform.instance.ReEnablePlatform(gameObject);
    }

    IEnumerator DropPlatform()
    {
        yield return new WaitForSeconds(0.8f);
        rb.isKinematic = false;
        yield return new WaitForSeconds(0.8f);
        rb.isKinematic = true;
        gameObject.SetActive(false);
        
    }

    public IEnumerator ResetPlatform()
    {
        //Debug.Log("Reset Platform Coroutine Start , wait for few sec");
        yield return new WaitForSeconds(3f);
        //Debug.Log("Reset Platform Reset the platform");
        transform.position = currentPosisition;
        transform.eulerAngles = currentRotation;
        gameObject.SetActive(true);
        canReset = true;
    }

}

   
