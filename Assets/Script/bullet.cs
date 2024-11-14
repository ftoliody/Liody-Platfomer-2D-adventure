using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 10f;               // Kecepatan peluru
    public float damage = 20f;              // Jumlah damage yang diberikan peluru
    public float lifetime = 5f;             // Waktu hidup peluru sebelum hancur
    public LayerMask enemyLayer;            // Layer untuk musuh (untuk mendeteksi musuh)

    private Rigidbody2D rb;                 // Rigidbody2D untuk menggerakkan peluru

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);     // Menghancurkan peluru setelah waktu tertentu
    }

    void Update()
    {
        // Menggerakkan peluru ke arah kanan atau kiri
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Mengecek apakah peluru mengenai musuh
        if (other.CompareTag("Enemy"))
        {
            // Mendapatkan komponen EnemyHealth untuk mengurangi HP musuh
            AIenemy enemyHealth = other.GetComponent<AIenemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // Mengurangi HP musuh
            }

            Destroy(gameObject);  // Menghancurkan peluru setelah mengenai musuh
        }
    }
}
