using UnityEngine;

namespace MyAssets
{
    /*
     * ダメージ処理を行うクラス
     * HP減少、蓄積ダメージ加算などのステータス変更
     * ダメージ発生時のダメージテキストの出力など
     */
    [System.Serializable]
    public class DamageContainer : IDamageContainer,ICharacterComponent<ICharacterSetup>
    {
        [SerializeField]
        private float       invincibilityCount = 0.1f;

        private bool        activateKnockback = false;
        public void         SetActivateKnockback(bool k) { activateKnockback = k; }

        [SerializeField]
        private int         data = 0;
        public void SetData(int d) { data = d; }
        public int          Data => data;

        private DamageType  attackType = DamageType.None;
        public void SetAttackType(DamageType type) { attackType = type; }
        public DamageType   AttackType => attackType;

        private Transform   attacker;
        public void SetAttacker(Transform t) { attacker = t; }
        public Transform    Attacker => attacker;

        private IBaseStauts baseStauts;

        private Transform   thisTransform;

        private FieldOfView fieldOfView;

        public void DoSetup(ICharacterSetup chara)
        {
            baseStauts = chara.BaseStauts;
            thisTransform = chara.gameObject.transform;
            fieldOfView = chara.gameObject.GetComponent<FieldOfView>();
        }

        public void GiveDamage(int power, DamageType type, Transform transform,CharacterType charaType)
        {
            if(baseStauts.HP <= 0) { return; }
            if (!baseStauts.InvincibilityTimer.IsEnd()) { return; }
            //ダメージを与える
            baseStauts.DecreaseAndDeathCheck(power);
            //GameManagerにダメージテキスト出力を依頼
            DamageTextOutput(power,charaType);
            fieldOfView?.AllSearchStart();
            //蓄積ダメージ量がたまったか
            if (baseStauts.IsMaxStoredDamage(power)|| activateKnockback )
            {
                data = power;
                attackType = type;
                attacker = transform;
                activateKnockback = false;
            }
            //無敵スタート
            baseStauts.InvincibilityTimer.Start(invincibilityCount);
        }

        public void DamageTextOutput(int power,CharacterType type)
        {
            if (power <= 0) { return; }
            //受け取ったタグ別にテキストの色を変更
            Color color = Color.white;
            switch (type)
            {
                case CharacterType.Null:
                    break;
                case CharacterType.Player:
                    break;
                case CharacterType.Enemy:
                    color = Color.red;
                    break;
            }
            GameUIController.Instance.DamageTextCreator.Crate(thisTransform, power,color);
        }

        public void Recoil(DamageType type, Transform t)
        {
            attackType = type;
            attacker = t;
            baseStauts.IsMaxStoredDamage(baseStauts.MaxStoredDamage);
        }
    }
}
