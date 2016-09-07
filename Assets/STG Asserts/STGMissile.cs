using UnityEngine;
using System.Collections;

public class STGMissile : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    [SerializeField]
    public float deadTime = 10.0f;

    [SerializeField]
    private float speed;
    [SerializeField]
	private float lookSpeed;

    [SerializeField]
    private GameObject explosion;
	
	void Start ()
    {

	}
	
	void Update ()
    {
        deadTime -= Time.deltaTime;
        if(deadTime < 0)
        {
            OnHitObject(null);
            return;
        }
        if (target)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
        }
        transform.Translate(0, 0, speed / 100);
	}

    void OnHitObject(GameObject obj)
    {
        bool flag = !obj;

        // 测试tag
        if (obj && !obj.CompareTag("Player") && !obj.CompareTag("Missile"))
        {
            //Debug.Log("collided " + GetInstanceID());
            var script = obj.GetComponent<STGDestroyable>(); // 测试是否包含组件
            if (script != null)
            {
                script.triggerHitted();
            }
            flag = true;
        }

        if(flag)
        {
            Destroy(Instantiate(explosion, transform.position, transform.rotation), 3.0f);
            Destroy(gameObject, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        OnHitObject(collision.gameObject);
	}

}
