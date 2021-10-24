using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    #region Public Variables
    public float attackDistance;
    public float moveSpeed;
    public float timer; //cooldown
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    ///////////////////
    public Transform attackPoint;
    public LayerMask playerLayers;
    public int attackDamage = 10;
    public bool alive;
    /////////////////
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    #endregion

    void Awake(){

        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
        alive = true;
        
    }

    void Update()
    {

        if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyBandit_Attack")){

            SelectTarget();

        }

        if(!attackMode & alive){
             Move();
        }

        if(inRange & alive){
            
            EnemyLogic();
        }
        
    }



    void EnemyLogic(){
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackDistance){
           
            StopAttack();

        }
        else if(attackDistance >= distance && cooling == false){

            Attack();

        }

        if(cooling){
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move(){

        anim.SetBool("canWalk", true);

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyBandit_Attack")){

            Vector2  targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position =  Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


        }
    }

    void Attack(){
        timer = intTimer;
        attackMode = true;
        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
        //////////////////////////////
         //Enemy érzékelése rangen belül
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, playerLayers);

        //Damage 1 dmg = 250 hp
        foreach(Collider2D Player in hitPlayer){
            
            Player.GetComponent<Player>().TakeDamage(attackDamage);
        }

    }
        //////////////

    

    void StopAttack(){
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);

    }

    void Cooldown(){
        timer -= Time.deltaTime;
        if(timer <= 0 && cooling && attackMode){
            cooling = false;
            timer=intTimer;
        }
    }

    public void TriggerCooling(){
        cooling = true;
    }

    private bool InsideofLimits(){

        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget(){
        
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight){
            target = leftLimit;
        }
        else{
            target = rightLimit;
        }
        
        Flip();
        
    }

    public void Flip(){
        if(alive){
        Vector3 rotation = transform.eulerAngles;
        
        if(transform.position.x > target.position.x){
            rotation.y = 180f;
        }
        else{
            rotation.y = 0f;

        }

        transform.eulerAngles = rotation;
        }
    }
    
    }
