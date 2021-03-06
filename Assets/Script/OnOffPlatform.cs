using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffPlatform : MonoBehaviour
{
    [SerializeField] private float waitingTime = 2f;
    private Coroutine setOnCoroutine,setOffCoroutine;
    [SerializeField] private List<GameObject> onOffPlatforms;
    void Start()
    {
        StartCoroutine(SetOff());
    }

    IEnumerator SetOn()
    {
        yield return new WaitForSeconds(waitingTime);
        
        for(int i=0;i<onOffPlatforms.Count;i++)
        {
            onOffPlatforms[i].gameObject.SetActive(true);
        }
        StopCoroutine(SetOn());
        setOffCoroutine = null;
        if (setOffCoroutine == null)
        {
            setOffCoroutine = StartCoroutine(SetOff());
        }
        
    }

    IEnumerator SetOff()
    {
        yield return new WaitForSeconds(waitingTime);
        //Debug.Log("First"+setOnCoroutine);
        for (int i = 0; i < onOffPlatforms.Count; i++)
        {
            onOffPlatforms[i].gameObject.SetActive(false);
        }
        StopCoroutine(SetOff());
        setOnCoroutine = null;
        if (setOnCoroutine == null)
        {
            setOnCoroutine = StartCoroutine(SetOn());
        }

    }
}
