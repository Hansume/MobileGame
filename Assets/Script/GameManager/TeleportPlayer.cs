using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public GameObject TelePosition;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerInstance.instance.TeleportPlayer(TelePosition.transform.position);
    }
}
