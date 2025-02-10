using UnityEngine;

namespace MyAssets
{
    /*
     * キャラクターが死亡した時にアタッチするクラス
     * オブジェクトを消去するクラス
     */
    public class DestroyCommand : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject);
        }
    }
}
