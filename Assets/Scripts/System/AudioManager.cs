using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField]
    private AudioSource m_BGMSource;
    [SerializeField]
    private AudioSource m_SESource;

    private Dictionary<string, AudioClip> m_BGMClip = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> m_SEClip = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        object[] bgmList = Resources.LoadAll("Audio/BGM");
        object[] seList = Resources.LoadAll("Audio/SE");

        foreach (AudioClip bgm in bgmList)
        {
            m_BGMClip[bgm.name] = bgm;
        }

        foreach (AudioClip se in seList)
        {
            m_SEClip[se.name] = se;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void PlayBGM(string name)
    {
        if (!m_SEClip.ContainsKey(name))
        {
            Debug.Log(name + "が見つかりません");
            return;
        }

        m_BGMSource.clip = m_BGMClip[name] as AudioClip;
        m_BGMSource.Play();
    }

    public void PlaySE(string name)
    {
        if (!m_SEClip.ContainsKey(name))
        {
            Debug.Log(name + "が見つかりません");
            return;
        }

        m_SESource.PlayOneShot(m_SEClip[name] as AudioClip);
    }

    public void AudioMute(bool f)
    {
        m_BGMSource.mute = f;
        m_SESource.mute = f;
    }

    public void ChangeVolume(float bgmVolume, float seVolume)
    {
        m_BGMSource.volume = bgmVolume;
        m_SESource.volume = seVolume;
    }
}