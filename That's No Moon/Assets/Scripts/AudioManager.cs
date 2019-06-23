using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); // TODO: Add on a child gameobject in the future, maybe?
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            // TODO: How do I update these if they are changed while the game is running?
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Any clips you want playing from when the game begins, such as the theme, put here.
    }

    // To use this method anywhere in the code, call AudioManager.instance.Play("soundclipname");
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound clip " + name + " not found.");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound clip " + name + " not found.");
            return;
        }

        if (s.source.isPlaying)
            s.source.Stop();
        else
            Debug.LogError("Attempting to stop sound: " + name + ", but it is not playing.");
    }
}

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
