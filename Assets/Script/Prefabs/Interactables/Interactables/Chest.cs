using UnityEngine;

public class Chest : Interactables
{
    [SerializeField] private GameObject gemPrefab;
    private bool isInstantiated = false;

    public override void Interact()
    {
        base.Interact();
        if (!isInstantiated)
        {
            GameObject gem = Instantiate(gemPrefab, transform.position + new Vector3(1, 0.5f, 0), Quaternion.identity);
            isInstantiated = true;
        }
    }
}
