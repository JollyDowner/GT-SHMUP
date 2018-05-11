using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour { 

    public static UiManager instance ;
    private GameManager gameMan;

    public Text scoreText;
    public Text prePlayText;
    public Text GameOverText;
    private int score;

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
    void Start () {
        score = 0;
        gameMan = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
        showText();
	}

    public void updateScore(int addedScore)
    {
        this.score += addedScore;
    }


    private void showText()
    {
        switch (gameMan.currentGameState)
        {
            case GameManager.gameState.preplay:
                scoreText.text = "";
                GameOverText.text = "";
                prePlayText.text = "Press Space to start.";

                break;

            case GameManager.gameState.inPlay:
                GameOverText.text = "";
                prePlayText.text = "";
                scoreText.text = "Score: " + score;
                break;

            case GameManager.gameState.gameOver:
                prePlayText.text = "";
                scoreText.text = "";
                score = 0;
                GameOverText.text = "u ded. Press Space to play again.";
                break;

        }
    }

}
