using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourcesOnAdition;

    [SerializeField] AudioSource audioSourseOnFade;

    void Start()
    {
        audioSourcesOnAdition.GetComponent<AudioSource>();

        audioSourseOnFade.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        TouchManager.onSoundAdition += PlaySoundAdition;

        TouchManager.onSoundFade += PlaySoundFade;
    }

    private void OnDisable()
    {
        TouchManager.onSoundAdition -= PlaySoundAdition;

        TouchManager.onSoundFade -= PlaySoundFade;
    }

    private void PlaySoundFade()
    {
        audioSourseOnFade.Play();
    }

    private void PlaySoundAdition()
    {
        audioSourcesOnAdition.PlayOneShot(audioSourcesOnAdition.clip);
    }
}
