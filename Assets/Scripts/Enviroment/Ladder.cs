using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private PlatformEffector2D effector2D;
    private EdgeCollider2D edgeCollider2D;
    private void Start()
    {
        effector2D = gameObject.GetComponent<PlatformEffector2D>();
        edgeCollider2D = gameObject.GetComponent<EdgeCollider2D>();
    }
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetAxis("Vertical") > 0f)
            {
                collision.gameObject.GetComponent<PlayerController>().ClimbLadder();
                effector2D.rotationalOffset = 0f;
                edgeCollider2D.enabled = false;
                collision.gameObject.GetComponent<PlayerController>().ClimbLadderMove(1f);
            }
            if (Input.GetAxis("Vertical") < 0f)
            {
                collision.gameObject.GetComponent<PlayerController>().ClimbLadder();
                Debug.Log("quierobajar");
                effector2D.rotationalOffset = 180f;
                edgeCollider2D.enabled = false;
                collision.gameObject.GetComponent<PlayerController>().ClimbLadderMove(-1f);
            }
            if (Input.GetAxisRaw("Vertical") == 0f && collision.gameObject.GetComponent<PlayerController>().GetInLadder())
            {
                collision.gameObject.GetComponent<PlayerController>().ClimbLadder();
                collision.gameObject.GetComponent<PlayerController>().ClimbLadderMove(0f);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            edgeCollider2D.enabled = true;
            collision.gameObject.GetComponent<PlayerController>().EndClimbLadder();
        }
    }
}
