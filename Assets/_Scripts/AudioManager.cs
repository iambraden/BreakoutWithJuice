using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public float setPitch;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    private void Start(){ //default values and music starter
        setPitch = 2.0f;
        sfxSource.pitch = setPitch;
        sfxSource.volume = 0.4f;
        musicSource.volume = 0.6f;
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
            sfxSource.pitch = setPitch; //checks pitch before every play of sfx
            sfxSource.PlayOneShot(s.clip);
        }
    }
    
    //getters, setters, shortcuts
    public void decreasePitch(){
        setPitch -= 0.15f;
    }

    public float getPitch(){
        return setPitch;
    }

    public void resetPitch(){
        setPitch = 2.0f;
    }

    public void lastHit(){
        sfxSource.volume = 1.0f;
    }

    public void resetVolume(){
        sfxSource.volume = 0.6f;
    }

}
