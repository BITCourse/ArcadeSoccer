using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

[RequireComponent(typeof(CommonController))]
public class FastController : NetworkBehaviour
{
    public GameObject effectObject;
    public GameObject cdObject;

    CommonController con;

    bool accelerated = false;
    float lastTime = -20f;

    bool accelerateReady = true;

    [Command]
    void CmdAccelerate()
    {
        doAccelerate();
    }

    [ClientRpc]
    void RpcAccelerate()
    {
        if (!isLocalPlayer)
            accelerated = true;
    }

    void doAccelerate()
    {
        if (!isServer)
        {
            if (isLocalPlayer)
                CmdAccelerate();
        }
        else
        {
            RpcAccelerate();
        }
        accelerated = true;
    }


    // Use this for initialization
    void Start ()
    {
        con = GetComponent<CommonController>();
    }
	
	// Update is called once per frame
	void Update () {

        bool ctrl = false;
        bool fire2 = false;

        if (isLocalPlayer)
        {
            // 处理控制器
            ctrl = CrossPlatformInputManager.GetButton("Control");
            if (!ctrl)
            {
                fire2 = CrossPlatformInputManager.GetButtonDown("Fire2");
            }

            // 动作状态，同时向服务器发射指令
            if (fire2 && con.specialReady && accelerateReady)
                doAccelerate();

        }
        
        if (accelerated)
        {
            lastTime = Time.time;
            accelerateReady = false;
            cdObject.SetActive(false);
            effectObject.SetActive(true);
            Invoke("disableEffect", 5.0f);
            con.setSpecialAnim(0.5f);
            accelerated = false;
        }

        if (Time.time - lastTime < 3.0f)
        {
            con.moveSpeed = 40f;
            con.cameraDistance = Mathf.Lerp(0.7f, con.cameraDistance, Mathf.Exp(-Time.deltaTime * 5f));
            Camera.main.fieldOfView = Mathf.Lerp(90f, Camera.main.fieldOfView, Mathf.Exp(-Time.deltaTime * 5f));
        }
        else
        {
            con.moveSpeed = Mathf.Lerp(con.moveSpeed, 16f, Mathf.Exp(Time.deltaTime));
            con.cameraDistance = Mathf.Lerp(1.2f, con.cameraDistance, Mathf.Exp(-Time.deltaTime));
            Camera.main.fieldOfView = Mathf.Lerp(60f, Camera.main.fieldOfView, Mathf.Exp(-Time.deltaTime));
        }

        if (Time.time - lastTime > 10.0f && !accelerateReady)
        {
            accelerateReady = true;
            cdObject.SetActive(true);
        }

    }

    void disableEffect()
    {
        effectObject.SetActive(false);
    }

}
