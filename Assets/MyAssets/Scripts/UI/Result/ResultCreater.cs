using UnityEngine;

namespace MyAssets
{
    /*
     * 結果UIの生成を行うクラス
     */
    public class ResultCreater : MonoBehaviour
    {
        //非表示になったら生成
        private void OnDisable()
        {
            GameUIController.Instance.CreateResultUI();
        }
    }
}
