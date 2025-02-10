

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
        //攻撃オブジェクトのパラメーターをまとめたクラス
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
