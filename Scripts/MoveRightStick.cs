using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightStick : MonoBehaviour
{
    public float speed = 40f;
    //public float boundX = 20.25f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = this.transform.position;
    }

    public void ResetStick()
    {
        transform.position = startPos;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        /*var pos = transform.position;

        if (pos.x > boundX)
        {
            pos.x = boundX;
        }else if (pos.x < -boundX)
        {
            pos.x = -boundX;
        }

        transform.position = pos;*/
    }
}
