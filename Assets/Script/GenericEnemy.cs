using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GenericEnemy : MonoBehaviour {
    public float turnVelocity;
    public float moveIntoScreenVelocity;
    public float seekVelocity;
    public int scoreValue;


    public UiManager uiMan;
    public GameObject player;
    public Collider enemyCollider;

    bool isInvisible = false;
    // Use this for initialization
    void Start () {

        scoreValue = 25;
        uiMan = UiManager.instance;
        turnVelocity = 5;
        StartCoroutine("GenericMovement");
        StartCoroutine("Rotation");
        enemyCollider = GetComponent<Collider>();
        enemyCollider.enabled =  !enemyCollider.enabled;
        player = GameObject.Find("Player");
        seekVelocity = 50f;


    }
	
	// Update is called once per frame
	void Update () {


    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(this.gameObject);

        if (collision.gameObject.CompareTag("PlayerBullet")) { uiMan.updateScore(scoreValue); }
 
        
    }


    private void OnBecameVisible()
    {
        enemyCollider.enabled = !enemyCollider.enabled;
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        isInvisible = true;
    }


    IEnumerator GenericMovement()
    {
 
        while(transform.position.z >= -2)
        {

            transform.position += new Vector3(0,0,-10+moveIntoScreenVelocity)*Time.deltaTime;
            yield return 0;
        }

        yield return new WaitForSeconds(0.25f);

        Vector3 enemyTarget = (player.transform.position - transform.position).normalized;

        while (!isInvisible)
        {
   
            transform.position += enemyTarget*seekVelocity*Time.deltaTime;

            yield return 0;
        }

    }


    IEnumerator Rotation()
    {
        while (true)
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y + turnVelocity, transform.rotation.z);
            yield return 0;
        }
    }

}

