using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IChangingState
    {
        bool IsBattleMode {  get; }

        void SetBattleMode(bool b);
    }
}
