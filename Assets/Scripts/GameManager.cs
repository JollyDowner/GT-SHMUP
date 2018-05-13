using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public static UiManager uiMan;

    public GameObject player;
    public GameObject enemy;
    private GameObject spawner;

    private Vector3 playerSpawnPosition = new Vector3(-3.95f, 1.88f, -17.08f);


    public enum gameState{


        preplay,
        inPlay,
        gameOver

        }

    public gameState currentGameState;



    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

    }

    // Use this for initialization
    void Start () {

        currentGameState = gameState.preplay;
        Instantiate(player, playerSpawnPosition, new Quaternion(0, 0, 0, 0));

    }

	// Update is called once per frame
	void Update () {

        checkForInput();

    }



    private void checkForInput()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentGameState)
            {
                case gameState.preplay:
                    currentGameState = gameState.inPlay;
                    createEnemySpawner();
                    break;

                case gameState.gameOver:
                    currentGameState = gameState.inPlay;
                    Instantiate(player, playerSpawnPosition, new Quaternion(0, 0, 0, 0));
                    createEnemySpawner();
                    break;
            }
        }

    }

    public void gameOver()
    {
        currentGameState = gameState.gameOver;
        Destroy(spawner);

        GameObject[] enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemiesLeft) { Destroy(enemy); }
    }


    void createEnemySpawner()
    {
        spawner = new GameObject("EnemySpawner");
        spawner.AddComponent<EnemySpawner>();
    }
}
