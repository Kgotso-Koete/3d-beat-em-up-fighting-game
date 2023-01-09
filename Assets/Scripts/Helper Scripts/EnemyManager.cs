using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField]
    private GameObject enemyPrefab;
    void Awake()
    {
       if(instance == null)
       {
            instance = this;
       }
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
