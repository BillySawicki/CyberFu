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
        
    }

    void FixedUpdate(){
        FollowTarget();
    }

    void FollowTarget(){
        //If the enemy is not following the target...
        if(!isFollowingTarget){
            //"isFollowingTarget" is true

            return;
        }

        //If the enemy is too far away to attack the target...

        if(Vector3.Distance(transform.position, target.position) >= attackingDistance){
            //direction equals to target's position minus enemy's position
            direction = target.position - transform.position;
            //y aixs value equals to 0
            direction.y = 0;
            //enemy rotation to focus on the target
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 20);
            // If enemy's distance is close to attack the player
            else if(Vector3.Distance(transfrom.position, target.position) <= attackingDistance){
                
            }
        }


        
        //If the enmy's physics equals to 0
        if(rigidbodyEnemy.velocity.sqrMagnitude != 0){
            //move the enemy by the velocity of speed 
            rigidbodyEnemy.velocity = transform.forward * speed;
            animatorEnemy.SetBool("Walk", true);
        }
    }
}
