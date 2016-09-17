using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

[RequireComponent (typeof(CommonController))]
public class DJController : NetworkBehaviour
{
    public GameObject pushObject;

    CommonController con;
    
    bool pushed = false;

    float lastPush = -20;

    [Command]
    void CmdDJPush()
    {
        doDJPush();
    }

    [ClientRpc]
    void RpcDJPush()
    {
        if(!isLocalPlayer)
            pushed = true;
    }

    void doDJPush()
    {
        if (!isServer)
        {
            if (isLocalPlayer)
                CmdDJPush();
        }
        else
        {
            RpcDJPush();
        }
        pushed = true;
    }

    // Use this for initialization
    void Start () {
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
            if (fire2 && con.specialReady && Time.time - lastPush > 0.5f)
                doDJPush();

        }

        // 拳击
        if (pushed)
        {
            lastPush = Time.time;
            pushObject.GetComponent<DJPush>().push();
            con.setSpecialAnim(0.7f);
            pushed = false;
        }

    }
}
