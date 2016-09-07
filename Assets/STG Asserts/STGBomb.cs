using UnityEngine;
using System.Collections;
using STGAsserts;

public class STGBomb : MonoBehaviour
{
    [SerializeField]
    public Vector3 targetPos;

    [SerializeField]
    public float deadTime = 10.0f;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxExplosionDistance = 50.0f;
    [SerializeField]
    private float maxHit = 5;

    [SerializeField]
    private GameObject explosion;

	// Use this for initialization
	void Start ()
    {
        transform.LookAt(targetPos);
	}

    void Update()
    {
        deadTime -= Time.deltaTime;
        if (deadTime < 0)
        {
            OnHitObject(null);
            return;
        }
        transform.Translate(0, 0, speed / 100);
    }

    void OnHitObject(GameObject obj)
    {
        if (!obj || obj.CompareTag("Player") || obj.CompareTag("Missile"))
            return;

        var arr = GameObject.FindGameObjectsWithTag("Destroyable");
        float dis;
        STGDestroyable s;

        foreach(var des in arr)
        {
            s = des.GetComponent<STGDestroyable>();
            if(!s) continue;
            dis = Vector3.Distance(des.transform.position, transform.position);
            if(dis < maxExplosionDistance)
            {
                s.triggerHitted(Mathf.CeilToInt(maxHit * (1 - dis / maxExplosionDistance)));
            }
        }

        GameObject b = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
        b.transform.localScale *= 3.0f;
        Destroy(b, 3.0f);
        Destroy(gameObject, 0);

    }

    void OnCollisionEnter(Collision collision)
    {
        OnHitObject(collision.gameObject);
    }
}
