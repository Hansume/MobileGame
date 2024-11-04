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
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        spriteRenderer.sortingLayerName = player.GetLayerName();
    }
}
