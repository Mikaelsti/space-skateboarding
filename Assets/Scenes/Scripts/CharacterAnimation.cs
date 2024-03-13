using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    private bool isRotating = false;

    void Update()
    {
        if (controller.isRotatingLeft)
        {
            animator.SetBool("StopRotLeft", false);
            animator.SetBool("IsRotatingLeft", true);
        }
        else {
            animator.SetBool("StopRotLeft", true);
            animator.SetBool("IsRotatingLeft", false); }

        if (controller.isRotatingRight)
        {
            animator.SetBool("StopRotRight", false);
            animator.SetBool("IsRotatingRight", true);
        }
        else {
            animator.SetBool("StopRotRight", true);
            animator.SetBool("IsRotatingRight", false); }
    }
}
