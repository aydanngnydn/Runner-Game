using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Dreamteck.Splines;
using UnityEngine;

public class MovementDeneme : MonoBehaviour
{
    [SerializeField] float xMin, xMax;
     public Animator anim;
     public SplineFollower spF;
     public CinemachineSwitcher cs;
     public CinemachineVirtualCamera failCam, startCam;
     [SerializeField] private float speed;
     [SerializeField] private Transform target;
     private void Start()
     {
         phy(true);
     }
     private void Update()
     {
         anim.SetFloat("speed", Mathf.Abs(spF.followSpeed));
         anim.SetBool("enabled", GameManager.Instance.currentState == States.Win);
         Vector2 xPos = new Vector2(Mathf.Clamp(transform.localPosition.x, xMin, xMax), transform.position.y);
         transform.localPosition = xPos;

         if (GameManager.Instance.currentState == States.Playing)
         {
             Move();
         }
     }

     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("cube"))
         {
             Stop();
             GameManager.Instance.currentState = States.Fail;
         }
     }
     
     private void phy(bool situation)
     {
         Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
         foreach (Rigidbody phychildren in rb)
         {
             phychildren.isKinematic = situation;
         }
     }

     public void Stop()
     {
         anim.enabled = false;
         phy(false);
         spF.follow = false;
     }

     public void Move()
     {
         //cs.SwitchPri(startCam, failCam);
         spF.followSpeed = speed;
     }
}
