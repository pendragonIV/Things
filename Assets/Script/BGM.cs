using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class BGM : MonoBehaviour
{
    [SerializeField]
    public AudioSource worldDefault;
    [SerializeField]
    public AudioSource worldCombat;
    [SerializeField]
    public AudioSource worldHome;

    public AudioSource currentPlay;

    public static BGM instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        if(currentPlay == null)
        {
            if(SceneManager.GetActiveScene().name == "World")
            {
                PlayWorldDefaultMusic();
            }
            else if(SceneManager.GetActiveScene().name == "House")
            {
                PlayWorldHomeMusic();
            }
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "World")
        {
            if (GameManager.instance.player.GetComponent<Player>().enemies.Count > 0)
            {
                if (!worldCombat.isPlaying)
                {
                    PlayWorldCombatMusic();
                }
            }
            else
            {
                if (!worldDefault.isPlaying)
                {
                    PlayWorldDefaultMusic();
                }
            }
        }
        else if(SceneManager.GetActiveScene().name == "House")
        {
            if (!worldHome.isPlaying)
            {
                PlayWorldHomeMusic();
            }
        }
    }


    public void PlayWorldDefaultMusic()
    {
        worldDefault.Play();
        worldCombat.Stop();
        worldHome.Stop();
        currentPlay = worldDefault;
    }
    public void PlayWorldCombatMusic()
    {
        worldDefault.Stop();
        worldCombat.Play();
        worldHome.Stop();   
        currentPlay = worldCombat;
    }
    public void PlayWorldHomeMusic()
    {
        worldDefault.Stop();
        worldCombat.Stop();
        worldHome.Play();
        currentPlay = worldHome;
    }

    public float fadeTime = 1; // fade time in seconds

    public void FadeSound(AudioSource source)
    {
        if (fadeTime == 0)
        {
            source.volume = 0;
            return;
        }
        StartCoroutine(_FadeSound(source));
    }

    private IEnumerator _FadeSound(AudioSource source)
    {
        float t = fadeTime;
        while (t > 0)
        {
            yield return null;
            t -= Time.deltaTime;
            source.volume = t / fadeTime;
        }
        yield break;
    }

}
