using UnityEngine;
using System.Collections;

public class STGDestroyable : MonoBehaviour {

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int totalHitCount = 1;

    public bool invincible { get; set; }
    public bool hovered { get; private set; }

    private Collider coll;
    private Rigidbody rigid;
    private Renderer rend;

    private Color originColor;

    private int currHit = 0;

    private float timeAfterHit = 0.0f;

	// Use this for initialization
	void Start ()
    {
        coll = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();

        if (rend != null && rend.material != null)
        {
            originColor = rend.material.GetColor("_Color");
        }

        if (totalHitCount <= 0)
            totalHitCount = 1;

        invincible = false;
	}
	
	// 这里主要是实现对象被破坏后的淡出效果
	void LateUpdate ()
    {
        if (currHit >= totalHitCount)
        {
            timeAfterHit += Time.deltaTime;
            originColor.a = (timeAfterHit > 0.6f ? 0 : 1 - timeAfterHit / 0.6f);
            //Debug.Log(originColor);
            rend.material.SetColor("_Color", originColor);
        }
	}

    public void setHover(bool isHovered)
    {
        hovered = isHovered;
        if (rend != null && rend.material != null)
        {
            rend.material.SetFloat("_OutlineOpacity", isHovered ? 1.0f : 0.0f);
            rend.material.SetColor("_Color", Color.Lerp(originColor, new Color(1, 1, 0.5f), isHovered ? 0.3f : 0));
        }
    }

    // 触发碰撞，有可能调用onHitted和onDestroy方法
    public void triggerHitted(int count = 1)
    {
        if (invincible || count < 1)
            return;
        currHit += count;
        if (currHit > totalHitCount)
            currHit = totalHitCount;
        onHitted();
        if (currHit >= totalHitCount)
            onDestroy();
    }

    virtual protected void onHitted()
    {
        if (rend != null && rend.material != null)
        {
            originColor = Color.Lerp(originColor, new Color(0.5f, 0.2f, 0), 0.3f); // 利用差值计算变深后的颜色
            rend.material.SetColor("_Color", originColor);
        }
    }

    virtual protected void onDestroy()
    {
        if (rigid != null) // 禁用刚体
            rigid.isKinematic = true;
        if (coll != null) // 禁用碰撞
            coll.enabled = false;
        if (prefab != null) // 加入爆炸效果
        {
            GameObject obj = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
            obj.transform.localScale *= 0.6f;
            Destroy(obj, 3.0f);
        }
        Destroy(gameObject, 0.6f); // 删除自身
    }


}
