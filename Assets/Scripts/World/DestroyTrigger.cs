using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Destroying " + collision.gameObject.name);
        Destroy(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Destroying " + collision.gameObject.name);
        Destroy(collision.gameObject);
    }
}
