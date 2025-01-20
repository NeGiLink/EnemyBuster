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

        [SerializeField]
        private LayerMask targetLayer;

        public Vector3 SpawnPositionOutput()
        {
            Vector3 pos = transform.position;
            Ray ray = new Ray(transform.position,-transform.up);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(ray, out hit, 5, targetLayer);
            pos.y = hit.point.y;
            return pos;
        }
    }
}
