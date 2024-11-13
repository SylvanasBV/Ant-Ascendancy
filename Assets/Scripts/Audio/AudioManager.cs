using System;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public static AudioManager Instance;
    [SerializeField] AudioSource musicSource, effectsSource;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void PlaySFX(string name)
    {
        //Search the name of the audio if it´s the same to the Array
        Sound sound = Array.Find(sfxSounds, (soundSFX) => soundSFX.name == name);

        if (sound == null)
        {
            Debug.Log("Don´t found the audio");
        }

        else
        {
            effectsSource.PlayOneShot(sound.clip);
        }
    }
    public void PlayMusic(string name)
    {
        //Search the name of the audio if it´s the same to the Array
        Sound sound = Array.Find(musicSounds, (soundMusic) => soundMusic.name == name);

        if (sound == null)
        {
            Debug.Log("Don´t found the audio");
        }

        else
        {
            musicSource.Stop();
            musicSource.clip = sound.clip;
            musicSource.Play();
            musicSource.loop = true;
        }

    }
}
