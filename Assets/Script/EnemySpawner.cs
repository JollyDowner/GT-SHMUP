using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {


    public float[] spawnPointX;

    public int leftSpawnBorder;
    public int rightSpawnBorder;
    public float upperSpawnBorder;
    public float lowerSpawnBorder;

    public float zLock;
    public float yLock;


    public float spawnTime;
    private int intensity;

    public GameObject enemy;

    private GameManager gameMan;
    private static EnemySpawner instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        zLock = 6f;
        yLock = 1.88f;

        rightSpawnBorder = 12;
        leftSpawnBorder = -21;

        intensity = 3;
        spawnTime = 1.25f;
   
        gameMan = GameManager.instance;
        enemy = gameMan.enemy;


        StartCoroutine("SpawnEnemies");

    }
	
	// Update is called once per frame
	void Update () {
       
	}


 IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            int spawnNumber = intensity + 1;
            spawnPointX = new float[spawnNumber];
            float randomPoint = Random.Range(leftSpawnBorder, rightSpawnBorder);
            float step = 2.2f;

            if (randomPoint > -4.5f) { step = step * -1; }



            for(int i = 0; i < spawnNumber; i++)
            {
                spawnPointX[i] = randomPoint + step* i;
            }


            for (int i =0; i <spawnPointX.Length; i++)
            {

                Instantiate(enemy, new Vector3(spawnPointX[i], yLock, zLock), new Quaternion(0, 0, 0, 0));

            }       

            yield return new WaitForSeconds(spawnTime);
        }
            
    }
}
