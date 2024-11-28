using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

namespace MyAssets
{
    public class SubScene : MonoBehaviour
    {
        [SerializeField]
        private AssetReference reference;

        //�I�u�W�F�N�g�̃��[�h�n���h�����i�[����
        private AsyncOperationHandle handle;

        bool load = false;

        [SerializeField]
        private float objectRectWidth = 100f; // �I�u�W�F�N�g�̎l�p�`�̕�

        public void LoadAsset()
        {
            if (handle.IsValid()&& load) { return; }
            handle = reference.InstantiateAsync();
            load = true;
        }

        public void UnloadAsset()
        {
            if (!handle.IsValid())
            {
                return;
            }

            Addressables.ReleaseInstance(handle);
            load = false;
        }

        /// <summary>
        /// �I�u�W�F�N�g�̎l�p�`���擾
        /// </summary>
        /// <returns>�I�u�W�F�N�g�̎l�p�`�i���S�ƃT�C�Y�j</returns>
        public Rect GetObjectRectangle(SubScene targetSubScene)
        {
            // �I�u�W�F�N�g�̃��[�J�����W�n�ł̒��S
            Vector3 localCenter = targetSubScene.transform.position;

            Rect rect = new Rect(localCenter.x - objectRectWidth / 2,
                localCenter.z - objectRectWidth / 2,
                objectRectWidth,
                objectRectWidth
            );
            return rect;
        }
    }
}
