using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;
    Rigidbody2D rigidbody2D;
    public bool vertical;
    public float changeTime = 2.0f;
    float timer;
    int direction = 1;
    float verticalTimer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        verticalTimer = 3 * changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        verticalTimer -= Time.deltaTime;
        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        if(verticalTimer < 0)
        {
            vertical = !vertical;
            verticalTimer = 3 * changeTime;
        }
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    private void MoveEnemy()
    {
        Vector2 position = rigidbody2D.position;
        if(vertical) 
        {
            position.y = position.y + speed * Time.deltaTime * direction;
        } else 
        {
            position.x = position.x + speed * Time.deltaTime * direction;
        }
        rigidbody2D.MovePosition(position);
    }
}
