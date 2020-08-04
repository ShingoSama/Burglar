using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishPlatform : MonoBehaviour
{
    private Animator animator;
    private float nextTime;
    public float timeToRespawn;
    public float timeToDestroy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (nextTime < Time.time)
            {
                nextTime = Time.time + timeToRespawn;
                PlatformManager.Instance.ReSpawn(new Vector2(transform.position.x, transform.position.y), timeToRespawn, timeToDestroy);
                animator = gameObject.GetComponent<Animator>();
                animator.SetTrigger("Vanish");
                StartCoroutine(TimetoWait());
            }
        }
    }
    IEnumerator TimetoWait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this.gameObject);
    }
}
