using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private ParticleSystem ps;
    private GameManager gameManager;
    private AudioController audioController;

    [Header("Health Attributes")]
    [SerializeField] private int currHealth;
    [SerializeField] private int maxHealth;

    [Header("Health UI")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthIcons;

    [Header("Death Timeout")]
    [SerializeField] private float fadeTime;

    private bool canTakeDamage;
    private float iconStartingWidth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;

        currHealth = maxHealth;
        canTakeDamage = true;
        iconStartingWidth = healthIcons.rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        UpdateHealthUI();
        if(currHealth == 0)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        float time = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        while(time < fadeTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / fadeTime);
            Color col = sr.color;
            col.a = alpha;
            sr.color = col;
            time += Time.deltaTime;
            yield return null;
        }

        gameManager.Gameover();
    }

    private void UpdateHealthUI()
    {
        healthText.text = "" + currHealth;
        Vector2 _temp = healthIcons.rectTransform.sizeDelta;
        _temp.x = ((float)currHealth / (float)maxHealth) * iconStartingWidth;
        healthIcons.rectTransform.sizeDelta = _temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.GetComponent<Enemy>().isAlive == false)
            {
                return;
            }
            TakeDamage(1);
        }
        if (collision.gameObject.CompareTag("Crusher"))
        {
            currHealth = 0;
        }
    }

    public void TakeDamage(int value)
    {
        if (!canTakeDamage) return;
        canTakeDamage = false;
        audioController.DamageSFX();
        animator.SetBool(AnimationStrings.isHit, true);
        currHealth -= value;
        ps.Play();
        StartCoroutine(IFrames());
    }

    private IEnumerator IFrames()
    {
        yield return new WaitForSeconds(1.5f);
        canTakeDamage = true;
        animator.SetBool(AnimationStrings.isHit, false);
    }
}
