using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        playMusic("Music");
    }
        

    public void playMusic(string name){
        Sound s = Array.Find(musicSounds, sound => sound.clipName == name);

        if(s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
        }else{
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name){
        Sound s = Array.Find(sfxSounds, sound => sound.clipName == name);

        if(s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
        }else{
            sfxSource.PlayOneShot(s.clip);
        }
    }

}
