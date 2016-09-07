using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private Transform spawnParent;

    [SerializeField]
    private int maxCount = 20;

    [SerializeField]
    private Vector3 randSize;

    [SerializeField]
    private double spawnInterval = 1.0;

    private int currCount = 0;
    private double processTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        double newTime = processTime + Time.deltaTime;
        while (currCount < maxCount && newTime > spawnInterval)
        {
            Vector3 randv = new Vector3(Random.value, Random.value, Random.value);
            randv = randv * 2 - Vector3.one;
            randv.Scale(randSize);
            GameObject obj = (GameObject)Instantiate(prefab, transform.position + randv, transform.rotation);
            if (spawnParent != null)
                obj.transform.parent = spawnParent;
            ++currCount;
            newTime -= spawnInterval;
        }
        if (currCount < maxCount)
            processTime = newTime;
        else
            processTime = 0.0;
	}
}
