using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public int maxHP = 100;
    public int currentHP;

    public Slider hpBar;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Awake()
{
    spriteRenderer = GetComponent<SpriteRenderer>();
}
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHP = maxHP;

        UpdateHPBar();
    }

    void Update()
    {
        moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            moveInput.y += 1;

        if (Keyboard.current.sKey.isPressed)
            moveInput.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            moveInput.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            moveInput.x += 1;

        moveInput = moveInput.normalized;

        animator.SetBool("IsPlayerRun", moveInput != Vector2.zero);
            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, -8.5f, 8.5f);
        position.y = Mathf.Clamp(position.y, -4.5f, 4.5f);

        transform.position = position;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP < 0)
            currentHP = 0;

        Debug.Log("Player HP: " + currentHP);

        UpdateHPBar();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.maxValue = maxHP;
            hpBar.value = currentHP;
        }
    }

    void Die()
    {
        Debug.Log("PLAYER DEAD");

        rb.linearVelocity = Vector2.zero;

        GameOverManager gameOverManager =
            FindObjectOfType<GameOverManager>();

        if (gameOverManager != null)
        {
            gameOverManager.GameOver();
        }

        gameObject.SetActive(false);
    }
}