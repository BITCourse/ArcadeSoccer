using UnityEngine;
using System.Collections;

public class STGSceneMove : MonoBehaviour
{
    [SerializeField]
    private Transform targetsParent; // 移动路径节点所在的父对象

    private Transform[] targets; // 路径节点

    [SerializeField]
    private float[] times; // 每段路径的时长

    private int maxSegment;
    private int currSegment;
    private float remainTime;


	// Use this for initialization
	void Start ()
    {
        maxSegment = targetsParent.childCount;
        targets = new Transform[maxSegment];

        // 从targetsParent获取所有的节点
        for(int i = 0; i < maxSegment; ++i)
        {
            targets[i] = targetsParent.GetChild(i);
        }

        maxSegment = Mathf.Min(maxSegment, times.Length + 1);
        currSegment = -1;
        remainTime = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        remainTime -= Time.deltaTime;

        // 维护当前节点
        while(remainTime <= 0 && currSegment + 1 < maxSegment)
        {
            ++currSegment;
            if (currSegment + 1 < maxSegment)
                remainTime += times[currSegment];
        }

        // 插值计算位置和角度
        if (currSegment < maxSegment - 1)
        {
            transform.position = Vector3.Lerp(targets[currSegment].position, targets[currSegment + 1].position, 1 - remainTime / times[currSegment]);
            transform.rotation = Quaternion.Lerp(targets[currSegment].rotation, targets[currSegment + 1].rotation, 1 - remainTime / times[currSegment]);
        }
	}
}
