using UnityEngine;
using System.Collections;

public class BorderDisabler : MonoBehaviour {

    public GameObject border;

    private Collider coll = null;

	// Use this for initialization
	void Start () {
        coll = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball" && border != null)
        {
            border.GetComponent<Collider>().enabled = false;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball" && border != null)
        {
            border.GetComponent<Collider>().enabled = true;
        }
    }

}
