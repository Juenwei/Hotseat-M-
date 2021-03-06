using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public static SoundManagement instance;
    [SerializeField] private List<AudioClip> bgList;
    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

    }

    private void Start()
    {
        PlayBgSound();
    }

    

    public void PlayBgSound()
    {
        int index = Random.Range(0, 2);
        audioSource.clip = bgList[index];
        audioSource.loop = true;
        audioSource.Play();
    }
    
}
