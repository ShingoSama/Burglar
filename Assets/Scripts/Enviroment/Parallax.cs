using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lengthX, lengthY, startposX, startposY;
    public GameObject cam;
    public float parallaxEffectX;
    public float parallaxEffectY;
    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float tempX = (cam.transform.position.x * (1 - parallaxEffectX));
        float distX = (cam.transform.position.x * parallaxEffectX);
        float tempY = (cam.transform.position.x * (1 - parallaxEffectY));
        float distY = (cam.transform.position.y * parallaxEffectY);

        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

        if (tempX > startposX + lengthX)
        {
            startposX += lengthX;
        }
        else if (tempX < startposX - lengthX)
        {
            startposX -= lengthX;
        }

        if (tempY > startposY + lengthY)
        {
            startposY += lengthY;
        }
        else if (tempY < startposY - lengthY)
        {
            startposY -= lengthY;
        }
    }
}
