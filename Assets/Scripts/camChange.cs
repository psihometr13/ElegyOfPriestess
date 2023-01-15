using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class camChange : MonoBehaviour
{
 
    //public CameraCont cameraCont;

    private Animator animator;
    
    private void Awake()
    {

        animator = GetComponent<Animator>();
        //cameraCont = GetComponentInChildren<CameraCont>();

    }

    void Update()
    {
        animator.Play("Camera" + (Upd_PlayerControl.Instance.currentRoom + 1).ToString());
    }

}
