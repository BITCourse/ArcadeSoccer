using UnityEngine;
using UnityEngine.Networking;

public class BallAutoCreate : NetworkBehaviour
{
    public GameObject RollerBall;

	public Vector3 createPosition;

    public override void OnStartServer()
    {
		var pos = createPosition;

        var rotation = Quaternion.Euler(Random.Range(-90, 90), Random.Range(0, 180), Random.Range(0, 180));

        var bal = (GameObject)Instantiate(RollerBall, pos, rotation);
        NetworkServer.Spawn(bal);

        var objs = GameObject.FindGameObjectsWithTag("Border");
        foreach(var obj in objs)
        {
            obj.GetComponent<STGBorder>().target = bal.transform;
        }

    }
}