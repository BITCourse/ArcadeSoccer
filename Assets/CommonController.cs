using UnityEngine;
using System;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class CommonController : NetworkBehaviour {

    public Transform mainCamera;

    public GameObject ball;
    public GameObject push;

    public string idleAnimation = "idle";
    public string moveAnimation = "run";
    public string jumpAnimation = "hit";
    public string pushAnimation = "punch";

    public float cameraDistance = 1f;
    public float cameraHeight = 1f;

    public float mouseXSensitivity = 0.02f;
    public float mouseYSensitivity = 0.02f;

    public float moveSpeed = 0.1f;

    public float jumpSpeed = 5f;

    [SyncVar]
    private float charRot = 0;
    [SyncVar]
    private float bodyTurn = 0;

    [SyncVar]
    private bool jumped = false;
    [SyncVar]
    private bool moving = false;
    [SyncVar]
    private bool punched = false;

    private float lastPunch = -10f;

    private Vector2 viewRot = Vector2.zero;

    private Rigidbody rigid = null;
    private Collider col = null;
    private Animation anim = null;


    public Quaternion getViewRotation()
    {
        return Quaternion.Euler(viewRot.y, viewRot.x, 0);
    }

    [Command]
    void CmdMove(float deltaX, float deltaZ, Vector2 view)
    {
        doMove(deltaX, deltaZ, view);
        //Debug.Log("CmdMove " + isClient.ToString());
    }

    void doMove(float deltaX, float deltaZ, Vector2 view)
    {
        if (!isServer)
        {
            //Debug.Log("doMove " + isClient.ToString());
            CmdMove(deltaX, deltaZ, view);
        }

        moving = (deltaX != 0 || deltaZ != 0);

        // 左右横移转身
        float targetTurn = moving ? Mathf.Atan2(deltaX, deltaZ) / Mathf.PI * 180f : 0;
        targetTurn -= Mathf.Round((targetTurn - bodyTurn) / 360) * 360;

        bodyTurn += (targetTurn - bodyTurn) * (1 - Mathf.Exp(-Time.deltaTime * 10f));

        // 转动人物
        charRot = view.x + bodyTurn;

        // 设置镜头方向
        if(isServer && !isLocalPlayer)
            viewRot = view;

    }

    [Command]
    void CmdJump()
    {
        doJump();
    }

    void doJump()
    {
        if (!isServer)
            CmdJump();
        if (transform.position.y < 0.1f)
            jumped = true;
    }

    [Command]
    void CmdPunch()
    {
        doPunch();
    }

    void doPunch()
    {
        if (!isServer)
            CmdPunch();
        punched = true;
    }

    // Use this for initialization
    void Start () {
		rigid = GetComponent<Rigidbody> ();
        col = GetComponents<Collider>()[0];
        anim = GetComponent<Animation>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        bool ctrl = false;
        float deltaX = 0, deltaZ = 0;
        bool jump = false, fire1 = false;
        float xRot = 0, yRot = 0;

        if (isLocalPlayer)
        {
            // 处理控制器
            ctrl = CrossPlatformInputManager.GetButton("Control");

            Cursor.visible = ctrl;
            Cursor.lockState = ctrl ? CursorLockMode.None : CursorLockMode.Locked;

            if (!ctrl)
            {
                deltaX = CrossPlatformInputManager.GetAxis("Horizontal");
                deltaZ = CrossPlatformInputManager.GetAxis("Vertical");
                jump = CrossPlatformInputManager.GetButtonDown("Jump");
                fire1 = CrossPlatformInputManager.GetButtonDown("Fire1");
                xRot = CrossPlatformInputManager.GetAxis("Mouse X") * mouseXSensitivity;
                yRot = CrossPlatformInputManager.GetAxis("Mouse Y") * mouseYSensitivity;
            }
            
            // 视角转动
            viewRot.x = viewRot.x + xRot;
            viewRot.y = viewRot.y - yRot;
            if (viewRot.y > 90.0f) viewRot.y = 90.0f;
            if (viewRot.y < -90.0f) viewRot.y = -90.0f;

            mainCamera.localRotation = Quaternion.Euler(viewRot.y, viewRot.x, 0);
            mainCamera.localPosition = transform.localPosition + new Vector3(0, cameraHeight, 0)
                + mainCamera.localRotation * new Vector3(0, 0, -cameraDistance);

            // 动作状态，同时向服务器发射指令
            if (fire1)
                doPunch();

            if (jump)
                doJump();

            doMove(deltaX, deltaZ, viewRot);
            
        }

        // 人物动画
        if (punched || Time.time - lastPunch < 0.5f)
        {
            anim.CrossFade(pushAnimation);
        }
        else if (jumped || transform.position.y > 0.1f)
        {
            anim.CrossFade(jumpAnimation, 0.1f);
        }
        else if (moving)
        {
            anim.CrossFade(moveAnimation, 0.5f);
        }
        else
        {
            anim.CrossFade(idleAnimation, 0.3f);
        }

        // 人物旋转
        transform.localRotation = Quaternion.Euler(0, charRot, 0);

        // 拳击
        if (punched)
        {
            lastPunch = Time.time;
            push.GetComponent<PushOnce>().punch();
            punched = false;
        }

        // 移动人物
        /*transform.localPosition += Quaternion.Euler(0, viewRot.x, 0) 
            * new Vector3(deltaX * moveSpeed, 0, deltaZ * moveSpeed);*/
        float velY = rigid.velocity.y;
        if (jumped)
        {
            velY = jumpSpeed;
            jumped = false;
        }
        rigid.velocity = Quaternion.Euler(0, viewRot.x, 0) * new Vector3(deltaX * moveSpeed, velY, deltaZ * moveSpeed);


    }
}
