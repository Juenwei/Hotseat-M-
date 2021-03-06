using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour, IAttachableObject,IDeath
{
    [SerializeField] private Transform followHeadTranform;

    private void Update()
    {
        CheckForDeathCondition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IBreakable breakable = collision.gameObject.GetComponent<IBreakable>();
        if (breakable != null)
        {
            breakable.BreakObject();

        }

        IActivation activationObject = collision.gameObject.GetComponent<IActivation>();
        if(activationObject!=null)
        {
            activationObject.DoActivate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IActivation activationObject = other.GetComponent<IActivation>();
        if (activationObject != null)
        {
            activationObject.DoActivate();
        }
    }

    public void AttachObject(Transform parentPoint)
    {
        transform.parent = parentPoint;
    }

    public void ReleaseObject(Transform parentPoint)
    {
        transform.parent = null;
    }

    public void DoDeath()
    {
        
        //Here should do remove control
        gameObject.GetComponent<PlayerMovementFirst>().RemovePlayerControl();
        UIManager.instance.ShowDeathPanel(true);

        //Report to Game manager
        StartCoroutine(DeathScene());
    }

    IEnumerator DeathScene()
    {
        gameObject.GetComponent<AnimController>().FreezeAnimation();
        yield return new WaitForSeconds(2f);
        UIManager.instance.ShowDeathPanel(false);
        GameManager.instance.PlayerIsDeath(gameObject);
    }
    
    public Transform PassFollowHead()
    {
        //Debug.Log("Pass Follow Head  + position: " + followHeadTranform.position);
        return followHeadTranform;
    }

    void CheckForDeathCondition()
    {
        if (transform.position.y<-50)
        {
            DoDeath();
        }

    }

    
}
