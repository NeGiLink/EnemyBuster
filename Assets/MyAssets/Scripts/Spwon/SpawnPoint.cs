using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// キャラクターを生成する位置に置いてるスポーンオブジェクトにアタッチするもの
    /// 現在は特に何もなし
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        private bool use = false;
        public bool IsUse => use;
        public void SetUseFlag(bool b) { use = b; }
    }
}
