

using UnityEngine;

namespace MyAssets
{
    public enum AttackType
    {
        Null,
        Normal,
        Charge,
        Succession
    }
    [RequireComponent(typeof(SEHandler))]
    [RequireComponent(typeof(AttackObject))]
    public class BaseAttackController : MonoBehaviour
    {
        //�U���I�u�W�F�N�g�̃p�����[�^�[���܂Ƃ߂��N���X
        [SerializeField]
        protected AttackObject  attackObject;

        protected AttackType    attackType = AttackType.Normal;

        protected SEHandler     seHandler;

        protected virtual void Awake()
        {
            attackObject = GetComponent<AttackObject>();

            seHandler = GetComponent<SEHandler>();
        }
    }
}
