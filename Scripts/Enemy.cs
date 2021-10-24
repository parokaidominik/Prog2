using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public int maxHealth = 100;
    public int currentHealth;

    private Enemy_behaviour enemyParent;
    private void Awake(){

        enemyParent = GetComponentInParent<Enemy_behaviour>();
    }

    void Start()
    {   
        currentHealth = maxHealth;
        
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        // Sérülés animáció
        anim.SetTrigger("Hurt");
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Debug.Log("Meghalt az ellenfél.");
        // Halál animáció
        anim.SetBool("IsDead", true);
        enemyParent.alive = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        //Destroy (gameObject);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }


}
