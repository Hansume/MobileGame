using UnityEngine;

public class Door : Interactables
{
    public bool canTeleport = false;
    public GameObject teleportPosition;

    public override void Interact()
    {
        base.Interact();
        canTeleport = true;
    }

    public override void ResetInteract()
    {
        base.ResetInteract();
        canTeleport = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (canTeleport)
        {
            PlayerInstance.instance.TeleportPlayer(teleportPosition.transform.position);
        }
    }
}
