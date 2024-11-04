using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        animator.SetBool("isOpened", true);
        Debug.Log("1");
    }

    public void ResetInteract()
    {
        animator.SetBool("isOpened", false);
    }
    protected virtual void SpecialInteraction()
    {

    }
}
