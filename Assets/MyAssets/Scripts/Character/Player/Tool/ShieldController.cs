using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace MyAssets
{
    public class ShieldController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        public Animator Animator => animator;

        private readonly string stateName = "State";

        [Header("ガードのしきい値 (1 に近いほど厳しい)")]
        [Range(0.0f, 1.0f)]
        public float guardThreshold = 0.6f;

        [Header("ガード方向 (ローカル座標)")]
        public Vector3 guardDirection = Vector3.forward;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void ShieldOpen()
        {
            animator.SetInteger(stateName, 0);
        }

        public void ShieldClose()
        {
            animator.SetInteger(stateName, -1);
        }

        public bool IsGuarid(Transform target,Transform transform)
        {
            Vector3 ev = target.position - transform.position;
            ev.Normalize();
            Vector3 worldGuardDirection = transform.transform.TransformDirection(guardDirection).normalized;

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
                        damageContainer.Recoil( AttackType.Middle, transform);
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
