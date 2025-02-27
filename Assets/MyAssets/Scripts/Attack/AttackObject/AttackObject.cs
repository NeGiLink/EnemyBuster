using UnityEngine;

namespace MyAssets
{
    public enum DamageType
    {
        None = -1,
        Small,
        Middle,
        Big
    }
    /*
     * 攻撃オブジェクトのパラメーターをまとめたクラス
     * パワー、ノックバック、ダメージのタイプをまとめたもの
     */
    public class AttackObject : MonoBehaviour
    {
        //スクリプタブルオブジェクト
        [SerializeField]
        private AttackData          data;
        //スクリプタブルオブジェクトの中身が複数ならその配列の要素数に使う
        [SerializeField]
        private int                 attackTypeCount = 0;
        //パワー
        public int Power =>         data.AttackDataInfo[attackTypeCount].power;
        //ノックバック
        public float KnockBack =>   data.AttackDataInfo[attackTypeCount].knockBack;
        //ノックバック時にノックバックレベル
        public DamageType Type =>   data.AttackDataInfo[attackTypeCount].attackType;
        //要素数変更関数
        public void SetAttackTypeCount(int c) { attackTypeCount = c; }
    }
}
