using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishPlatform : MonoBehaviour
{
    private Animator animator;
    private float nextTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (nextTime < Time.time)
            {
                nextTime = Time.time + 2f;
                PlatformManager.Instance.StartCoroutine("ReSpawnPlatforms", new Vector2(transform.position.x, transform.position.y));
                animator = gameObject.GetComponent<Animator>();
                animator.SetTrigger("Vanish");
                StartCoroutine(TimetoWait());
            }
        }
    }
    IEnumerator TimetoWait()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }
}
