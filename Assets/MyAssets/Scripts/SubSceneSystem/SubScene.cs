using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MyAssets
{
    //�I�u�W�F�N�g�����[�h����I�u�W�F�N�g�ɃA�^�b�`����N���X
    //AssetReference�ɃZ�b�g�����I�u�W�F�N�g��SubSceneLoader�Ƃ̏����œǂݍ���&�������
    public class SubScene : MonoBehaviour
    {
        [SerializeField]
        private AssetReference reference;

        //�I�u�W�F�N�g�̃��[�h�n���h�����i�[����
        private AsyncOperationHandle handle;

        bool load = false;
        public bool IsLoading => load;

        // �I�u�W�F�N�g�̎l�p�`�̕�
        [SerializeField]
        private float objectRectWidth = 100f; 
        //���[�h&�A�����[�h���s���������������点��̂Ɏg���Ă���^�C�}�[
        private Timer timer = new Timer();

        private float count = 0.25f;

        public void DoUpdate()
        {
            timer.Update(Time.deltaTime);
        }

        public void LoadAsset()
        {
            if (!timer.IsEnd()) { return; }
            if (handle.IsValid()&& load) { return; }
            handle = reference.InstantiateAsync();
            load = true;
            timer.Start(count);
        }

        public void UnloadAsset()
        {
            if (!timer.IsEnd()) { return; }
            if (!handle.IsValid())
            {
                return;
            }

            Addressables.ReleaseInstance(handle);
            load = false;
            timer.Start(count);
        }

        /// <summary>
        /// �I�u�W�F�N�g�̎l�p�`���擾
        /// </summary>
        /// <returns>�I�u�W�F�N�g�̎l�p�`�i���S�ƃT�C�Y�j</returns>
        public Rect GetObjectRectangle(SubScene targetSubScene)
        {
            // �I�u�W�F�N�g�̃��[�J�����W�n�ł̒��S
            Vector3 localCenter = targetSubScene.transform.position;

            Rect rect = new Rect(
                localCenter.x - objectRectWidth / 2,
                localCenter.z - objectRectWidth / 2,
                objectRectWidth,
                objectRectWidth
            );
            return rect;
        }
    }
}
