using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask interactablesLayer;
    public float radius;

    private Collider2D interactables;
    private Collider2D prevInteractables;

    void Start()
    {
        interactables = null;
        prevInteractables = null;
    }

    void Update()
    {
        interactables = Physics2D.OverlapCircle(transform.position, radius, interactablesLayer);
        if (interactables == null && prevInteractables != null)
        {
            prevInteractables.GetComponent<IInteract>().ResetInteract();
        }
    }

    public void Interact()
    {
        if (interactables != null)
        {
            prevInteractables = interactables;
            interactables.GetComponent<IInteract>().Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}