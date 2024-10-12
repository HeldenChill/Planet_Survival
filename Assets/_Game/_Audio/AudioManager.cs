using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    using Base;
    using Common;
    using DesignPattern;

    [System.Serializable]
    public struct AudioSourceData
    {
        [SerializeField]
        private AudioSource audioSource;
        [HideInInspector]
        public ObjectContainer audioSourcePool;

        public Audio currentAudio;

        [SerializeField] private float baseVolume;

        private List<AudioSource> audioSources;

        public void SetLoop(bool loop)
        {
            audioSource.loop = loop;
        }

        public void SetAudio(Audio audio)
        {
            if (audio is null) return;
            if (audioSourcePool && audioSource.isPlaying)
            {
                audioSourcePool.Push(0, audioSource.transform);
                audioSource = audioSourcePool.Pop(0).AudioSource;
            }

            currentAudio = audio;
            audioSource.clip = audio.clip;
            Volume = audio.multiplier;
        }

        public void Play()
        {
            audioSource.Play();
        }

        public void Pause()
        {
            audioSource.Pause();
        }

        public void Stop()
        {
            audioSource.Stop();
        }

        public float BaseVolume
        {
            get => baseVolume;
            set
            {
                baseVolume = value;
                Volume = currentAudio?.multiplier ?? 1;
            }
        }

        public float Volume
        {
            get => audioSource.volume;
            set => audioSource.volume = baseVolume * value;
        }

        public bool IsMute => audioSource.volume < 0.01 || audioSource.mute;

    }

    public class AudioManager : Singleton<AudioManager>, IAudioService
    {
        [SerializeField] private AudioData audioData;
        [SerializeField] ObjectContainer audioSourcePool;

        [SerializeField] private AudioSourceData bgm;
        [SerializeField] private AudioSourceData sfx;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            audioSourcePool.OnInit();
            sfx.audioSourcePool = audioSourcePool;

            bgm.BaseVolume = false ? 0 : 1;
            sfx.BaseVolume = false ? 0 : 1;

            bgm.BaseVolume = Locator.Data.GetData<GameData>().setting.isBgmMute ? 0 : 1;
            sfx.BaseVolume = Locator.Data.GetData<GameData>().setting.isSfxMute ? 0 : 1;

            Locator.Audio = this;
        }

        public float BgmVolume => bgm.BaseVolume;
        public float SfxVolume => sfx.BaseVolume;

        // <summary>
        // Get a background music from AudioData
        // </summary>
        // <param name="type">Type of BGM</param>
        // <returns>Audio</returns>
        private Audio GetBgmAudio(BGM_TYPE type)
        {
            return GetAudio(audioData.BgmAudioDict, type);
        }

        // <summary>
        // Get a sound effect from AudioData
        // </summary>
        // <param name="type">Type of SFX</param>
        // <returns>Audio</returns>
        private Audio GetSfxAudio(SFX_TYPE type)
        {
            return GetAudio(audioData.SfxAudioDict, type);
        }

        // <summary>
        // Get an audio from a certain dictionary
        // </summary>
        // <param name="audioDictionary">Dictionary of audio</param>
        // <param name="type">Type of audio</param>
        // <returns>Audio</returns>
        private static Audio GetAudio<T>(IReadOnlyDictionary<T, Audio> audioDictionary, T type)
        {
            return audioDictionary.GetValueOrDefault(type);
        }

        // <summary>
        // Play a background music
        // </summary>
        // <param name="type">Type of BGM</param>
        // <param name="fadeFloat">Fade out time</param>
        public void PlayBgm(BGM_TYPE type, float fadeOut = 0.3f)
        {
            Audio audioIn = GetBgmAudio(type);
            if (audioIn is null) return;
            if (audioIn == bgm.currentAudio) return;
            bgm.SetLoop(true);
            if (fadeOut == 0f || bgm.IsMute)
            {
                bgm.SetAudio(audioIn);
                bgm.Play();
            }
            else
            {
                DOVirtual.Float(bgm.currentAudio.multiplier, 0, fadeOut, value => bgm.Volume = value)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        bgm.SetAudio(audioIn);
                        bgm.Play();
                    });
            }
        }

        // <summary>
        // Play a sound effect
        // </summary>
        // <param name="type">Type of SFX</param>
        public void PlaySfx(SFX_TYPE type)
        {
            Audio audioIn = GetSfxAudio(type);
            if (audioIn is null) return;
            sfx.SetAudio(audioIn);
            sfx.Play();
        }


        // <summary>
        // Play a random sound effect from a list
        // </summary>
        // <param name="sfxTypes">List of SFX</param>
        public void PlayRandomSfx(List<SFX_TYPE> sfxTypes)
        {
            PlaySfx(sfxTypes[Random.Range(0, sfxTypes.Count)]);
        }

        public void PauseBgm()
        {
            bgm.Pause();
        }

        public void UnPauseBgm()
        {
            bgm.Play();
        }

        public void StopBgm()
        {
            bgm.Stop();
        }

        public void StopSfx(SFX_TYPE type = SFX_TYPE.NONE)
        {
            if (type == SFX_TYPE.NONE)
            {
                sfx.Stop();
                return;
            }
            if (sfx.currentAudio != GetSfxAudio(type)) return;
            sfx.Stop();
        }

        public bool IsBgmMute()
        {
            return bgm.IsMute;
        }

        public bool IsSfxMute()
        {
            return sfx.IsMute;
        }

        public void ToggleBgmVolume(bool isMute)
        {
            bgm.BaseVolume = isMute ? 0 : 1;
            //DataManager.Ins.GameData.setting.isBgmMute = isMute;
        }

        public void ToggleSfxVolume(bool isMute)
        {
            sfx.BaseVolume = isMute ? 0 : 1;
            //DataManager.Ins.GameData.setting.isSfxMute = isMute;
        }
    }
}