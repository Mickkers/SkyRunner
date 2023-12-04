using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    private Rigidbody2D rbody;

    [SerializeField] private float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rbody.velocity = new Vector2(projectileSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.isAlive = false;
                StartCoroutine(enemy.Death());
            }
        }

        if (collision.gameObject.CompareTag("DestroyTrigger"))
        {
            Destroy(this.gameObject);
        }
    }

    public void destroyBall()
    {

    }
}
