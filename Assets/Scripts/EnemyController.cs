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
    Animator animator;
    bool agressive = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
        verticalTimer = 3 * changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!agressive)
        {
            return;
        }
    }

    void FixedUpdate()
    {
        if(!agressive)
        {
            return;
        }
        timer -= Time.deltaTime;
        verticalTimer -= Time.deltaTime;
        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        // if(verticalTimer < 0)
        // {
        //     vertical = !vertical;
        //     verticalTimer = 3 * changeTime;
        // }
        MoveEnemy();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("je suis entré dans quelque chose");
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
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + speed * Time.deltaTime * direction;
        } else 
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x = position.x + speed * Time.deltaTime * direction;
        }
        rigidbody2D.MovePosition(position);
    }

    public void Fix()
    {
        agressive = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
