using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector2D;
    public float waitTime;
    void Start()
    {
        effector2D = gameObject.GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && Input.GetAxisRaw("Vertical") < 0f )
        {
            if (waitTime < Time.time)
            {
                StartCoroutine(FallTimer());
                waitTime = Time.time + 0.5f;
            }
        }
    }
    IEnumerator FallTimer()
    {
        effector2D.rotationalOffset = 180f;
        yield return new WaitForSeconds(0.5f);
        effector2D.rotationalOffset = 0f;
    }
}
