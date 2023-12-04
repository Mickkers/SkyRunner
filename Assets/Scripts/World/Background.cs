using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    private float spriteWidth;

    [SerializeField] GameObject[] backgrounds;
    [SerializeField] float[] parallaxFactors;

    private void Start()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

        spriteRenderer = backgrounds[0].GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 3; i++)
        {
            Vector3 _temp = backgrounds[i].transform.position;
            if (_temp.x > -spriteWidth)
            {

                if (gameManager is null)
                {
                    _temp.x -= parallaxFactors[i] * Time.deltaTime;
                }
                else
                {
                    _temp.x -= gameManager.gameSpeed * parallaxFactors[i] * Time.deltaTime;
                }
                backgrounds[i].transform.position = _temp;
                continue;
            }
            backgrounds[i].transform.position = Vector3.zero;
        }
    }
}
