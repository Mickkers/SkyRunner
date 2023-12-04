using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Animator animator;
    private SpriteRenderer sr;
    private ParticleSystem ps;
    private GameManager gameManager;
    private AudioController audioController;
    public bool isAlive;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float scoreOnDeath;
    [SerializeField] private float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        rbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        rbody.velocity = new Vector2(-moveSpeed * gameManager.gameSpeed, 0f);

    }


    public IEnumerator Death()
    {
        isAlive = false;
        gameManager.AddScore(scoreOnDeath);
        audioController.EnemySFX();
        ps.Play();
        animator.SetTrigger(AnimationStrings.death);
        float time = 0f;

        while (time < fadeTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / fadeTime);
            Color col = sr.color;
            col.a = alpha;
            sr.color = col;
            time += Time.deltaTime;
            yield return null;
        }
        transform.SetPositionAndRotation(new Vector2(-12, 0), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyTrigger"))
        {
            Destroy(this.gameObject);
        }
    }
}
