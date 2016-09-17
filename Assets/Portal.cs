using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Transform portalOut;
    float yOffset = 3f;

    public GameObject portalEffect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Vector3 newPos = other.GetComponent<Rigidbody>().position + (portalOut.position - transform.position);
            newPos.y = portalOut.position.y + yOffset;
            other.GetComponent<Rigidbody>().position = newPos;
            portalEffect.SetActive(false);
            portalEffect.SetActive(true);
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().position = portalOut.position;
            portalEffect.SetActive(false);
            portalEffect.SetActive(true);
        }
    }

}
