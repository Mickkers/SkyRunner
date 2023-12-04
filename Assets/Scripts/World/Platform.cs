using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D rbody;
    private GameManager gameManager;

    [SerializeField] private float platformSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rbody.velocity = new Vector2(-platformSpeed * gameManager.gameSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyTrigger"))
        {
            Destroy(this.gameObject);
        }
    }
}
