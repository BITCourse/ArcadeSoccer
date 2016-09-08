using UnityEngine;
using UnityEngine.Networking;

public class BallAutoCreate : NetworkBehaviour
{

    public GameObject RollerBall;

    public override void OnStartServer()
    {
        var pos = new Vector3(0, 0.2f, 0);

        var rotation = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));

        var bal = (GameObject)Instantiate(RollerBall, pos, rotation);
        NetworkServer.Spawn(bal);
    }
}