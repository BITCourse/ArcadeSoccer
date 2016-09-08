using UnityEngine;
using UnityEngine.Networking;

public class NetworkCreate : NetworkBehaviour
{
    public GameObject prefab;

    public Vector3 position = Vector3.zero;
    public Vector3 rotation = Vector3.zero;

    public override void OnStartServer()
    {
        var obj = (GameObject)Instantiate(prefab, position, Quaternion.Euler(rotation));
        NetworkServer.Spawn(obj);
    }

}
