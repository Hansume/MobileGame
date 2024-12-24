using UnityEngine;

public class Gem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GemCount.instance.IncreaseGemCount();
            Destroy(gameObject);
        }
    }
}
