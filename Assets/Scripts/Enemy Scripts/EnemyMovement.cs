using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// saved place on YouTube = https://youtu.be/gcF66q-UPCs?t=7101
public class EnemyMovement : MonoBehaviour
{
    private CharacterAnimation enemyAnim;
    private Rigidbody myBody;
    public float speed = 5f;
    private Transform playerTarget;
    public float attack_Distance = 1f;
    private float chase_Player_After_Attack = 1f;
    private float current_Attack_Time;
    private float default_Attack_Time = 2f;
    private bool followPlayer, attackPlayer;
    void Awake()
    {
        enemyAnim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        current_Attack_Time = default_Attack_Time;
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    void FixedUpdate()
    {
        FollowTarget();
    }
    void FollowTarget()
    {
        if(!followPlayer)
        {
            return;
        }
        // walk to player if out of attack range from player
        if(Vector3.Distance(transform.position,playerTarget.position) > attack_Distance)
        {
            // look at player
            transform.LookAt(playerTarget);
            // go towards them at designated speed
            myBody.velocity = transform.forward * speed;
            // if the character is not stationary
            if(myBody.velocity.sqrMagnitude != 0)
            {
                enemyAnim.Walk(true);
            }
        }
        else if(Vector3.Distance(transform.position,playerTarget.position) <= attack_Distance)
        {
            // stop any movement
            myBody.velocity = Vector3.zero;
            enemyAnim.Walk(false);
            followPlayer = false;
            attackPlayer = true;
        }
    }
    void Attack()
    {
        if(!attackPlayer)
        {
            return;
        }
        current_Attack_Time += Time.deltaTime;
        // keep attacking while attack time has not run out
        if(current_Attack_Time > default_Attack_Time)
        {
            // attack animation 0, 1, or 2
            enemyAnim.EnemyAttack(Random.Range(0,3));
            current_Attack_Time = 0f;
        }
        if(Vector3.Distance(transform.position,playerTarget.position) > (attack_Distance + chase_Player_After_Attack))
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }
}
