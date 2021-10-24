using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float AttackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;


    void Update()
    {
        if(Time.time >= nextAttackTime){
            if(Input.GetKeyDown(KeyCode.Space)){
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;

        }
        }
        
        
    }

    void Attack(){

        //Attack animáció
        anim.SetTrigger("Attack");

        //Enemy érzékelése rangen belül
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, enemyLayers);

        //Damage
        foreach(Collider2D Enemy in hitEnemies){
            //enemy.GameObject.FindGameObjectsWithTag("Enemy").TakeDamage(attackDamage);
            Enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    void OnDrawGizmosSelected(){

        if (attackPoint == null)
        return;
        Gizmos.DrawWireSphere(attackPoint.position, AttackRange);
    }
}

