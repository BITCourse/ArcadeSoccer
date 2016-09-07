using UnityEngine;
using System.Collections;

public class DelayedEnable : MonoBehaviour
{
    [SerializeField]
    private GameObject operatingObject;

    [SerializeField]
    private float delay;

    [SerializeField]
    private bool disableRatherThanEnable = false;
    [SerializeField]
    private bool unsetWhenStart = false;

    void Start ()
    {
        if (unsetWhenStart)
            operatingObject.SetActive(disableRatherThanEnable);
    }

	// Update is called once per frame
	void Update ()
    {
        if (delay <= 0.0f)
            return;
        delay -= Time.deltaTime;
        if (delay <= 0.0f && operatingObject)
        {
            operatingObject.SetActive(!disableRatherThanEnable);
        }
	}

}
