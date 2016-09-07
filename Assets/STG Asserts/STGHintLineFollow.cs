using UnityEngine;
using System.Collections;

public class STGHintLineFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 followMultiplier;

    private Vector3 centerPos;
    private Vector3 targetCenterPos;

	// Use this for initialization
	void Awake ()
    {
        centerPos = transform.localPosition;
        targetCenterPos = target.localPosition;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        var offset = target.localPosition - targetCenterPos;
        offset.x *= followMultiplier.x;
        offset.y *= followMultiplier.y;
        offset.z *= followMultiplier.z;
        transform.localPosition = centerPos + offset;
	}
}
