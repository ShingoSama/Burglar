using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKnockback : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rigidbody2D.velocity = new Vector2(3.5f,3.5f);
        }
    }
}
