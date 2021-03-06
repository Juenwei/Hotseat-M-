using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatformPlus : MonoBehaviour,IBreakable
{
    [SerializeField] private BoxCollider boxColl;
    [SerializeField] private GameObject platMesh;
    private Rigidbody rb;
    Vector3 initialPosition;
    //private Animator anime;
    private float currentTime, startTime=7f;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //anime = GetComponent<Animator>();
        currentTime = startTime;
    }

    void Start()
    {
        //Hide object and coll
        EnablePlatform(false);
        initialPosition = transform.position;
    }

    public void BreakObject()
    {
        StartCoroutine(DropPlatform());
    }

    IEnumerator DropPlatform()
    {
        yield return new WaitForSeconds(0.8f);
        rb.isKinematic = false;


        yield return new WaitForSeconds(0.8f);
        rb.isKinematic = true;
        EnablePlatform(false);
        transform.position = initialPosition;
    }

    public void EnablePlatform(bool setupBoolean)
    {
        boxColl.enabled = setupBoolean;
        platMesh.SetActive(setupBoolean);
    }

    
}
