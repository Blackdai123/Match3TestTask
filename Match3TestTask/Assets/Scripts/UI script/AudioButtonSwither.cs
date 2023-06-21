using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonSwither : MonoBehaviour
{
    [SerializeField] Button buttonSwither;

    [SerializeField] List<AudioSource> audioSources;

    bool buttonState = true;

    void Start()
    {
        buttonSwither.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        buttonState = !buttonState;

        if (buttonState)
        {
            transform.localPosition = new Vector3(90, transform.localPosition.y, transform.localPosition.z);

            foreach (var source in audioSources) 
            {
                source.GetComponent<AudioSource>();

                source.mute = false;            
            }
        }
        else
        {
            transform.localPosition = new Vector3(-90, transform.localPosition.y, transform.localPosition.z);

            foreach (var source in audioSources)
            {
                source.GetComponent<AudioSource>();

                source.mute = true;
            }
        }
    }
}

