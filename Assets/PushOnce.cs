using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PushOnce : NetworkBehaviour {

    public CommonController charactor;
    public float force = 100f;
    
    private float proc = 0.0f;

    public void punch()
    {
        proc = 1.5f;
        gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        if(charactor == null)
        {
            charactor = GetComponentInParent<CommonController>();
        }
    }
	
	// Update is called once per frame
	void Update () {

        proc *= Mathf.Exp(-Time.deltaTime * 5f);
        
        transform.localPosition = new Vector3(0, 0.9f, 0.6f * proc);

        if (proc > 0.1f)
            transform.localScale = new Vector3(proc, proc, proc);
        else
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            gameObject.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            Quaternion direction = charactor.getViewRotation();
            other.GetComponent<Rigidbody>().AddForce(direction * Vector3.forward * force, ForceMode.Impulse);
        }
    }

}
