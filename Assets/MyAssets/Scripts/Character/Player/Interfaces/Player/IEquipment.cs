using UnityEngine;

namespace MyAssets
{
    public interface IEquipment
    {
        ShieldController ShieldTool {  get; }

        GameObject HaveWeapon {  get; }
        void SetInWeapon();
        void SetOutWeapon();
    }
}
