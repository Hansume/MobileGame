using UnityEngine;

public class Statue : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject gemPrefab;

    public void Interact()
    {
        GameObject gem = Instantiate(gemPrefab, transform.position + new Vector3(1, 0.5f, 0), Quaternion.identity);
        gem.GetComponent<SpriteRenderer>().sortingLayerName = "Layer 3";
    }

    public void ResetInteract()
    {

    }
}
