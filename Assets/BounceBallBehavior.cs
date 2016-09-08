using UnityEngine;
using System.Collections;

public class BounceBallBehavior : MonoBehaviour {

    Rigidbody rigid = null;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            Vector3 dir = transform.position - collision.gameObject.transform.position;
            dir.Normalize();
            dir *= collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 0.5f;
            dir.y *= 0.5f;
            rigid.velocity += dir;
        }
    }


}
