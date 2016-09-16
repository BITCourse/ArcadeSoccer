using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DJPush : MonoBehaviour
{
    public CommonController charactor;
    public float ballForce = 360f;
    public float charactorPushSpeed = 15;
    public float charactorPushVerticalSpeed = 5;

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
            // 抬高玩家一点，以便该玩家被判定为离地
            other.transform.localPosition += Vector3.up * 0.11f;

            Vector3 direction = other.transform.localPosition - charactor.transform.localPosition;
            direction.y *= 0.2f;
            direction.Normalize();

            other.GetComponent<Rigidbody>().velocity =
                charactorPushSpeed * direction + Vector3.up * charactorPushVerticalSpeed;
        }
    }

}
