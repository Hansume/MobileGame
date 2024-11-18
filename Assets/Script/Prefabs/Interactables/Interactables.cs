using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    private Animator animator;
    private bool isInteracted = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInteracted)
        {

        }
        else
        {

        }
    }

    public void Interact()
    {
        isInteracted = true;
    }
}
