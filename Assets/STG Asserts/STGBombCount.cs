using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class STGBombCount : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteBomb;

    [SerializeField]
    private GameObject imageObject;

    [SerializeField]
    private GameObject textObject;

    private Image image;
    private Text text;

    private int maxCount = 0;

    public int count { get; private set; }

    public void setCount(int x)
    {
        count = x;
        if (x == 0)
        {
            if (image)
                image.sprite = null;
        }
        if (x >= 0 && x < maxCount)
        {
            if (text)
                text.enabled = false;
            if (image && maxCount > 0)
                image.sprite = spriteBomb[count];
        }
        else
        {
            if (image && maxCount > 1)
                image.sprite = spriteBomb[1];
            if (text)
            {
                text.enabled = true;
                text.text = "x " + count.ToString();
            }
        }
        
    }

	// Use this for initialization
	void Start ()
    {
        if (textObject)
            text = textObject.GetComponent<Text>();
        if (imageObject)
            image = imageObject.GetComponent<Image>();
        if (spriteBomb != null)
            maxCount = spriteBomb.Length;
        setCount(5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
