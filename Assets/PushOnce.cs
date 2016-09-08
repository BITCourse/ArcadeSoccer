using UnityEngine;
using System.Collections;

public class PushOnce : MonoBehaviour {

    public GameObject charactor;
    public float force = 100f;
    
    private CommonController con;
    private float proc = 0.0f;

    public void punch()
    {
        proc = 1.5f;
    }

    // Use this for initialization
    void Start () {
        con = charactor.GetComponent<CommonController>();
    }
	
	// Update is called once per frame
	void Update () {

        proc *= Mathf.Exp(-Time.deltaTime * 7f);

        Debug.Log(proc);

        if (proc > 0.1f)
            transform.localScale = new Vector3(proc, proc, proc);
        else
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        transform.localPosition = new Vector3(0, 0.9f, 0.5f * proc);

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            Quaternion direction = con.getViewRotation();
            other.GetComponent<Rigidbody>().AddForce(direction * Vector3.forward * force, ForceMode.Impulse);
        }
    }

}
