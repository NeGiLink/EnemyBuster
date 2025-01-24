using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class BGMHandler : MonoBehaviour
    {
        private static BGMHandler instance;
        public static BGMHandler Instance => instance;

        [SerializeField]
        [Range(0f, 1f)]
        private float volum = 1f;
        public void SetVolum(float v) {  volum = v; }

        [SerializeField]
        private AudioClip[] audioClips;

        [SerializeField]
        private BGMPlayer bgmPlayer;

        [SerializeField]
        private bool noAwakePlay;

        [SerializeField]
        private bool randomPlay;

        [SerializeField]
        private bool loop;
        [SerializeField]
        private bool waitPlay;
        [SerializeField]
        private float waitCount;

        public void SetLoop(bool b) { loop = b; }
        public void SetWaitPlay(bool w) { waitPlay = w; }
        public void SetWaitCount(float w) { waitCount = w; }
        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            if (!noAwakePlay)
            {
                SetPlayer(loop,waitPlay,waitCount,randomPlay);
            }
        }

        public void SetPlayer(bool loop,bool wait,float waitCount,bool random)
        {
            BGMPlayer bgm = Instantiate(bgmPlayer);
            bgm.SetVolum(volum);
            if (random)
            {
                bgm.SetAudioClip(audioClips[Random.Range(0, audioClips.Length)]);
            }
            else
            {
                bgm.SetAudioClip(audioClips[0]);
            }
            if (loop)
            {
                bgm.SetLoop(loop);
            }
            if (wait)
            {
                bgm.SetWaitPlay(wait);
                bgm.SetWaitCount(waitCount);
            }
            
        }
    }
}
