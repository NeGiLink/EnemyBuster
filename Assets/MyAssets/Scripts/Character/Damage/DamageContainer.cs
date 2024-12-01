using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class DamageContainer : IDamageContainer,ICharacterComponent<ICharacterSetup>
    {
        [SerializeField]
        private int data = 0;
        public void SetData(int d) { data = d; }
        public int Data => data;

        private AttackType attackType = AttackType.None;
        public void SetAttackType(AttackType type) { attackType = type; }
        public AttackType AttackType => attackType;

        private Transform attacker;
        public void SetAttacker(Transform t) { attacker = t; }
        public Transform Attacker => attacker;

        public void DoSetup(ICharacterSetup chara){}

        public void SetAttackerData(int power, AttackType type, Transform transform)
        {
            data = power;
            attackType = type;
            attacker = transform;
        }
    }
}
