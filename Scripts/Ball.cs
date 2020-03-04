using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    public float speed = 80;
    public GameObject particleEffect;

    private new Rigidbody2D rigidbody2D;
    private GameManager gameManager;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        Invoke("GoBall", 2);

        audioSource = GetComponent<AudioSource>();

        //GetComponent<Rigidbody2D>().velocity = Vector2.zero * speed;
    }

    void GoBall()
    {
        float rand = Random.Range(0, 2);

        if (rand < 1)
        {
            rigidbody2D.AddForce(new Vector2(2, -1));

            speed = 40;
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(-2, -1));

            speed = 40;
        }
    }

    void ResetBall()
    {
        rigidbody2D.velocity = Vector2.zero;

        transform.position = Vector2.zero;
    }

    public void ClearPauseMenu()
    {
        //gameman.hidepaused
        gameManager.hidePaused();
    }

    void RestartGame()
    {
        ResetBall();

        Invoke("GoBall", 3);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "stick left" || col.gameObject.name == "player stick left")
        {
            audioSource.Play(0);

            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

            Vector2 dir = new Vector2(1, y).normalized;

            GetComponent<Rigidbody2D>().velocity = dir * (speed += 10f);
        }

        if (col.gameObject.name == "stick right")
        {
            audioSource.Play(0);

            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

            Vector2 dir = new Vector2(-1, y).normalized;

            GetComponent<Rigidbody2D>().velocity = dir * (speed += 10f);
        }

        if (col.gameObject.tag == "Wall")
        {
            Instantiate(particleEffect, transform.position, Quaternion.identity);
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 stickPos, float stickHeight)
    {
        return (ballPos.y - stickPos.y) / stickHeight;
    }
}
