using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // This is very important if we want to restart the level 
using UnityEngine.UI;

public class EnemyReaction : MonoBehaviour {

    public GameObject PauseMenu;

    static float maxHealth = 100;
    public Slider healthBar, healthBar2;
    float health = maxHealth;
    float health2 = maxHealth;
    public bool Player1;
    private bool Player1Dead = false;
    private bool Player2Dead = false;

    // This function is called every time another collider overlaps the trigger collider 
    void OnTriggerEnter2D (Collider2D other)
    {
        // Checking if the overlapped collider is an enemy 
        if (other.CompareTag ("Enemy") && Player1)
        {
            health -= 25;
            healthBar.value = health / maxHealth;
            if (health == 0)
            {
                Debug.Log("Player1Dead");
                Player1Dead = true;
                Debug.Log("Player2 Wins");
                Time.timeScale = 0f;
                PauseMenu.SetActive(true);
                if (Player2Dead)
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }
        
        if (other.CompareTag ("Enemy") && !Player1)
        {
            health2 -= 25;
            healthBar2.value = health2 / maxHealth;
            if (health2 == 0)
            {
                Debug.Log("Player2 Dead");
                Player2Dead = true;
                Debug.Log("Player1 Wins");
                Time.timeScale = 0f;
                PauseMenu.SetActive(true);
                if (Player1Dead)
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }
    }
}