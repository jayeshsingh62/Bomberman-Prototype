using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DesBlock"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("IndBlock"))
        {
            Destroy(gameObject);
        }
    }
}
