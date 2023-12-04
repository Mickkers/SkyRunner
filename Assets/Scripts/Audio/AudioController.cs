using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;
    [Header("Audio Clips")]
    [SerializeField] private AudioSource clickUI;
    [SerializeField] private AudioSource walk;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource damage;
    [SerializeField] private AudioSource enemy;
    [SerializeField] private AudioSource gameover;
    [SerializeField] private AudioSource attack;
    [SerializeField] private AudioSource reload;

    void Awake()
    {
        float sfxDB;
        float bgmDB;
        audioMixer.GetFloat("sfxVol", out sfxDB);
        audioMixer.GetFloat("bgmVol", out bgmDB);
        sfxSlider.value = GetFloatVolume(sfxDB);
        bgmSlider.value = GetFloatVolume(bgmDB);
    }

    public void ReloadSFX()
    {
        reload.Play();
    }
    public void AttackSFX()
    {
        attack.Play();
    }
    public void GameoverSFX()
    {
        gameover.Play();
    }

    public void EnemySFX()
    {
        enemy.Play();
    }

    public void DamageSFX()
    {
        damage.Play();
    }

    public void JumpSFX()
    {
        jump.Play();
    }

    public void WalkSFX(bool value)
    {
        walk.mute = !value;
    }

    public void ClickSFX()
    {
        clickUI.Play();
    }

    private float GetFloatVolume(float value)
    {
        return Mathf.Pow(10, value/20);
    }

    private float GetDb(float value)
    {
        return Mathf.Log10(value) * 20;
    }

    public void SetBGM(float value)
    {
        audioMixer.SetFloat("bgmVol", GetDb(value));
    }

    public void SetSFX(float value)
    {
        audioMixer.SetFloat("sfxVol", GetDb(value));
    }
}

