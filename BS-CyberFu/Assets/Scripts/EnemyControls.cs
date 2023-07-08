using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    // the set speed of the enemy
    public float speed = 5;
    //the set attacking distance for the enemy
    public float attackingDistance = 1;
    //axis points (x,y,z) for the direction for the enemy
    public Vector3 direction;

    // the animator component controls the animations for the enemy
    private Animator animatorEnemy;
    //the rigidbody component controls the physics for the enmeny
    private Rigidbody rigidbodyEnemy;
    // this variable will help the enemy target any object with the variable
    private Transform target;

    //If the enemy is following the target or not
    public bool isFollowingTarget;
    //If the enemy is attacking the target or not
    public bool isAttackingTarget;
    // Start is called before the first frame update

    //set value for the amount of time to chase to the player
    private float chasingPlayer = 0.01f;
    //current value of what the current time is for attacking 
    public float currentAttackingTime;
    //the set value of the maximum time of attacking 
    public float maxAttackingTime = 2f;

    void Start()
    {
        //at the start of the game, the enemy will follow the target
        isFollowingTarget = true;
        //the enemy's current attacking time equals the maximum attacking time
        currentAttackingTime = maxAttackingTime;

        //getting the component for the animator
        animatorEnemy = GetComponent<Animator>();
        //getting the component for the rigidbody
        rigidbodyEnemy = GetComponent<Rigidbody>();
        //set the target for the enemy to follow and attack to the player by tag
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void FixedUpdate(){
        FollowTarget();
    }

    void FollowTarget(){
        //If the enemy is not following the target...
        if(!isFollowingTarget){
            //"isFollowingTarget" is true
            rigidbodyEnemy.isKinematic = true;
            return;
        }

        //If the enemy is too far away to attack the target...

        if(Vector3.Distance(transform.position, target.position) >= attackingDistance){
            rigidbodyEnemy.isKinematic = false;
            //direction equals to target's position minus enemy's position
            direction = target.position - transform.position;
            //y aixs value equals to 0
            direction.y = 0;
            //enemy rotation to focus on the target
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 100);
        }
        // If enemy's distance is close to attack the player
        else if(Vector3.Distance(transform.position, target.position) <= attackingDistance){
            rigidbodyEnemy.isKinematic = false;
            //Keeps the enemy from moving within its place
            rigidbodyEnemy.velocity = Vector3.zero;
            //animation "Walk" is set to false
            animatorEnemy.SetBool("Walk", false);
            isFollowingTarget = false;
            isAttackingTarget = true;
            }
    


        
        //If the enmy's physics equals to 0
        if(rigidbodyEnemy.velocity.sqrMagnitude != 0){
            //move the enemy by the velocity of speed 
            rigidbodyEnemy.velocity = transform.forward * speed;
            animatorEnemy.SetBool("Walk", true);
        }
    }
    
    void Attack(){
        if (!isAttackingTarget){
            return;
        }
        currentAttackingTime += Time.deltaTime;
        if(currentAttackingTime > maxAttackingTime){
            currentAttackingTime = 0f;
            EnemyAttack(Random.Range(1,7));
        }

        if(Vector3.Distance(transform.position, target.position) > attackingDistance + chasingPlayer){
            isAttackingTarget = false;
            isFollowingTarget = true;
        }
    }
    public void EnemyAttack(int attack){
        if(attack == 1){
            animatorEnemy.SetTrigger("Attack1");
        }

        if(attack ==2){
            animatorEnemy.SetTrigger("Attack");
        }

        if(attack == 3 ){
            animatorEnemy.SetTrigger("Attack3");
        }

        if(attack == 4){
            animatorEnemy.SetTrigger("Attack4");
        }

        if(attack == 5){
            animatorEnemy.SetTrigger("Attack5");
        }

        if(attack == 6){
            animatorEnemy.SetTrigger("Attack6");
        }
    }
}
