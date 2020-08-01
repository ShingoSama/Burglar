using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform gearL;
    public Transform gearR;
    public bool isVertical;
    public float movementSpeed;
    private int directionMovement;
    public LayerMask gearLayer;
    private void Awake()
    {
        directionMovement = -1;
    }
    void Update()
    {
        if (GearDetected())
        {
            Flip();
        }
        Moving();
    }
    private void Moving()
    {
        if (isVertical)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + movementSpeed * Time.deltaTime * directionMovement);
        }
        else
        {
            transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime * directionMovement, transform.position.y);
        }
    }
    public void Flip()
    {
        directionMovement *= -1;
    }
    private bool GearDetected()
    {
        if (isVertical)
        {
            return Physics2D.Raycast(transform.position, transform.up, (directionMovement * 0.3f), gearLayer);
        }
        else
        {
            return Physics2D.Raycast(transform.position, transform.right, (directionMovement * 1.5f), gearLayer);
        }
    }
    private void OnDrawGizmos()
    {
        if (isVertical)
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + (directionMovement * 0.3f)));
        }
        else
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (directionMovement * 1.5f), transform.position.y));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = this.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
