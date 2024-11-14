using System.Collections;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float slideSpeed = 7f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public int maxHealth = 3; // Number of hearts
    public int currentHealth; // Current health
    public Image heartFullImage; // Full heart UI image
    public Image heartEmptyImage; // Empty heart UI image
    public GameObject gameOverPanel; // Game Over UI panel
    

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isSliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameOverPanel.SetActive(false);
        currentHealth = maxHealth; // Initialize health
        UpdateHeartUI(); // Update UI
    }

    private void Update()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");

        // Run
        if (moveDirection != 0)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(moveDirection, 1, 1); // Flip character based on direction
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
        }

        // Slide
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isSliding)
        {
            StartCoroutine(Slide(moveDirection));
        }

        // Shoot
        if (Input.GetMouseButtonDown(1)) // Right-click mouse
        {
            Shoot();
            animator.SetTrigger("Shoot");
        }

        // Update grounded status for animations
        animator.SetBool("isGrounded", isGrounded);

        UpdateHeartUI(); // Update health UI every frame
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
             animator.SetBool("Jump", true);
        }
    }

    private IEnumerator Slide(float direction)
    {
        isSliding = true;
        rb.velocity = new Vector2(direction * slideSpeed, rb.velocity.y);
        animator.SetTrigger("Slide");
        yield return new WaitForSeconds(0.5f); // Duration of the slide
        isSliding = false;
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHeartUI(); // Update health UI after taking damage
    }

    // Update heart UI
    void UpdateHeartUI()
    {
        // Display heart images based on current health
        heartFullImage.enabled = currentHealth > 0;
        heartEmptyImage.enabled = currentHealth <= 0;
    }

    // Handle player death
    void Die()
    {
        Debug.Log("Player died!");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        // Add logic to handle death, such as disabling the player or restarting the scene
        // Example: gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Check if colliding with an enemy
        {
            int damage = 1; // Set the damage value as needed
            TakeDamage(damage); // Call TakeDamage method
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // Button functionality to exit the game
    public void ExitGame()
    {
        Application.Quit(); // Exit the game
    }
}