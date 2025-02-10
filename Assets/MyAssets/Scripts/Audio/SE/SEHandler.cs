using UnityEngine;

namespace MyAssets
{
    /*
     * SE���Đ�����N���X
     * �I�u�W�F�N�g�̐e�ɃA�^�b�`����`�Ŏg�p����
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
        //�A���ōĐ�����悤�̊֐�
        public void Play(int num)
        {
            if(audioSource == null) { return; }
            audioSource.volume = SystemManager.SEVolume;
            audioSource.PlayOneShot(seLedger[num]);
        }
        //��x�Đ����I���܂ōēx�Đ��ł��Ȃ��悤�̊֐�
        public void OnPlay(int num)
        {
            if (audioSource.isPlaying) { return; }
            audioSource.volume = SystemManager.SEVolume;
            audioSource.PlayOneShot(seLedger[num]);
        }
        //�������Đ�����֐�
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
        //�����Ȃ��L�����N�^�[(�X���C���Ȃ�)�̏ꍇ�̑����Đ��֐�
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
