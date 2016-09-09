using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BallReset : NetworkBehaviour {

    public Vector3 resetPosition = new Vector3();

    public GameObject showWhenGoal;

    private bool resetInProgress = false;
    private float deadTime = -20;

    private Rigidbody rigid;
    private Collider coll;

    // Use this for initialization
    void Start () {
        Score.Instance().onScored += onScored;
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (resetInProgress)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 1,
                Mathf.Max(0.65f - (Time.time - deadTime) * 0.3f, 0.0f));
        }

    }

    void onScored(int blue, int red)
    {
        if (resetInProgress)
            return;
        resetInProgress = true;
        deadTime = Time.time;
        rigid.useGravity = false;
        rigid.velocity *= 0.3f;
        showWhenGoal.SetActive(true);
        Invoke("resetBall", 2.5f);
    }

    void resetBall()
    {
        transform.position = resetPosition;
        rigid.velocity = Vector3.zero;
        rigid.useGravity = true;
        showWhenGoal.SetActive(false);
        GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.65f);
        resetInProgress = false;
    }

}
