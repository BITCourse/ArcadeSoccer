using UnityEngine;
using System.Collections;

public class STGSceneTestMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private float circleRadius = 100.0f;

    private Vector3 center;
    private Vector3 begin;

    private float timePassed = 0.0f;

	// Use this for initialization
	void Start ()
    {
        center = transform.localPosition + transform.localRotation * Vector3.right * circleRadius;
        begin = transform.localPosition - center;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timePassed += Time.deltaTime;
        transform.localPosition = center + Quaternion.AngleAxis(Mathf.Rad2Deg * (timePassed * moveSpeed / circleRadius), new Vector3(0, 1, 0)) * begin;
        transform.Rotate(new Vector3(0, 1, 0), Mathf.Rad2Deg * (Time.deltaTime * moveSpeed / circleRadius));
	}

}
