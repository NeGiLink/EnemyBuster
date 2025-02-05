using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class BGMPlayer : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField]
        private AudioClip clip;

        [SerializeField]
        [Range(0f, 1f)]
        private float volum;
        [SerializeField]
        private bool loop;
        [SerializeField]
        private bool waitPlay;
        [SerializeField]
        private float waitCount;

        public void SetAudioClip(AudioClip c) { clip = c; }
        public void SetVolum(float v) {  volum = v; }
        public void SetAudioVolume(float v) { audioSource.volume = v; }
        public void SetLoop(bool b) { loop = b; }
        public void SetWaitPlay(bool w) {  waitPlay = w; }
        public void SetWaitCount(float w) { waitCount = w; }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            audioSource.clip = clip;
            audioSource.volume = volum;
            if (loop)
            {
                audioSource.loop = loop;
            }

            if (waitPlay)
            {
                StartCoroutine(WaitPlay());
            }
            else
            {
                audioSource.Play();
            }
        }

        private IEnumerator WaitPlay()
        {
            audioSource.Stop();
            yield return new WaitForSecondsRealtime(waitCount);
            audioSource.Play();
        }

        public void Play(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
