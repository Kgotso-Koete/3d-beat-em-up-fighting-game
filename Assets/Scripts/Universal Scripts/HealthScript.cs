using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthScript : MonoBehaviour
{
    public float health = 100f;
    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;
    private bool characterDied;
    public bool is_Player;
    private HealthUI health_UI;
    void Awake()
    {
        animationScript = GetComponentInChildren<CharacterAnimation>();
        if(is_Player)
        {
            health_UI = GetComponent<HealthUI>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void ApplyDamage(float damage, bool knockDown)
    {
        if(characterDied)
        {
            return;
        }
        health -= damage;
        //display health UI
        if(is_Player)
        {
            health_UI.DisplayHealth(health);
        }
        // check if character died
        if(health <= 0f)
        {
            animationScript.Death();
            characterDied = true;
            // if is player then deactivate enemy script
            if(is_Player)
            {
                GameObject.FindWithTag(Tags.ENEMY_TAG).GetComponent<EnemyMovement>().enabled = false;
            }
            return;
        }
        if(!is_Player)
        {
            if(knockDown)
            {
                // 50/50 chance of knock down
                if(Random.Range(0,2) > 0)
                {
                    animationScript.KnockDown();
                }
            }
            else
            {
                // 33% chance of hit
                if(Random.Range(0,3) > 1)
                {
                    animationScript.Hit();
                }
            }
        }
    }
}
