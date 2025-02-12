using UnityEngine;

namespace MyAssets
{
    /*
     * 剣や盾の装備処理クラスのインターフェース
     */
    public interface IEquipment
    {
        ShieldController    ShieldTool {  get; }

        GameObject          HaveWeapon {  get; }
        void SetInWeapon();
        void SetOutWeapon();
    }
}
