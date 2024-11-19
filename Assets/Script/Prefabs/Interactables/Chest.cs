using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactables
{
    [SerializeField] private GameObject gemPrefab;

    public override void Interact()
    {
        base.Interact();
        GameObject gem = Instantiate(gemPrefab, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
    }
}
