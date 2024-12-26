using UnityEngine;

public class AttackAnim : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private bool isFired = false;

    void Start()
    {
        playerAttack = PlayerInstance.instance.player.GetComponent<PlayerAttack>();
    }

    private void FireArrow()
    {
        if (!isFired)
        {
            isFired = true;
            playerAttack.FireArrow();
        }
    }

    private void ResetAttack()
    {
        isFired = false;
        playerAttack.ResetAttack();
    }
}
