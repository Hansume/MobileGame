using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullets : MonoBehaviour
{
    public float speed = 3f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, -1, 0) * speed;
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(Random.Range(2,5));
        Destroy(gameObject);
    }
}
