using UnityEngine;

namespace MyAssets
{
    /*
     * SEを再生するクラス
     * オブジェクトの親にアタッチする形で使用する
     */
    [RequireComponent(typeof(AudioSource))]
    public class SEHandler : MonoBehaviour
    {
        [SerializeField]
        private SELedger    seLedger;

        [SerializeField]
        private SELedger    footStepSELedger;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        //連続で再生するようの関数
        public void Play(int num)
        {
            if(audioSource == null) { return; }
            audioSource.volume = SystemManager.SEVolume;
            audioSource.PlayOneShot(seLedger[num]);
        }
        //一度再生が終わるまで再度再生できないようの関数
        public void OnPlay(int num)
        {
            if (audioSource.isPlaying) { return; }
            audioSource.volume = SystemManager.SEVolume;
            audioSource.PlayOneShot(seLedger[num]);
        }
        //足音を再生する関数
        public void OneShotPlayFootstepSound()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
            {
                FootstepSurface surface = hit.collider.GetComponent<FootstepSurface>();
                if (surface != null)
                {
                    audioSource.volume = SystemManager.SEVolume;
                    audioSource.PlayOneShot(footStepSELedger[(int)surface.SurfaceType]);
                }
            }
        }
        //足がないキャラクター(スライムなど)の場合の足音再生関数
        public void PlayFootstepSound()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
            {
                FootstepSurface surface = hit.collider.GetComponent<FootstepSurface>();
                if (surface != null)
                {
                    if (audioSource.isPlaying) { return; }
                    audioSource.volume = SystemManager.SEVolume;
                    audioSource.PlayOneShot(footStepSELedger[(int)surface.SurfaceType]);
                }
            }
        }
    }
}
