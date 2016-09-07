using UnityEngine;
using System.Collections;

public class VoiceStoryBoard : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] voices;

    [SerializeField]
    private bool randomOrder;

    private int currentVoice = 0;
    private int maxVoice = 0;

    void Start()
    {
        if (voices != null)
            maxVoice = voices.Length;
        if (randomOrder && maxVoice > 0)
            currentVoice = Random.Range(0, maxVoice - 1);
    }

    public void playNextVoice()
    {
        if (maxVoice <= 0 || !audioSource)
            return;
        if (currentVoice < maxVoice && currentVoice>=0)
            audioSource.PlayOneShot(voices[currentVoice]);
        currentVoice = (randomOrder ? Random.Range(0, maxVoice - 1) : currentVoice + 1);
    }

}
