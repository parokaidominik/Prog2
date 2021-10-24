using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Animator anim;
    public GameOverScreen GameOverScreen;
    public float delayTime = 0.2f;

    public int maxHealth = 100;
    int currentHealth;
   // public HealthbarBehaviour Healthbar;

    void Start()
    {
        currentHealth = maxHealth;
      //  Healthbar.SetHealth(currentHealth, maxHealth);
        
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        // Sérülés animáció
        //anim.SetTrigger("Hurt");
        //Healthbar.SetHealth(currentHealth, maxHealth);
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        
        Debug.Log("Meghaltál.");
        // Halál animáció
        anim.SetBool("isDead", true);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Invoke("DelayedAction", delayTime);

            
    }
    void DelayedAction(){
         SceneManager.LoadScene("Gameover");
    }
 

}
