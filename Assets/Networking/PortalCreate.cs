using UnityEngine;
using UnityEngine.Networking;

public class PortalCreate : NetworkBehaviour
{
    public GameObject Portal;

    public override void OnStartServer()
    {
        var portal = (GameObject)Instantiate(Portal);
        NetworkServer.Spawn(portal);
    }
}
