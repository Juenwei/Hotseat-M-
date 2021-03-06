using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlatform : MonoBehaviour
{
    public static EnablePlatform instance;

    private void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ReEnablePlatform(GameObject platform)
    {
        BreakingPlatform temporObject;
        temporObject = platform.GetComponent<BreakingPlatform>();
        StartCoroutine(temporObject.ResetPlatform());
    }

    
}
