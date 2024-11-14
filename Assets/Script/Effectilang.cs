using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectilang : MonoBehaviour
{
  public float disappearDelay = 1f; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Invoke("DisablePlatform", disappearDelay);
        }
    }

    void DisablePlatform()
    {
        gameObject.SetActive(false); 
    }
}