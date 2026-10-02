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

    // =========================
    // DAMAGE SOUND
    // =========================

    public AudioSource audioSource;
    public AudioClip damageSound;

    // =========================
    // DASH
    // =========================

    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 5f;

    public Slider dashBar;

    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    private Vector2 lastMoveDirection = Vector2.right;
    private bool isDashing = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHP = maxHP;

        UpdateHPBar();

        // ตั้งค่า Dash Bar
        if (dashBar != null)
        {
            dashBar.maxValue = dashCooldown;
            dashBar.value = dashCooldown;
        }
    }

    void Update()
    {
        // =========================
        // MOVEMENT
        // =========================

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

        // จำทิศทางล่าสุด
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;
        }

        // Player Animation
        animator.SetBool(
            "IsPlayerRun",
            moveInput != Vector2.zero
        );

        // Flip Player
        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        // =========================
        // DASH COOLDOWN
        // =========================

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;

            if (dashCooldownTimer < 0f)
                dashCooldownTimer = 0f;
        }

        // =========================
        // DASH UI
        // =========================

        if (dashBar != null)
        {
            dashBar.maxValue = dashCooldown;

            dashBar.value =
                dashCooldown - dashCooldownTimer;
        }

        // =========================
        // DASH INPUT
        // =========================

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (dashCooldownTimer <= 0f &&
                !isDashing)
            {
                StartDash();
            }
        }

        // =========================
        // DASH TIMER
        // =========================

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }
    }

    void FixedUpdate()
    {
        // =========================
        // NORMAL MOVE / DASH MOVE
        // =========================

        if (isDashing)
        {
            rb.linearVelocity =
                lastMoveDirection * dashSpeed;
        }
        else
        {
            rb.linearVelocity =
                moveInput * moveSpeed;
        }

        // =========================
        // PLAYER BOUNDARY
        // =========================

        Vector3 position = transform.position;

        position.x = Mathf.Clamp(
            position.x,
            -8.5f,
            8.5f
        );

        position.y = Mathf.Clamp(
            position.y,
            -4.5f,
            4.5f
        );

        transform.position = position;
    }

    // =========================
    // START DASH
    // =========================

    void StartDash()
    {
        isDashing = true;

        dashTimer = dashDuration;

        dashCooldownTimer = dashCooldown;

        // ทะลุ Enemy
        IgnoreEnemyCollisions(true);

        Debug.Log("DASH!");
    }

    // =========================
    // END DASH
    // =========================

    void EndDash()
    {
        isDashing = false;

        // กลับมาชน Enemy ได้
        IgnoreEnemyCollisions(false);
    }

    // =========================
    // IGNORE ENEMY COLLISION
    // =========================

    void IgnoreEnemyCollisions(bool ignore)
    {
        Collider2D playerCollider =
            GetComponent<Collider2D>();

        if (playerCollider == null)
            return;

        EnemyCrawler[] enemies =
            FindObjectsOfType<EnemyCrawler>();

        foreach (EnemyCrawler enemy in enemies)
        {
            if (enemy == null)
                continue;

            Collider2D enemyCollider =
                enemy.GetComponent<Collider2D>();

            if (enemyCollider != null)
            {
                Physics2D.IgnoreCollision(
                    playerCollider,
                    enemyCollider,
                    ignore
                );
            }
        }
    }

    // =========================
    // TAKE DAMAGE
    // =========================

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        // เสียงโดน Damage
        if (audioSource != null &&
            damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        if (currentHP < 0)
            currentHP = 0;

        Debug.Log(
            "Player HP: " + currentHP
        );

        UpdateHPBar();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    // =========================
    // HP BAR
    // =========================

    public void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.maxValue = maxHP;
            hpBar.value = currentHP;
        }
    }

    // =========================
    // DIE
    // =========================

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