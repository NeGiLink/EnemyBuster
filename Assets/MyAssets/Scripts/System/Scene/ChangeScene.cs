using UnityEngine;

namespace MyAssets
{
    public class ChangeScene : MonoBehaviour
    {
        //‚±‚±‚Éİ’è‚µ‚½ƒV[ƒ“‚ğSceneChanger‚Éİ’è
        [SerializeField]
        private SceneList nextScene;

        public void SetNextScene()
        {
            SceneChanger.Instance.SetNextScene(nextScene);
        }
    }
}
