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
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CameraCont.Instance.check)
            {
                SwitchState();
               
            }
        }

    }


    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        check = true;
    //    }

    //}
    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        if (collision.gameObject == check)
    //        {
    //            check = false;
    //        }
    //    }

    //}

    private void SwitchState()
    {
        //state1 (room1-2)
        if (CameraCont.Instance.name == "colliderForCamera2(1)") animator.Play("Camera2");
        else if (CameraCont.Instance.name == "colliderForCamera1(2)") animator.Play("Camera1");

        //state2 (room2-3)
        else if (CameraCont.Instance.name == "colliderForCamera3(2)") animator.Play("Camera3");
        else if (CameraCont.Instance.name == "colliderForCamera2(3)") animator.Play("Camera2");

        //state3 (room3-4)
        else if (CameraCont.Instance.name == "colliderForCamera4(3)")  animator.Play("Camera4");
        else if (CameraCont.Instance.name == "colliderForCamera3(4)") animator.Play("Camera3");

        //state4 (room4-5)
        else if (CameraCont.Instance.name == "colliderForCamera4(5)") animator.Play("Camera4");
        else if (CameraCont.Instance.name == "colliderForCamera5(4)") animator.Play("Camera5");

        //state5 (room5-6)
        else if (CameraCont.Instance.name == "colliderForCamera5(6)") animator.Play("Camera5");
        else if (CameraCont.Instance.name == "colliderForCamera6(5)") animator.Play("Camera6");

        //state5 (room6-7)
        else if (CameraCont.Instance.name == "colliderForCamera6(7)") animator.Play("Camera6");
        else if (CameraCont.Instance.name == "colliderForCamera7(6)") animator.Play("Camera7");

        //state5 (room3-secret room)
        else if (CameraCont.Instance.name == "colliderForCamera3(8)") animator.Play("Camera3");
        else if (CameraCont.Instance.name == "colliderForCamera8(3)") animator.Play("Camera8");

        //state6 (room4-exit)
        else if (CameraCont.Instance.name == "colliderForCamera4(9)") animator.Play("Camera4");
        else if (CameraCont.Instance.name == "colliderForCamera9(4)") animator.Play("Camera9");
    }
}
