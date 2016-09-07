using UnityEngine;
using System.Collections;

public class STGBorder : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distance = 20.0f;
    [SerializeField]
    private float maxOpacity = 0.3f;

    private Quaternion facing;
    private Renderer rend;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(target != null)
        {
            Vector3 offset = target.position - transform.position;
            float dis = Vector3.Dot(offset, transform.rotation * Vector3.up);
            rend.material.color = new Color(1.0f, 1.0f, 1.0f, dis > distance ? 0.0f : maxOpacity * (1 - dis / distance));
        }
	}
}
