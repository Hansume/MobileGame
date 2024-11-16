using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBullets : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Deactive", 2f);
    }

    void Deactive()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInstance.instance.DamagePlayer(1);
            gameObject.SetActive(false);
        }
    }
}
