using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftStick : MonoBehaviour
{
    public float speed = 40f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    public void ResetStick()
    {
        transform.position = startPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
