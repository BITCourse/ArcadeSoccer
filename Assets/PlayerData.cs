using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour
{

    static int pos = 0;

    [Command]
    void CmdCreatePlayer()
    {

    }

	// Use this for initialization
	void Start () {
        CmdCreatePlayer();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

}
