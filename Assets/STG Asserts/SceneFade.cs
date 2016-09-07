using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    [SerializeField]
    private float timeFadeIn;
    [SerializeField]
    private float timeFadeOut;
    [SerializeField]
    private float totalTime;

    [SerializeField]
    private string sceneAfterFadeOut;

    private RawImage image;


	// Use this for initialization
	void Start () 
    {
        image = GetComponent<RawImage>();
        image.color = new Color(0, 0, 0, 1);
	}
	
	// Update is called once per frame
	void Update ()
    {
        float t = Time.timeSinceLevelLoad;
	    if(t < timeFadeIn)
        {
            image.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, t / timeFadeIn));
        }
        else if(t > totalTime - timeFadeOut)
        {
            image.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, (totalTime - t) / timeFadeOut));
            if (t > totalTime && sceneAfterFadeOut.Length>0)
            {
                Application.LoadLevel(sceneAfterFadeOut);
            }
        }
        else
        {
            image.color = new Color(0, 0, 0, 0);
        }
	}

}
