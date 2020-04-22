using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyReaction : MonoBehaviour {

    public GameObject P1P2Draw, P1Wins, P2Wins;

    public Rigidbody2D Player1RB2D, Player2RB2D;
    public Vector2 knockbackDirection;

    static float maxHealth = 200;
    public Slider healthBar, healthBar2;
    float health = maxHealth;
    float health2 = maxHealth;
    public bool Player1;
    private bool Player1Dead = false;
    private bool Player2Dead = false;

    void Start()
    {
        P1P2Draw.SetActive(false);
        P1Wins.SetActive(false);
        P2Wins.SetActive(false);
    }

    void Update()
    {
        if (health <= 0 && health2 <= 0)
        {
            Debug.Log("Draw");
            Time.timeScale = 0f;
            P1P2Draw.SetActive(true);
        }
        else if (health <= 0)
        {
            Debug.Log("Player1 Dead");
            Debug.Log("Player2 Wins");
            Time.timeScale = 0f;
            P2Wins.SetActive(true);
        }
        else if (health2 <= 0)
        {
            Debug.Log("Player2 Dead");
            Debug.Log("Player1 Wins");
            Time.timeScale = 0f;
            P1Wins.SetActive(true);
        }
    }

    // This function is called every time another collider overlaps the trigger collider 
    void OnTriggerEnter2D (Collider2D other)
    {
        // Checking if the overlapped collider is an enemy 
        if (other.CompareTag ("P2Attack1") && Player1)
        {
            knockbackDirection = Player1RB2D.transform.position - other.transform.position;
            Player1RB2D.AddForce( knockbackDirection.normalized * 300f);
            health -= 10;
            healthBar.value = health / maxHealth;
        }
        else if (other.CompareTag ("P2Attack2") && Player1)
        { 
            knockbackDirection = Player1RB2D.transform.position - other.transform.position;
            Player1RB2D.AddForce(knockbackDirection.normalized * 300f);
            health -= 12;
            healthBar.value = health / maxHealth;
        }
        
        if (other.CompareTag ("P1Attack1") && !Player1)
        {
            knockbackDirection = Player2RB2D.transform.position - other.transform.position;
            Player2RB2D.AddForce(knockbackDirection.normalized * 500f);
            health2 -= 6;
            healthBar2.value = health2 / maxHealth;

        }
        else if (other.CompareTag("P1Attack2") && !Player1)
        {
            knockbackDirection = Player2RB2D.transform.position - other.transform.position;
            Player2RB2D.AddForce(knockbackDirection.normalized * 800f);
            health2 -= 10;
            healthBar2.value = health2 / maxHealth;
        }
    }
}