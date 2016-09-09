using UnityEngine;
using UnityEngine.Networking;

public class Score : NetworkBehaviour {

    static Score instance;
    static public Score Instance()
    {
        if(instance == null)
        {
            instance = (Score)GameObject.FindObjectOfType(typeof(Score));
            if (instance.gameObject == null)
                instance = null;
            if (instance == null)  
                Debug.LogError("Singleton instance of Score not found on either GameObject");   
        }
        return instance;
    }

    [SyncVar]
    public int scoreBlue;
    [SyncVar]
    public int scoreRed;

    public delegate void ScoreAction(int blue, int red);
    public event ScoreAction onScored;

    public void score(int blue, int red)
    {
        if(isServer)
            RpcOnScore(blue, red);
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    [ClientRpc]
    void RpcOnScore(int blue, int red)
    {
        scoreBlue += blue;
        scoreRed += red;
        onScored(blue, red);
    }

}
