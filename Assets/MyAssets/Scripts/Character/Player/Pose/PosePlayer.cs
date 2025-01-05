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

        // 内部で監視する値
        [SerializeField] 
        private bool value; 
        // UnityEventを使用
        public UnityEvent OnValueChangedToTrue; 
        // Actionを使用
        public System.Action OnValueChangedToTrueAction; 

        public bool Value
        {
            get => value;
            set
            {
                if (!this.value && value) // false から true に変化した時
                {
                    OnValueChangedToTrue?.Invoke(); // UnityEventを呼び出し
                    OnValueChangedToTrueAction?.Invoke(); // Actionを呼び出し
                }
                this.value = value; // 値を更新
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
