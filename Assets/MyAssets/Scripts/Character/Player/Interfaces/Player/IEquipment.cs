using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IEquipment
    {
        ShieldTool ShieldTool {  get; }

        //bool IsHaveWeapon {  get; }
        void SetInWeapon();
        void SetOutWeapon();
    }
}
