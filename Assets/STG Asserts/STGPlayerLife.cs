using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class STGPlayerLife : MonoBehaviour
{
    [SerializeField]
    private int remainLife = 5;

    [SerializeField]
    private float invincibleTime = 5.0f;

    [SerializeField]
    private GameObject prefabExplode;

    [SerializeField]
    private Text textLife;

    private float remainInvTime;

    private Collider coll;

	// Use this for initialization
	void Start ()
    {
        coll = GetComponent<Collider>();
        triggerInvincible();
        if (textLife != null)
            textLife.text = remainLife.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (remainInvTime>0)
            remainInvTime-=Time.deltaTime;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (remainInvTime <= 0 && !collision.gameObject.CompareTag("Missile"))
        {
            triggerMiss();
        }

    }

    void triggerInvincible()
    {
        remainInvTime = invincibleTime;
    }

    void triggerDeath()
    {
        gameObject.SetActive(false);
        if (coll != null)
            coll.enabled = false;
        GameObject obj = (GameObject)Instantiate(prefabExplode, transform.position, transform.rotation);
        obj.transform.localScale *= 4.0f;
        Destroy(obj, 3.0f);
    }

    void triggerMiss()
    {
        if (prefabExplode != null)
        {
            GameObject obj = (GameObject)Instantiate(prefabExplode, transform.position, transform.rotation);
            obj.transform.localScale *= 2.5f;
            Destroy(obj, 3.0f);
        }
        if (remainLife<=0)
        {
            triggerDeath();
        }
        else
        {
            --remainLife;
            textLife.text = remainLife.ToString();
            triggerInvincible();
        }
        Debug.Log("miss " + remainLife);
    }

}
