using UnityEngine;
using System.Collections;

public class GoalChecker : MonoBehaviour {

    public GameObject ball;
    public GameObject showWhenGoal = null;

    public enum GoalSide { Blue, Red };
    public GoalSide goalSide;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball)
        {
            Score score = Score.Instance();
            if(goalSide == GoalSide.Blue)
            {
                score.score(1, 0);
            }
            else
            {
                score.score(0, 1);
            }

            showWhenGoal.SetActive(true);
            Invoke("RemovePrefab", 3);

        }

    }

    void RemovePrefab()
    {
        showWhenGoal.SetActive(false);
    }

}
