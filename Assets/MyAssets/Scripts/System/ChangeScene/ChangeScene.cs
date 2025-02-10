using UnityEngine;

namespace MyAssets
{
    /*
     * シーン遷移するボタンにアタッチして
     * onClickで使用してるクラス
     */
    public class ChangeScene : MonoBehaviour
    {
        //ここに設定したシーンをSceneChangerに設定
        [SerializeField]
        private SceneList nextScene;

        public void SetNextScene()
        {
            SceneChanger.Instance.SetNextScene(nextScene);
        }
    }
}
