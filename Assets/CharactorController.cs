using UnityEngine;
using System;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CharactorController : MonoBehaviour {

    public Transform mainCamera;

    public float cameraDistance = 1f;
    public float cameraHeight = 1f;

    public float mouseXSensitivity = 0.02f;
    public float mouseYSensitivity = 0.02f;

    public float moveSpeed = 0.1f;

    public float jumpSpeed = 5f;

    private float charRot = 0;
    private float bodyTurn = 0;
    private Vector2 targetViewRotation = new Vector2(0, 0);
    private Rigidbody rigid = null;

    private Collider col = null;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        col = GetComponents<Collider>()[0];
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }
    }
	
	// Update is called once per frame
	void Update () {

        // 处理控制器
        float deltaX = CrossPlatformInputManager.GetAxis("Horizontal");
        float deltaZ = CrossPlatformInputManager.GetAxis("Vertical");
        bool jump = CrossPlatformInputManager.GetButtonDown("Jump");
        bool fire1 = CrossPlatformInputManager.GetButtonDown("Fire1");
        float xRot = CrossPlatformInputManager.GetAxis("Mouse X") * mouseXSensitivity;
        float yRot = CrossPlatformInputManager.GetAxis("Mouse Y") * mouseYSensitivity;

        // 左右横移转身
        float targetTurn = 0;
        if (deltaX < 0) targetTurn = -30.0f;
        else if (deltaX > 0) targetTurn = 30.0f;

        bodyTurn += (targetTurn - bodyTurn) * (1 - Mathf.Exp(-Time.deltaTime * 10f));
        
        // 转动人物
        charRot = targetViewRotation.x + bodyTurn;
        transform.localRotation = Quaternion.Euler(0, charRot, 0);

        // 移动人物
        /*transform.localPosition += Quaternion.Euler(0, targetViewRotation.x, 0) 
            * new Vector3(deltaX * moveSpeed, 0, deltaZ * moveSpeed);*/
        float velY = rigid.velocity.y;
        if (jump && transform.position.y < 0.1)
        {
            velY = jumpSpeed;
        }
        rigid.velocity = Quaternion.Euler(0, targetViewRotation.x, 0)
            * new Vector3(deltaX * moveSpeed, velY, deltaZ * moveSpeed);

        // 视角转动
        targetViewRotation.x = targetViewRotation.x + xRot;
        targetViewRotation.y = targetViewRotation.y - yRot;
        if (targetViewRotation.y > 90.0f) targetViewRotation.y = 90.0f;
        if (targetViewRotation.y < -90.0f) targetViewRotation.y = -90.0f;

        // 移动相机
        mainCamera.localRotation = Quaternion.Euler(targetViewRotation.y, targetViewRotation.x, 0);
        mainCamera.localPosition = transform.localPosition + new Vector3(0, cameraHeight, 0)
            + mainCamera.localRotation * new Vector3(0, 0, -cameraDistance);

    }
}
