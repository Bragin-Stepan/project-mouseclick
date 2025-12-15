using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudionManager : MonoBehaviour
{
    [SerializeField] private List<AudioClipData> _gameMusic;
    [SerializeField] private List<AudioClipData> _sfx;
    
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;
    
    private AudioDatabase _audioDB;
    private bool _isBgmPlay;
    
    private const float MinValuePitch = 0.95f;
    private const float MaxValuePitch = 1.1f;

    private void Awake()
    {
        _audioDB = new AudioDatabase(_gameMusic, _sfx);
    }

    public void PlaySFX(string nameKey)
    {
        AudioClipData data = _audioDB.Get(nameKey);
        if (data == null) return;

        AudioClip clip = data.GetRandomClip();
        if (clip == null) return;

        _sfxSource.clip = clip;
        _sfxSource.pitch = UnityEngine.Random.Range(MinValuePitch, MaxValuePitch);
        _sfxSource.volume = data.Volume;
        _sfxSource.PlayOneShot(clip);
    }
    
    public void StartBGM(string nameKey)
    {
        AudioClipData data = _audioDB.Get(nameKey);
        AudioClip nextMusic = data.GetRandomClip();
        
        if (data == null || nextMusic == null)
        {
            Debug.LogError("Audio: null music group " + nameKey);
            return;
        }
        
        _bgmSource.clip = nextMusic;
        _bgmSource.volume = data.Volume;
        _bgmSource.Play();
    }

    public void StopBGM()
    {
        _bgmSource.Stop();
    }
}
