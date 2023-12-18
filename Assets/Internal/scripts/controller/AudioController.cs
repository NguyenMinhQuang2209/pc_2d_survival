using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioSource bgAudio;

    public float audioBgVolume = 1f;

    public Slider bgVolumeSlider;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        bgAudio.volume = audioBgVolume;
        bgAudio.loop = true;
        bgAudio.Play();

        bgVolumeSlider.maxValue = 1f;
        bgVolumeSlider.minValue = 0f;
        bgVolumeSlider.value = audioBgVolume;
        bgAudio.volume = audioBgVolume;
    }
    private void Update()
    {
        ChangeVolume(bgVolumeSlider.value);
    }
    public void ChangeVolume(float v)
    {
        audioBgVolume = v <= 1f ? v : 1f;
        bgAudio.volume = audioBgVolume;
    }
}
