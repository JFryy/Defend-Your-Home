using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int spawnChancePerTickBase = 37;

    public GameObject mushroomEnemyPrefab;
    public GameObject basicEnemyPrefab;
    public GameObject goblinArcherPrefab;
    public GameObject upgradeScript;
    public float secondsBetweenSpawn = 1;
    public float elapsedTime = 0.0f;
    public float witchWeightingBase = 0;

    // Update spawner to spawn on three rows randomly + update spawner to use RNG each second for spawn chance.
    void FixedUpdate()
    {
        int level = upgradeScript.GetComponent<LevelEnd>().levelNumber;
        var spawnOffset = new Dictionary<int, float>(){
            {1, -3},
            {2, -4},
            {3, -6}
        };
        int spawnChance = Random.Range(1, 1001);
        int witchChance = Random.Range(1, 1001);
        int mushroomChance = Random.Range(1, 1001);
    
        // range has upper bound exclusive and lower inclusive (it's annoying).
        int spawnPoint = Random.Range(1, 4);
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn)
        {
            elapsedTime = 0;
            
            if (spawnChance > 750 - ((level-1)*30))
            {
                if (mushroomChance < 0+((level)*20))
                {
                    GameObject basicEnemy = Instantiate(mushroomEnemyPrefab, new Vector3(transform.position.x, spawnOffset[spawnPoint], transform.position.z), Quaternion.identity);
                }
                else if (witchChance < 100+((level)*35))
                {
                    GameObject goblinArcher = Instantiate(goblinArcherPrefab, new Vector3(transform.position.x, spawnOffset[spawnPoint], transform.position.z), Quaternion.identity);
                }
                else 
                {
                    GameObject basicEnemy = Instantiate(basicEnemyPrefab, new Vector3(transform.position.x, spawnOffset[spawnPoint], transform.position.z), Quaternion.identity);
                    basicEnemy.transform.localScale = new Vector3(5,5,1);
                }        
            }
        }
    }
}


