using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour {

    public Vector3 rotation;

    private Quaternion quat;

    // Use this for initialization
    void Start () {
        quat = Quaternion.Euler(rotation);
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = quat;
    }

}
