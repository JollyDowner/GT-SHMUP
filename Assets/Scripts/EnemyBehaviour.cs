using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyBehaviour : MonoBehaviour {
    public float turnVelocity;
    public float enterScreenVelocity;
    public float chargeVelocity;
    public int scoreValue;


    public UiManager uiMan;
    public GameManager gameMan;
    public GameObject player;
    public Collider enemyCollider;

    bool isHit= false;
    // Use this for initialization
    void Start () {

        gameMan = GameManager.instance;
        uiMan = UiManager.instance;
 

        enemyCollider = GetComponent<Collider>();
        enemyCollider.enabled =  !enemyCollider.enabled;
        player =gameMan.player;



        turnVelocity = 5f;
        chargeVelocity = 40f;
        scoreValue = 25;


        StartCoroutine("GenericMovement");
        StartCoroutine("Rotation");
    }
	
	// Update is called once per frame
	void Update () {


    }


    private void OnCollisionEnter(Collision collision)
    {
        isHit = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.AddComponent<BurnDissvoleBehaviour>();
        GetComponent<BurnDissvoleBehaviour>().dissolveSpeed = 0.1f;


        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            uiMan.updateScore(scoreValue);
            Destroy(collision.gameObject);

        }
 
        
    }


    private void OnBecameVisible()
    {
        enemyCollider.enabled = !enemyCollider.enabled;
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);

    }


    IEnumerator GenericMovement()
    {
        player = gameMan.player;
        while (transform.position.z >= -2)
        {

            transform.position += new Vector3(0,0,-10+enterScreenVelocity)*Time.deltaTime;
            yield return 0;
        }

        yield return new WaitForSeconds(0.25f);

        Vector3 direction =Vector3.back.normalized;

        while (!isHit)
        {
   
            transform.position += direction*chargeVelocity*Time.deltaTime;

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

