using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask interactablesLayer;
    public float radius;

    private Collider2D interactables;
    private Collider2D prevInteractables;

    // Start is called before the first frame update
    void Start()
    {
        interactables = null;
        prevInteractables = null;
    }

    // Update is called once per frame
    void Update()
    {
        interactables = Physics2D.OverlapCircle(transform.position, radius, interactablesLayer);
        if (interactables == null && prevInteractables != null)
        {
            prevInteractables.GetComponent<Interactables>().ResetInteract();
            prevInteractables.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void Interact()
    {
        if (interactables != null)
        {
            prevInteractables = interactables;
            interactables.GetComponent<BoxCollider2D>().enabled = false;
            interactables.GetComponent<Interactables>().Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}