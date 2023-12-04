using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        cam.ViewportToWorldPoint(Vector3.one);
        transform.localScale = cam.ViewportToWorldPoint(Vector3.one) * 2 + new Vector3(1, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
