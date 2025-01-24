using UnityEngine;

namespace MyAssets
{
    [RequireComponent(typeof(AudioSource))]
    public class SEHandler : MonoBehaviour
    {
        [SerializeField]
        private SELedger seLedger;

        [SerializeField]
        private SELedger footStepSELedger;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play(int num)
        {
            audioSource.PlayOneShot(seLedger[num]);
        }

        public void OnPlay(int num)
        {
            if (audioSource.isPlaying) { return; }
            audioSource.PlayOneShot(seLedger[num]);
        }

        public void PlayFootstepSound()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
            {
                FootstepSurface surface = hit.collider.GetComponent<FootstepSurface>();
                if (surface != null)
                {
                    audioSource.PlayOneShot(footStepSELedger[(int)surface.SurfaceType]);
                }
            }
        }
    }
}
