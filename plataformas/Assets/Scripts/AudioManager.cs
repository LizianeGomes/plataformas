using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource systemSource;
    private List<AudioSource> activateSources;

    #region Singleton

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            systemSource = GetComponent<AudioSource>();
            activateSources = new List<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    #endregion

    #region AudioControls

    public void Play(AudioClip clip, AudioSource source)
    {
        if(!activateSources.Contains(source)) 
            activateSources.Add(source);
        source.Stop();
        source.clip = clip;
        source.Play();
        
    }

    
    public void Stop(AudioSource source)
    {
        if(activateSources.Contains(source))
        source.Stop(); 
        activateSources.Remove(source);
        
    }
    
    
    public void Pause()
    {
        if()
        systemSource.Pause();
    }

    
    public void Resume()
    {
        systemSource.UnPause();
    }

    public void PlayOneShot(AudioClip clip)
    {
        systemSource.PlayOneShot(clip);
    }
    
    #endregion

}