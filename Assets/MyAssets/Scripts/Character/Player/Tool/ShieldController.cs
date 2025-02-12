using UnityEngine;

namespace MyAssets
{
    public enum ShieldSETag
    {
        Guard
    }
    /*
     *盾の制御コントローラー 
     */
    public class ShieldController : MonoBehaviour
    {
        [SerializeField]
        private Animator            animator;
        public Animator             Animator => animator;

        private ShieldEffectHandler effectHandler;

        private SEHandler           seHandler;

        private readonly string     stateName = "State";

        [Header("ガードのしきい値 (1 に近いほど厳しい)")]
        [Range(0.0f, 1.0f)]
        public float                guardThreshold = 0.6f;

        [Header("ガード方向 (ローカル座標)")]
        public Vector3              guardDirection = Vector3.forward;

        private bool                guard = false;

        private IGuardTrigger       guardTrigger;

        private ICharacterSetup     characterSetup;
        public void SetICharacterSetup(ICharacterSetup c) { characterSetup = c; }

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            effectHandler = GetComponent<ShieldEffectHandler>();
            seHandler = GetComponent<SEHandler>();
        }

        public void Setup(ICharacterSetup actor)
        {
            characterSetup = actor;
            guardTrigger = characterSetup.GuardTrigger;
        }

        public void ShieldOpen()
        {
            animator.SetInteger(stateName, 0);
            guard = true;
        }

        public void ShieldClose()
        {
            animator.SetInteger(stateName, -1);
            guard = false;
        }

        public bool IsGuarid(Transform thistransform,Transform target)
        {
            if (!guard) { return false; }
            Vector3 ev = (target.position - thistransform.position).normalized;

            Vector3 worldGuardDirection = thistransform.TransformDirection(guardDirection).normalized;

            // 内積を計算
            float dotProduct = Vector3.Dot(worldGuardDirection, ev);

            // ガード成功判定
            if (dotProduct > guardThreshold)
            {
                ICharacterSetup characterSetup = GetCharacterSetup(target);
                if (characterSetup != null)
                {
                    IDamageContainer damageContainer = characterSetup.DamageContainer;
                    if (damageContainer != null) 
                    {
                        guardTrigger.SetGuardFlag(true);
                        seHandler.Play((int)ShieldSETag.Guard);
                        effectHandler.EffectLedger.SetPosAndRotCreate((int)ShieldEffectType.Hit, transform.position, transform.rotation);
                        damageContainer.Recoil( DamageType.Middle, thistransform);
                    }
                }
                return true;
            }
            return false;
        }

        private ICharacterSetup GetCharacterSetup(Transform transform)
        {
            // transform が null の場合は終了
            if (transform == null)
                return null;

            ICharacterSetup characterSetup = transform.GetComponent<ICharacterSetup>();
            if (characterSetup == null)
            {
                // 親を再帰的に探索
                return GetCharacterSetup(transform.parent);
            }
            return characterSetup;
        } 
    }
}
