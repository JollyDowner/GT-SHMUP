using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour { 

    public static UiManager instance ;

    public Text scoreText;
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
        scoreText.text = "Score: ";
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text ="Score: "+score;
	}

    public void updateScore(int addedScore)
    {
        this.score += addedScore;
    }
}
