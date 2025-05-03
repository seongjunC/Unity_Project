using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using EnumType;

public class AudioManager : Singleton<AudioManager>
{
    private AudioMixer audioMixer;
    private AudioMixerGroup bgmGroup;
    private AudioMixerGroup effectGroup;

    private AudioSource bgmSource;
    private AudioSource effectSource;

    private Dictionary<string, AudioClip> effectCached;

    private Transform audioParent;

    private const string SOUND_PATH = "Sound/";

    private void Awake()
    {
        effectCached = new Dictionary<string, AudioClip>();
    }

    public void Init()
    {
        audioMixer = Manager.Resources.Load<AudioMixer>("Sound/AudioMixer");
        bgmGroup = audioMixer.FindMatchingGroups("BGM")[0];
        effectGroup = audioMixer.FindMatchingGroups("Effect")[0];

        audioParent = new GameObject("Audio").transform;
        GameObject bgm = new GameObject("BGM_Source");
        bgm.transform.parent = audioParent;
        bgmSource = bgm.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.outputAudioMixerGroup = bgmGroup;

        GameObject effect = new GameObject("Effect_Source");
        effect.transform.parent = audioParent;
        effectSource = effect.AddComponent<AudioSource>();
        effectSource.outputAudioMixerGroup = effectGroup;
    }

    public void PlaySound(string path, SoundType type, float pitch = 1)
    {
        if (string.IsNullOrEmpty(path)) return;

        AudioClip clip = LoadClip(path, type);

        if (clip != null)
            PlaySound(clip, type, pitch);
    }

    public void PlaySound(AudioClip clip, SoundType type, float pitch = 1)
    {
        switch (type)
        {
            case SoundType.BGM:
                bgmSource.Stop();
                bgmSource.clip = clip;
                bgmSource.pitch = pitch;
                bgmSource.Play();
                break;
            case SoundType.Effect:
                effectSource.pitch = pitch;
                effectSource.PlayOneShot(clip);
                break;
        }
    }

    private AudioClip LoadClip(string path, SoundType type)
    {
        if(type == SoundType.Effect)
        {
            if (effectCached.TryGetValue(path, out var effect))
                return effect;

            AudioClip clip = Manager.Resources.Load<AudioClip>($"{SOUND_PATH}{type}/{path}");

            if(clip != null)
                effectCached[path] = clip;

            return clip;
        }

        return Manager.Resources.Load<AudioClip>($"{SOUND_PATH}{type}/{path}");
    }

    public void SetVolume(string exposedParam, float volume)
    {
        if(audioMixer)
            audioMixer.SetFloat(exposedParam, volume);
    }

    public float GetVolume(string exposedParam)
    {
        if(audioMixer.GetFloat(exposedParam, out float volume))
        {
            return volume;
        }
        return -80;
    }

    public void SliderValue(string parameter, float _value, float multiplier) => audioMixer.SetFloat(parameter, Mathf.Log10(_value) * multiplier);

    public void ClearEffectCached() => effectCached.Clear();
}
