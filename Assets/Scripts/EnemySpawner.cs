using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {




    public int leftSpawnBorder;
    public int rightSpawnBorder;

    public float zLock;
    public float yLock;

    public float middlePoint;
    public float[] spawnPoints;


    public float spawnBreakTime;
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
        middlePoint = -4.5f;

        rightSpawnBorder = 12;
        leftSpawnBorder = -21;

        intensity = 3;
        spawnBreakTime = 1.25f;
   
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
            spawnPoints = new float[spawnNumber];
            float randomPoint = Random.Range(leftSpawnBorder, rightSpawnBorder);
            float step = 2.2f;

            if (randomPoint > middlePoint) { step = step * -1; }



            for(int i = 0; i < spawnNumber; i++)
            {
                spawnPoints[i] = randomPoint + step* i;
            }


            for (int i =0; i <spawnPoints.Length; i++)
            {

                Instantiate(enemy, new Vector3(spawnPoints[i], yLock, zLock), new Quaternion(0, 0, 0, 0));

            }       

            yield return new WaitForSeconds(spawnBreakTime);
        }
            
    }
}
