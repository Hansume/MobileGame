using UnityEngine;

public class Interactables : MonoBehaviour, IInteract
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
            animator.SetBool("isOpened", true);
        }
        else
        {
            animator.SetBool("isOpened", false);
        }
    }

    public virtual void Interact()
    {
        isInteracted = true;
    }
    
    public virtual void ResetInteract()
    {
        isInteracted = false;
    }
}
