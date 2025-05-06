using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private void Awake()
    {
        instance = this;
    }

    public AudioSource[] soundEffects;

    public void PlaySFX(int sfxToPlay) {
        soundEffects[sfxToPlay].Stop();
        soundEffects[sfxToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay, float startTime, float endTime) {
        soundEffects[sfxToPlay].Stop();
        soundEffects[sfxToPlay].time = startTime;
        soundEffects[sfxToPlay].Play();
        soundEffects[sfxToPlay].SetScheduledEndTime(AudioSettings.dspTime + endTime - startTime);
    }

    public void PlaySFXPitched(int sfxToPlay) {
        if (sfxToPlay < 0) {
            return;
        }
        soundEffects[sfxToPlay].pitch = Random.Range(.8f, 1.2f);
        PlaySFX(sfxToPlay);
    }

    public void PlaySFXPitched(int sfxToPlay, float startTime, float endTime) {
        if (sfxToPlay < 0) {
            return;
        }
        soundEffects[sfxToPlay].pitch = Random.Range(.8f, 1.2f);
        PlaySFX(sfxToPlay, startTime, endTime);
    }
}
