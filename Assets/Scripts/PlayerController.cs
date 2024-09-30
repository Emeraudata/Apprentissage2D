using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    Rigidbody2D rigidbody2D;
    Vector2 move;
    public float speed = 3.0f;
    public int maxHealth = 5;
    public int currentHealth = 1;
    public int health { get { return currentHealth; } }

    public float timeInvicible = 2.0f;
    bool isInvicible;
    float damageCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        if(isInvicible)
        {
            damageCooldown -= Time.deltaTime;
            if(damageCooldown < 0)
            {
                isInvicible = false;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position + move * speed * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount){
        if(amount < 0)
        {
            if(isInvicible) 
            {
                return;
            }
            isInvicible = true;
            damageCooldown = timeInvicible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public bool CheckIfMaxHealth(){
        return health < maxHealth;
    }
}
