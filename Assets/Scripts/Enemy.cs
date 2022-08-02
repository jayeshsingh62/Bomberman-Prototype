using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyCount;
    public float accelerationTime = 3f;
    [Range(0f, 1f)]
    public float maxSpeed = 1.0f;
    [Range(0f, 1f)]
    public float awaySpeed = 1.0f;
    private float xRange = 7f;
    private float yRange = 4.7f;
    private Vector2 movement;
    private float timeLeft;
    private Rigidbody2D rb;
    public GameObject player;
    public PlayerController playerController;
    private GameManager gameManager;
    private Vector2 m_Velocity;
    private Vector3 targetVelocity;
    private float MovementSmoothing = 0.05f;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector2(-xRange, transform.position.y);
            targetVelocity = -targetVelocity;
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector2(xRange, transform.position.y);
            targetVelocity = -targetVelocity;
        }

        if (transform.position.y > yRange)
        {
            transform.position = new Vector2(transform.position.x, yRange);
            targetVelocity = -targetVelocity;
        }

        if (transform.position.y < -yRange)
        {
            transform.position = new Vector2(transform.position.x, -yRange);
            targetVelocity = -targetVelocity;
        }
    }

    void FixedUpdate()
    {
        Movement();
        MoveDirection();
    }

    void Movement()
    {
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, MovementSmoothing);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("DesBlock") || collision.gameObject.CompareTag("IndBlock"))
        {
            targetVelocity = -targetVelocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            Destroy(gameObject);
            gameManager.UpdateScore(100);
        }
    }

    void MoveDirection()
    {
        int h = Random.Range(-1,2) ;
        int v = Random.Range(-1,2);

        if (Mathf.Abs(h) > Mathf.Abs(v))
        {
            v = 0;
        }
        else
        {
            h = 0;
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            targetVelocity = new Vector2(h * 10f * maxSpeed, v * 10f * maxSpeed);
            timeLeft += accelerationTime;
        }

        if (enemyCount < 3)
        {
            targetVelocity = new Vector2(playerController.h * 10f * awaySpeed, playerController.v * 10f * awaySpeed);
        }
    }
}
