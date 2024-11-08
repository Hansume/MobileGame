using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance instance;
    public PlayerStats playerStats;

    public GameObject player;
    public Transform playerTransform;

    private void Awake()
    {
        instance = this;
        playerStats = GetComponent<PlayerStats>();
    }

    public string GetLayerName()
    {
        return player.GetComponent<SpriteRenderer>().sortingLayerName;
    }

    public void TeleportPlayer(Vector3 newPosition)
    {
        player.transform.position = newPosition;
    }

    public void DamagePlayer(int damage)
    {
        playerStats.currentHealth -= damage;
    }

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
