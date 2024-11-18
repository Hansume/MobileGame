using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    PlayerInstance player;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = PlayerInstance.instance;
    }

    private void Update()
    {
        spriteRenderer.sortingLayerName = player.GetLayerName();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
