using UnityEngine;
using UnityEngine.Events;

namespace MyAssets
{
    public class PosePlayer : MonoBehaviour
    {

        private Animator animator;

        private WeaponController weaponController;

        [SerializeField]
        private bool battleIdle;

        [SerializeField]
        private bool sitting;

        // �����ŊĎ�����l
        [SerializeField] 
        private bool value; 
        // UnityEvent���g�p
        public UnityEvent OnValueChangedToTrue; 
        // Action���g�p
        public System.Action OnValueChangedToTrueAction; 

        public bool Value
        {
            get => value;
            set
            {
                if (!this.value && value) // false ���� true �ɕω�������
                {
                    OnValueChangedToTrue?.Invoke(); // UnityEvent���Ăяo��
                    OnValueChangedToTrueAction?.Invoke(); // Action���Ăяo��
                }
                this.value = value; // �l���X�V
            }
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            weaponController = GetComponent<WeaponController>();
        }

        private void Update()
        {
            if (battleIdle)
            {
                SetBattleIdlePose();
            }
            if (sitting)
            {
                SetSittingPose();
            }
        }

        private void SetBattleIdlePose()
        {
            animator.SetFloat("AlertLevel", 1);
            weaponController.ShieldTool.ShieldClose();
            battleIdle = false;
        }

        private void SetSittingPose()
        {
            animator.SetInteger("Pose", 0);
            weaponController.ShieldTool.ShieldClose();
            weaponController.SetInWeapon();
            sitting = false;
        }
    }
}
