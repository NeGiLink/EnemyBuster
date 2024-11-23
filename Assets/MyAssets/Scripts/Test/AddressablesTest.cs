using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MyAssets
{
    public class AddressablesTest : MonoBehaviour
    {
        [SerializeField]
        AssetReference reference;

        [SerializeField]
        private List<AssetReference> references;

        // �e�I�u�W�F�N�g�̃��[�h�n���h�����i�[���郊�X�g
        private List<AsyncOperationHandle<GameObject>> handles = new List<AsyncOperationHandle<GameObject>>();

        void Start()
        {
            LoadAssets();
        }

        void Update()
        {
            // "Delete"�L�[�������ꂽ��S�ẴA�Z�b�g���A�����[�h
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                UnloadAssets();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                LoadAssets();
            }
        }

        // �����̃A�Z�b�g�����[�h
        void LoadAssets()
        {
            for (int i = 0; i < references.Count; i++)
            {
                // �e�A�Z�b�g�̃C���X�^���X�����J�n���A�n���h�������X�g�ɒǉ�
                var handle = references[i].InstantiateAsync();
                handles.Add(handle);
            }
        }

        // �����̃A�Z�b�g���A�����[�h
        void UnloadAssets()
        {
            // ���[�h���ꂽ���ׂẴn���h�������
            foreach (var handle in handles)
            {
                if (handle.IsValid())
                {
                    Addressables.ReleaseInstance(handle);
                }
            }

            // �n���h�����X�g���N���A
            handles.Clear();
        }
    }
}
