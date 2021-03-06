using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwingingObstacle1 : MonoBehaviour
{
    private LineRenderer lineObject;

    //WTF iss this???
    private int count = 1;

    //ARm should be passed by obstacle
    public Transform swingObs, arm;

    //public Transform player;
    private Rigidbody rb;

    //Joint itselft
    private SpringJoint joint;
    private Vector3 grapplePoint;

    private bool isSwinging;//Check for TExt
    private bool isTrigger;

    //public TextMeshProUGUI detach;
    //public TextMeshProUGUI tips;

    private void Awake()
    {
        lineObject = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb = player.GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        PlayerInput();
    }

    private void LateUpdate()
    {
        DrawRope();
    }


    void PlayerInput()
    {
        //Debug.Log(isSwinging);
        if (Input.GetKeyDown(KeyCode.F) && (count % 2) == 1 && isTrigger)
        {
            StartSwinging();
            count++;
        }
        else if (Input.GetKeyDown(KeyCode.F) && (count % 2) == 0 && isTrigger)
        {
            StopSwinging();
            count--;
        }
    }
    void StartSwinging()
    {
        if (!isSwinging)
        {
            Debug.Log("startSwing");
            //Connect pLayer object swingObs = PlayerObject 
            joint = swingObs.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;

            //Grapple Point is the anchor point not player
            grapplePoint = joint.anchor;
            joint.connectedAnchor = grapplePoint;

            //Player rigidbody
            joint.connectedBody = rb;

            joint.spring = 1.8f;
            joint.damper = 7f;
            joint.massScale = 1f;
            joint.connectedMassScale = 1f;
            joint.maxDistance = 7f;
            joint.minDistance = 0;

            //LineRenderer
            lineObject.positionCount = 2;
            isSwinging = true;
            Debug.Log(isSwinging);
        }
        
    }

    void StopSwinging()
    {
        Debug.Log("stopSwing");
        lineObject.positionCount = 0;
        Destroy(joint);
        isSwinging = false;
        //UIManager.Instance.SetActiveDetachText(false);
    }

    void DrawRope()
    {
        // if player not swinging then dont draw rope
        if (!joint) return;
        lineObject.SetPosition(0, swingObs.position);
        lineObject.SetPosition(1, arm.position);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    ISwinging swing = other.gameObject.GetComponent<ISwinging>();
    //    if (swing != null)
    //    {
    //        isTrigger = true;
    //        if(!isSwinging)
    //        {
    //            //Debug.Log(isSwinging);
    //            //UIManager.Instance.SetActiveDetachText(false);
    //            //UIManager.Instance.SetActiveAttachText(true);
    //        }
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    ISwinging swing = other.gameObject.GetComponent<ISwinging>();
    //    if (swing != null)
    //    {
    //        if (isSwinging)
    //        {


    //            //UIManager.Instance.SetActiveAttachText(false);
    //            //UIManager.Instance.SetActiveDetachText(true);
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    ISwinging swing = other.gameObject.GetComponent<ISwinging>();
    //    if (swing != null)
    //    {
    //        isTrigger = false;
    //    }
    //}

    //void DisableTipsText()
    //{
    //    tips.enabled = false;
    //}

    //IEnumerator disableDetach()
    //{
    //    yield return new WaitForSeconds(2);
    //    detach.enabled = false;
    //}
    //void DetachText()
    //{
    //    detach.enabled = true;
    //    StartCoroutine(disableDetach());
    //    //Invoke("disableDetachText", 2);
    //}
}
