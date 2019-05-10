using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyService
{
    public enum AudioEnum
    {
        BGM,
        Battle,
        Ather,
        FootStep
    }
    public class AudioService : Singleton<AudioService>
    {
        private AudioSource m_audioSource;
        private AudioClip m_AudioClip;

        public AudioSource AudioSource { get => m_audioSource; set => m_audioSource = value; }
        public AudioClip AudioClip { get => m_AudioClip; set => m_AudioClip = value; }

        public void PlayWithAS(AudioSource audioSource, AudioEnum audioEnum, string name)
        {
            AudioSource = audioSource;
            AudioClip = ResourceService.Instance.LoadAsset<AudioClip>(GameConfigService.Instance.MusicPath + audioEnum .ToString()+"/"+ name);
            if (AudioClip == null)
            {
                Debug.LogError("要播放声音为空");
                return;
            }
            AudioSource.clip = AudioClip;
            if(audioEnum == AudioEnum.BGM)
            {
                AudioSource.volume = GameConfigService.Instance.MusicVolume;
                AudioSource.loop = true;
                AudioSource.Play();
            }
            else
            {
                AudioSource.Play();
            }
        }
        /// <summary>
        /// 以物品播放声音
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="audioEnum"></param>
        /// <param name="str"></param>
        public void Play(GameObject gameObject, AudioEnum audioEnum, string str)
        {
            AudioSource = gameObject.GetComponent<AudioSource>();
            if(AudioSource == null)
            {
                Debug.Log("此物体没有AS，无法播放声音");
                return;
            }
            AudioClip = ResourceService.Instance.LoadAsset<AudioClip>(GameConfigService.Instance.MusicPath + audioEnum.ToString() + "/" + str);
            if (AudioClip == null)
            {
                Debug.LogError("要播放声音为空");
                return;
            }
            AudioSource.clip = AudioClip;
            if (audioEnum == AudioEnum.BGM)
            {
                AudioSource.volume = GameConfigService.Instance.MusicVolume;
                AudioSource.loop = true;
                AudioSource.Play();
            }
            else
            {
                AudioSource.Play();
            }
        }


    }
}
