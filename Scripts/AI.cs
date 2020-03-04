using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    //public Ball ball;
    public Ball Ball;
    //public float speed = 30;
    //public float lerpSpeed = 1f;

    private Transform currentTransform;
    private int speed = 30;
    private Rigidbody2D rigidbody2D;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        currentTransform = this.transform;
    }

    public void ResetStick()
    {
        transform.position = startPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (ball.transform.position.y > transform.position.y)
        {
            if (rigidbody2D.velocity.y < 0)
            {
                rigidbody2D.velocity = Vector2.zero;

                rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity, Vector2.up * speed, lerpSpeed * Time.deltaTime);
            }else if (ball.transform.position.y < transform.position.y)
            {
                if (rigidbody2D.velocity.y > 0)
                {
                    rigidbody2D.velocity = Vector2.zero;

                    rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity, Vector2.down * speed, lerpSpeed * Time.deltaTime);
                }
                else
                {
                    rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity, Vector2.zero * speed, lerpSpeed * Time.deltaTime);
                }
            }
        }*/

        currentTransform = this.transform;
        if (currentTransform.position.x < Ball.transform.position.x)
        {
            if (currentTransform.position.y < Ball.transform.position.y)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed;
            }else if (currentTransform.position.y > Ball.transform.position.y)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0) * speed;
            }
        }
    }
}
