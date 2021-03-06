using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCheckPoint : MonoBehaviour,IActivation
{
    [SerializeField] private GameObject checkPointDetector;
    [SerializeField] private ParticleSystem explosionParicle;

    public void DoActivate()
    {
        StartCoroutine(ChangeToFinalScene());
        explosionParicle.Play();
    }

    IEnumerator ChangeToFinalScene()
    {
        yield return new WaitForSeconds(3f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(2);
    }
}
