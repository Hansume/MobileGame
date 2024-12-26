using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField] private GameObject chest;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            chest.SetActive(true);
        }
    }
}
