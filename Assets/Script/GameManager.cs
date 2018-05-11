using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public static UiManager uiMan;

    public GameObject player;
    public GameObject enemy;
    public GameObject spawner;

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

	}
	
	// Update is called once per frame
	void Update () {


        if(currentGameState == gameState.preplay && Input.GetKeyDown(KeyCode.Space))
        {
            currentGameState = gameState.inPlay;
            createEnemySpawner();
            Debug.Log(currentGameState);
        }

        if (player == null)
        {
            currentGameState = gameState.gameOver;
            Debug.Log(currentGameState);

            Destroy(spawner);

            GameObject[] enemiesLEft = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemiesLEft) { Destroy(enemy); }
        }



        if(currentGameState ==gameState.gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            currentGameState = gameState.inPlay;
        }




    }

    void createEnemySpawner()
    {
        spawner = new GameObject();
        spawner.AddComponent<EnemySpawner>();
    }



}
