using UnityEngine;
using System.Collections;

public class GoalChecker : MonoBehaviour {

    public GameObject ball;
    public GameObject showWhenGoal = null;

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
            showWhenGoal.SetActive(true);
            Invoke("RemovePrefab", 3);
        }

    }

    void RemovePrefab()
    {
        showWhenGoal.SetActive(false);
    }

}
