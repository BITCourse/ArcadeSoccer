using UnityEngine;
using System.Collections;
using STGAsserts;

public class STGClickFire : MonoBehaviour
{
    [SerializeField]
    private GameObject missile;

    [SerializeField]
    private GameObject bomb;

    [SerializeField]
    private GameObject bombPanel;

    [SerializeField]
    private float timeAfterTargetOut = 0.5f;

    [SerializeField]
    private Vector3 offsetPushMissile = Vector3.zero;

    [SerializeField]
    private AudioSource soundShoot;

    private STGDestroyable lastObj = null;
    private float timeOut = 0.0f;

    private STGBombCount bc;

	// Use this for initialization
	void Start ()
    {
        var script = missile.GetComponent<STGMissile>();
        if (script == null)
            missile = null;
        if (bombPanel)
            bc = bombPanel.GetComponent<STGBombCount>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 利用光线追踪获得从摄像头出发经过鼠标位置的射线
        RaycastHit hit;
        bool flagRT = true;
        bool flagCanHit = Physics.Raycast(ray, out hit); // 射线是否与其他对象相交？
        if (flagCanHit)
        {
            var obj = hit.collider.gameObject;
            if (obj.tag == "Destroyable")
            {
                var beh = obj.GetComponent<STGDestroyable>();
                if (beh != null) // 鼠标指向一个“可破坏”的对象
                {
                    if (lastObj != null)
                        lastObj.setHover(false);
                    lastObj = beh;
                    lastObj.setHover(true);
                    timeOut = timeAfterTargetOut;
                    flagRT = false;
                }
            }
        }

        // 设置鼠标停留时的效果
        if (flagRT)
        {
            timeOut -= Time.deltaTime;
            if (timeOut <= 0 && lastObj != null)
            {
                lastObj.setHover(false);
                lastObj = null;
            }
        }

        // 左键发射导弹，要求目标必须是一个可破坏的物体
        if (Input.GetButtonDown("Fire1") && lastObj != null)
        {
            if (missile == null) // 没有指定导弹prefab则直接造成伤害
            {
                lastObj.triggerHitted();
            }
            else // 否则发射导弹
            {
                GameObject mis = (GameObject)Instantiate(missile, transform.position + offsetPushMissile, transform.rotation);
                offsetPushMissile.x = -offsetPushMissile.x;
                var script = mis.GetComponent<STGMissile>();
                if (script != null)
                    script.target = lastObj.transform;
                if (soundShoot)
                    soundShoot.Play();
            }
        }

        // 右键发射炸弹，只要鼠标指向一个地点就好
        if (Input.GetButtonDown("Fire2") && flagCanHit)
        {
            if (bomb != null && (bc == null || (bombPanel.activeInHierarchy && bc.count > 0)))
            {
                GameObject mis = (GameObject)Instantiate(bomb, transform.position, transform.rotation);
                var script = mis.GetComponent<STGBomb>();
                if (script != null)
                    script.targetPos = hit.point;
                if (soundShoot)
                    soundShoot.Play();
                if (bc)
                    bc.setCount(bc.count - 1);
            }
        }
	}
}
