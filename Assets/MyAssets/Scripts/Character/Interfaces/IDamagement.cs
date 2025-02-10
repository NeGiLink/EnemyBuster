using UnityEngine;

namespace MyAssets
{
    /*
     * ダメージを受けた時のノックバック処理をまとめたインターフェース
     */
    public interface IDamagement
    {
        void AddForceMove(Vector3 origin, Vector3 target, float power);
    }
}
