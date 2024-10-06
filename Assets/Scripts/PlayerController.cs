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

    Animator animator;
    Vector2 moveDirection = new Vector2(1,0);

    public GameObject projectilePrefab;
    public InputAction launchAction;

    public InputAction talkAction;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        MoveAction.Enable();
        launchAction.Enable();
        talkAction.Enable();
        launchAction.performed += Launch;
        talkAction.performed += FindFriend;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) 
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);
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
            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth/(float)maxHealth);
    }

    public bool CheckIfMaxHealth(){
        return health < maxHealth;
    }

    void Launch(InputAction.CallbackContext context)
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectil projectile = projectileObject.GetComponent<Projectil>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }

    void FindFriend(InputAction.CallbackContext context)
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if(hit.collider != null)
        {
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if(character != null)
            {
                UIHandler.instance.DisplayDialogue();
            }
        }
    }
}
