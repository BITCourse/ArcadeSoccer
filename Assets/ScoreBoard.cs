using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    public TextMesh scoreBlue;
    public TextMesh scoreRed;

	// Use this for initialization
	void Start ()
    {
        Score score = Score.Instance();
        score.onScored += updateScore;
        scoreBlue.text = score.scoreBlue.ToString();
        scoreRed.text = score.scoreRed.ToString();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void updateScore (int blue, int red)
    {
        Score score = Score.Instance();
        if (blue > 0) scoreBlue.text = score.scoreBlue.ToString();
        if (red > 0) scoreRed.text = score.scoreRed.ToString();
    }

}
