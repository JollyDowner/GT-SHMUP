using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDissvoleBehaviour : MonoBehaviour {

    Renderer thisRend;
    Material thisMat;

    public float dissolveSpeed;
    public float burnGrwothSpeed;

	// Use this for initialization
	void Start () {
        thisRend = GetComponent<Renderer>();
        thisMat = thisRend.material;


	}
	
	// Update is called once per frame
	void Update () {
        thisMat.SetFloat("_DissolveFactor",thisMat.GetFloat("_DissolveFactor")+dissolveSpeed);

        if (thisMat.GetFloat("_DissolveFactor") >= 1.0)
        {
            Destroy(this.gameObject);
        }
    }


}
