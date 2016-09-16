using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DJPush : MonoBehaviour
{
    public CommonController charactor;
    public float ballForce = 360f;
    public float charactorPushSpeed = 12;
    public float charactorPushVerticalSpeed = 2;

    private float proc = 0.0f;
    private bool ballPushed = true;

    public void push()
    {
        gameObject.SetActive(false);
        proc = 4.5f;
        ballPushed = false;
        gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start()
    {
        if (charactor == null)
        {
            charactor = GetComponentInParent<CommonController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        proc *= Mathf.Exp(-Time.deltaTime * 5f);

        transform.localPosition = new Vector3(0, 0.9f, 0.45f * proc);
        
        transform.localRotation = Quaternion.Euler(charactor.getViewRotation().eulerAngles.x, 0, 0);

        if (proc > 0.1f)
            transform.localScale = new Vector3(proc, proc, proc);
        else
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            gameObject.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball" && !ballPushed)
        {
            ballPushed = true;
            Quaternion direction = charactor.getViewRotation();
            other.GetComponent<Rigidbody>().AddForce(direction * Vector3.forward * ballForce, ForceMode.Impulse);
        }
        else if (other.tag == "Player")
        {
            Vector3 direction = charactor.transform.position - transform.position;
            direction.Normalize();
            other.GetComponent<Rigidbody>().velocity =
                charactorPushSpeed * direction + Vector3.up * charactorPushVerticalSpeed;
        }
    }

}
