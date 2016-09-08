using UnityEngine;
using System.Collections;

public class Score {

    static Score instance;
    static public Score Instance()
    {
        if(instance == null)
        {
            instance = new Score();
        }
        return instance;
    }

    public int scoreBlue;
    public int scoreRed;

    public delegate void ScoreAction();
    public event ScoreAction onScored;

    public void score(int blue, int red)
    {
        scoreBlue += blue;
        scoreRed += red;
        if(onScored != null)
            onScored();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
