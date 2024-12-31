using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{

    public class SwordController : MonoBehaviour
    {
        [SerializeField]
        private AttackObject attackObject;

        [SerializeField]
        private IPlayerAnimator animator;

        [SerializeField]
        private LayerMask hitLayer;

        [SerializeField]
        private float radius = 0.5f;

        [SerializeField]
        private float dis = 1.0f;

        [SerializeField]
        private new Collider collider;

        private List<IDamageContainer> damagers = new List<IDamageContainer>();

        private AttackType attackType = AttackType.Single;
        public void SetAttackType(AttackType type) {  attackType = type; }

        private void Awake()
        {
            attackObject = GetComponent<AttackObject>();

            PlayerController controller = GetComponentInParent<PlayerController>();

            if(controller != null)
            {
                animator = controller.PlayerAnimator;
            }

            collider = GetComponent<Collider>();
        }

        private void Start()
        {
            collider.enabled = false;
        }

        public void EnabledCollider(float start,float end,bool all)
        {
            if (all)
            {
                collider.enabled = true;
                /*
                Ray ray = new Ray(transform.position, transform.up);
                RaycastHit hit;
                if (Physics.SphereCast(ray, radius, out hit, dis, hitLayer))
                {
                    ICharacterSetup characterSetup = hit.collider.GetComponent<ICharacterSetup>();
                    if (characterSetup == null) { return; }
                    IDamageContainer damageContainer = characterSetup.DamageContainer;
                    if (damageContainer == null) { return; }
                    damageContainer.SetAttackerData(attackObject.Power, attackObject.Type, transform);
                }
                 */
            }
            else
            {
                AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
                if(animInfo.normalizedTime >= start &&animInfo.normalizedTime <= end)
                {
                    collider.enabled = true;
                    /*
                    Ray ray = new Ray(transform.position, transform.up);
                    RaycastHit hit;
                    if (Physics.SphereCast(ray, radius, out hit, dis, hitLayer))
                    {
                        ICharacterSetup characterSetup = hit.collider.GetComponent<ICharacterSetup>();
                        if (characterSetup == null) { return; }
                        IDamageContainer damageContainer = characterSetup.DamageContainer;
                        if (damageContainer == null) { return; }
                        damageContainer.SetAttackerData(attackObject.Power, attackObject.Type, transform);
                    }
                     */
                }
                else
                {
                    collider.enabled = false;
                }
            }
        }
        public void NotEnabledCollider()
        {
            collider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(attackType == AttackType.Succession) { return; }
            if(other.gameObject.layer != 8) { return; }
            ICharacterSetup characterSetup = other.GetComponent<ICharacterSetup>();
            if (characterSetup == null) { return; }
            IDamageContainer damageContainer = characterSetup.DamageContainer;
            if (damageContainer == null) { return; }
            //すでに当たった相手かチェック
            for (int i = 0; i < damagers.Count; i++)
            {
                if (damagers[i] == damageContainer) { return; }
            }

            damageContainer.GiveYouDamage(attackObject.Power, attackObject.Type, transform);
            damagers.Add(damageContainer);
        }

        private void OnTriggerStay(Collider other)
        {
            if (attackType == AttackType.Single) { return; }
            if (other.gameObject.layer != 8) { return; }
            ICharacterSetup characterSetup = other.GetComponent<ICharacterSetup>();
            if (characterSetup == null) { return; }
            IDamageContainer damageContainer = characterSetup.DamageContainer;
            if (damageContainer == null) { return; }
            damageContainer.GiveYouDamage(attackObject.Power, attackObject.Type, transform);
            damagers.Add(damageContainer);
        }

        public void DamagerReset()
        {
            damagers.Clear();
        }

    }
}
