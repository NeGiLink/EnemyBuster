using UnityEngine;

namespace MyAssets
{
    /*
     * �V�[���J�ڂ���{�^���ɃA�^�b�`����
     * onClick�Ŏg�p���Ă�N���X
     */
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
