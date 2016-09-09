using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GoalChecker : NetworkBehaviour {
    
    public GameObject showWhenGoal = null;

    public enum GoalSide { Blue, Red };
    public GoalSide goalSide;

    // Use this for initialization
    void Start () {
        Score.Instance().onScored += OnScore;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (!isServer)
            return;

        if (other.gameObject.tag == "Ball")
        {
            Score score = Score.Instance();
            if (goalSide == GoalSide.Blue)
            {
                score.score(1, 0);
            }
            else
            {
                score.score(0, 1);
            }

        }

    }

    void OnScore(int blue, int red)
    {
        if((goalSide == GoalSide.Blue ? blue : red) > 0)
        {
            showWhenGoal.SetActive(true);
            Invoke("RemovePrefab", 3);
        }
    }

    void RemovePrefab()
    {
        showWhenGoal.SetActive(false);
    }

}
