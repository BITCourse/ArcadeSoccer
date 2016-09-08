using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    public TextMesh scoreBlue;
    public TextMesh scoreRed;

	// Use this for initialization
	void Start () {
        Score.Instance().onScored += updateScore;
        scoreBlue.text = "0";
        scoreRed.text = "0";
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void updateScore ()
    {
        Score score = Score.Instance();
        scoreBlue.text = score.scoreBlue.ToString();
        scoreRed.text = score.scoreRed.ToString();
    }

}
