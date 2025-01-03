using UnityEngine;

namespace MyAssets
{
    public class PosePlayer : MonoBehaviour
    {

        private Animator animator;
        [SerializeField]
        private bool battleIdle;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (battleIdle)
            {
                SetBattleIdlePose();
            }
        }

        private void SetBattleIdlePose()
        {
            animator.SetFloat("AlertLevel", 1);
        }
    }
}
