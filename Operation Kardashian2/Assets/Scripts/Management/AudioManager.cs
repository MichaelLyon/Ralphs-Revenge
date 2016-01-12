using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AudioManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }


    void Update()
    {
        AudioListener.volume = PersistentData.Volume;
    }

    public AudioClip splash;
    public void PlaySplash()
    {
        AudioSource.PlayClipAtPoint(splash, Vector3.zero);
    }

    public AudioClip menuMusic;
    public void PlayMenuMusic()
    {
        AudioSource.PlayClipAtPoint(menuMusic, Vector3.zero);
    }

    public AudioClip buttonPress;
    public void PlayButtonPress()
    {
        AudioSource.PlayClipAtPoint(buttonPress, Vector3.zero);
    }

    public AudioClip buttonPurchase;
    public void PlayButtonPurchase()
    {
        AudioSource.PlayClipAtPoint(buttonPurchase, Vector3.zero);
    }
}
