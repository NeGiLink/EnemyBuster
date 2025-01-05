using UnityEngine;

namespace MyAssets
{
    public class ChangeScene : MonoBehaviour
    {
        //�����ɐݒ肵���V�[����SceneChanger�ɐݒ�
        [SerializeField]
        private SceneList nextScene;

        public void SetNextScene()
        {
            SceneChanger.Instance.SetNextScene(nextScene);
        }
    }
}
