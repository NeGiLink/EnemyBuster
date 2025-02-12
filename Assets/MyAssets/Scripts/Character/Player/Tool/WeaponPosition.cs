using UnityEngine;

namespace MyAssets
{
    public enum WeaponPositionTag
    {
        None = -1,
        Hand,
        Receipt,
        Count
    }
    //武器の移動したいオブジェクトにアタッチして使う武器のポジションクラス
    public class WeaponPosition : MonoBehaviour
    {
        //タグで判別
        [SerializeField]
        private new WeaponPositionTag   tag = WeaponPositionTag.None;
        public WeaponPositionTag        Tag => tag;

        public GameObject               ThisObject => gameObject;
    }
}
