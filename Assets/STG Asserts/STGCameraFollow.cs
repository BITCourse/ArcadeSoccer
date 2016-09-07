using UnityEngine;
using System.Collections;

public class STGCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target; // 摄像头移动所跟随的目标

    [SerializeField]
    private Vector3 center = Vector3.zero; // 中心位置，目标在这个位置时摄像头正对目标

    [SerializeField]
    private Vector3 offsetScale = Vector3.zero; // 位移缩放

    [SerializeField]
    private float distance = 10.0f; // 摄像头与位移缩放后目标的距离

    [SerializeField]
    private float height = 50.0f; // 摄像头相对于位移缩放后目标的高度

	// Use this for initialization
	void Start ()
    {
        var trans = new Vector3(0, -height, distance);
        transform.localRotation.SetLookRotation(trans);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target != null)
        {
            var offset = target.transform.localPosition - center;
            offset.Scale(Vector3.one - offsetScale);
            var trans = Vector3.back * distance;
            trans.y = height;
            transform.localPosition = target.transform.localPosition + trans;
            transform.LookAt(target);
            transform.localPosition -= offset;
        }
	}
}
