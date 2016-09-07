using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryTextShow : MonoBehaviour
{
    [SerializeField]
    private float delay = 0.0f;
    [SerializeField]
    private float interval = 1.0f;
    [SerializeField]
    private string[] textLines;


    private Text text;

    private int currentLine = -1;
    private float processTime;
    private bool ended;

    private VoiceStoryBoard voice;

	// Use this for initialization
	void Start ()
    {
        processTime = delay;
        ended = false;
        text = GetComponent<Text>();
        if(text == null)
            text = GetComponentInChildren<Text>();
        if(transform.position.y > 0)
            transform.Translate(0, -500, 0);
        voice = GetComponent<VoiceStoryBoard>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ended)
            return;
        processTime -= Time.deltaTime;
        if(processTime <= 0)
        {
            ++currentLine;
            if (currentLine == 0)
                transform.Translate(0, 500, 0);
            processTime += interval;
            if(currentLine >= textLines.Length)
            {
                ended = true;
                transform.Translate(0, -500, 0);
            }
            else if (currentLine >= 0)
            {
                if (text != null)
                    text.text = textLines[currentLine];
                if (voice)
                {
                    voice.playNextVoice();
                }
            }
        }
	}


}
