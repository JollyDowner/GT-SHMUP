using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed;

    public float rightBorder;
    public float leftBorder;
    public float upperBorder;
    public float lowerBorder;

    public GameObject bulletPrefab;
    private GameManager gameMan;

    public float bulletSpeed;

    private bool isHit;

    public 


    // Use this for initialization
    void Start () {


        gameMan = GameManager.instance;

        rightBorder = 13.04f;
        leftBorder= -21.84f;

        upperBorder = -11.58f;
        lowerBorder = -18.42f;

        movementSpeed = 20f;
        isHit = false;

}
	
	// Update is called once per frame
	void Update () {


        if (!isHit) { Move(); }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

    }



    void Move()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * Time.deltaTime;


        Vector3 pos = transform.position;
        Vector3 newPos = pos + new Vector3(x * movementSpeed, 0, z * movementSpeed);

        if (newPos.x >= leftBorder && newPos.x <= rightBorder)
        {
            transform.Translate(x * movementSpeed, 0, 0);
        }

        if (newPos.z <= upperBorder && newPos.z >= lowerBorder)
        {
            transform.Translate(0, 0, z * movementSpeed);
        }

    }

    void Fire()
    {

        GameObject bullet = Instantiate(bulletPrefab,this.transform.position,this.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        Destroy(bullet, 0.75f);
    }

    private void OnCollisionEnter(Collision collision)
    {

        isHit = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.AddComponent<BurnDissvoleBehaviour>();
        GetComponent<BurnDissvoleBehaviour>().dissolveSpeed = 0.07f;

        gameMan.gameOver();

       
    }
}
