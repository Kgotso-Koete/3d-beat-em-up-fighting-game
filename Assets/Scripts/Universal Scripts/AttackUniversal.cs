using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// https://youtu.be/gcF66q-UPCs?t=14595
public class AttackUniversal : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float radius = 1f;
    public float damage = 2f;
    public bool is_Player, is_Enemy;
    public GameObject hit_FX_Prefab;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        DetectCollision();
    }
    void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius,collisionLayer);
        // if there are collisions
        if(hit.Length > 0)
        {
            // print("We Hit The " + hit[0].gameObject.name);
            if(is_Player)
            {
                Vector3 hitFX_Pos = hit[0].transform.position;
                hitFX_Pos.y += 1.3f;
                // if enemy is facing right side
                if(hit[0].transform.forward.x > 0)
                {
                    hitFX_Pos.x += 0.3f;
                }
                // if enemy is facing left side
                else if(hit[0].transform.forward.x < 0)
                {
                    hitFX_Pos.x -= 0.3f;
                }
                // show effect
                Instantiate(hit_FX_Prefab, hitFX_Pos, Quaternion.identity);
                // if left arm or left leg heavy attack then knock down and damage, or else just damage
                if(gameObject.CompareTag(Tags.LEFT_ARM_TAG) || gameObject.CompareTag(Tags.LEFT_LEG_TAG))
                {
                   hit[0].GetComponent<HealthScript>().ApplyDamage(damage,true);
                }
                else
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage,false);
                }
            }
            if(is_Enemy)
            {
                hit[0].GetComponent<HealthScript>().ApplyDamage(damage,false);
            }
            // set game object as inactive to avoid multiple/concurrent collisions
            gameObject.SetActive(false);
        }
    }
}
