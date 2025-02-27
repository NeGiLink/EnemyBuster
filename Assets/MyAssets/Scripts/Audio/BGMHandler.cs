using UnityEngine;

namespace MyAssets
{
    public class BGMHandler : MonoBehaviour
    {
        private static BGMHandler   instance;
        public static BGMHandler    Instance => instance;

        [SerializeField]
        [Range(0f, 1f)]
        private float               volume = 1f;

        [SerializeField]
        private AudioClip[]         audioClips;

        [SerializeField]
        private BGMPlayer           bgmPlayer;

        private BGMPlayer           keepBGMPlayer;

        [SerializeField]
        private bool                noAwakePlay;

        [SerializeField]
        private bool                randomPlay;

        [SerializeField]
        private bool                loop;
        [SerializeField]
        private bool                waitPlay;
        [SerializeField]
        private float               waitCount;

        public void SetVolum(float v) {  volume = v; }

        public void SetAudioVolume(float v) { keepBGMPlayer.SetAudioVolume(v); }
        public void SetLoop(bool b) { loop = b; }
        public void SetWaitPlay(bool w) { waitPlay = w; }
        public void SetWaitCount(float w) { waitCount = w; }
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (!noAwakePlay)
            {
                SetPlayer(loop,waitPlay,waitCount,randomPlay);
            }
        }

        public void SetPlayer(bool loop,bool wait,float waitCount,bool random)
        {
            if(keepBGMPlayer == null)
            {
                keepBGMPlayer = Instantiate(bgmPlayer);
            }
            keepBGMPlayer.SetVolum(SystemManager.BGMVolume);
            if (random)
            {
                keepBGMPlayer.SetAudioClip(audioClips[Random.Range(0, audioClips.Length)]);
            }
            else
            {
                keepBGMPlayer.SetAudioClip(audioClips[0]);
            }
            if (loop)
            {
                keepBGMPlayer.SetLoop(loop);
            }
            if (wait)
            {
                keepBGMPlayer.SetWaitPlay(wait);
                keepBGMPlayer.SetWaitCount(waitCount);
            }
            
        }
        public void ChangeBGM(AudioClip clip)
        {
            keepBGMPlayer.Play(clip);
        }
    }
}
