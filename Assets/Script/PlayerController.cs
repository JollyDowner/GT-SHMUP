using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 20f;
    public float rightBorder;
    public float leftBorder;
    public float upperBorder;
    public float lowerBorder;
    public GameObject bulletPrefab;
    public float bulletSpeed;


    // Use this for initialization
    void Start () {





	}
	
	// Update is called once per frame
	void Update () {
        Move();

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(this.gameObject);
       
    }
}
