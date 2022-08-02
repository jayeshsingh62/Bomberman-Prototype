using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float v;
    public float h;
    [Range(0f, 1f)]
    public float moveSpeed = 1.0f;
    private float xRange = 7.25f;
    private float yRange = 4.5f;
    private Transform startPosition;
    public GameObject bomb;
    public GameObject explosion;
    public bool bombReady = true;
    public GameManager gameManager;
    private Rigidbody2D playerRb;
    private Vector2 m_Velocity;
    private float MovementSmoothing = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(-7.25f, 4.5f);
        playerRb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector2(-xRange, transform.position.y);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector2(xRange, transform.position.y);
        }

        if (transform.position.y > yRange)
        {
            transform.position = new Vector2(transform.position.x, yRange);
        }

        if (transform.position.y < -yRange)
        {
            transform.position = new Vector2(transform.position.x, -yRange);
        }

        if(bombReady == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var cloneBomb = Instantiate(bomb, transform.position, transform.rotation);
                bombReady = false;
                Vector3 bombPos = bomb.transform.position + this.transform.position;
                Destroy(cloneBomb, 3);
                StartCoroutine(bombExplosion(bombPos));                
            }
        }          
    }
    void Movement()
    { 
         h = Input.GetAxisRaw("Horizontal");
         v = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(h) > Mathf.Abs(v))
        {
            v = 0;
        }
        else
        {
            h = 0;
        }
        Vector3 targetVelocity = new Vector2(h * 10f * moveSpeed, v * 10f * moveSpeed);
        playerRb.velocity = Vector2.SmoothDamp(playerRb.velocity, targetVelocity, ref m_Velocity, MovementSmoothing);

    }

    private void FixedUpdate()
    {
        Movement();
    }
    IEnumerator bombExplosion(Vector3 bombPos)
    {
        yield return new WaitForSeconds(3);
        SpawnExplosion(bombPos);
        bombReady = true;
    }

    public void SpawnExplosion(Vector3 bombPos)
    {
        var explosion1 = Instantiate(explosion, bombPos + new Vector3Int(-1, 1, 0), bomb.transform.rotation);
        var explosion2 = Instantiate(explosion, bombPos + new Vector3Int(0, 1, 0), bomb.transform.rotation);
        var explosion3 = Instantiate(explosion, bombPos + new Vector3Int(1, 1, 0), bomb.transform.rotation);
        var explosion4 = Instantiate(explosion, bombPos + new Vector3Int(- 1, 0, 0), bomb.transform.rotation);
        var explosion5 = Instantiate(explosion, bombPos + new Vector3Int(0, 0, 0), bomb.transform.rotation);
        var explosion6 = Instantiate(explosion, bombPos + new Vector3Int(1, 0, 0), bomb.transform.rotation);
        var explosion7 = Instantiate(explosion, bombPos + new Vector3Int(- 1, - 1, 0), bomb.transform.rotation);
        var explosion8 = Instantiate(explosion, bombPos + new Vector3Int(0, - 1, 0), bomb.transform.rotation);
        var explosion9 = Instantiate(explosion, bombPos + new Vector3Int(1, - 1, 0), bomb.transform.rotation);

        GameObject[] explosions = new GameObject[] { explosion1, explosion2, explosion3, explosion4, explosion5, explosion6, explosion7, explosion8, explosion9 };
        foreach(GameObject i in explosions)
        {
            if (i.transform.position.x < -xRange)
            {
                Destroy(i);
            }

            if (i.transform.position.x > xRange)
            {
                Destroy(i);
            }

            if (i.transform.position.y > yRange)
            {
                Destroy(i);
            }

            if (i.transform.position.y < -yRange)
            {
                Destroy(i);
            }
        }
        Destroy(explosion1,1);
        Destroy(explosion2, 1);
        Destroy(explosion3, 1);
        Destroy(explosion4, 1);
        Destroy(explosion5, 1);
        Destroy(explosion6, 1);
        Destroy(explosion7, 1);
        Destroy(explosion8, 1);
        Destroy(explosion9, 1);       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }
    }
}
