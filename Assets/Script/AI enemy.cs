using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIenemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public float damageAmount = 10f;
    public Transform player;
    public LayerMask groundLayer;
    public float health = 100f;
    public int damage = 1;


    private Rigidbody2D rb;
    private bool isAttacking = false;
    private Transform Player; 


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && !isAttacking)
        {
            ChasePlayer();
        }

        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        if (player.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else if (player.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }

    void AttackPlayer()
    {
        isAttacking = true;
        // Trigger serangan musuh (misalnya memberikan damage ke pemain)
        // Logic damage bisa ditambahkan di sini jika dibutuhkan
        Debug.Log("Musuh menyerang pemain!");
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Musuh terkena damage! Sisa health: " + health);

        if (health <= 0)
        {
            Die();  // Jika darah habis, musuh mati
        }
    }

    // Fungsi untuk menangani kematian musuh
    void Die()
    {
        Debug.Log("Musuh mati!");
        // Di sini Anda bisa menambahkan logika untuk menghilangkan musuh (misalnya destroy)
        Destroy(gameObject);  // Menghancurkan objek musuh
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
             // Ambil komponen PlayerHealth dari objek pemain
            AIenemy playerHealth = collision.gameObject.GetComponent<AIenemy>();

            // Jika PlayerHealth ditemukan, beri damage pada pemain
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);  // Mengurangi darah pemain
            }
        }
    }
}

