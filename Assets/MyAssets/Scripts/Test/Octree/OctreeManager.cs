using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    using UnityEngine;
    using System.Collections.Generic;

    public class OctreeManager : MonoBehaviour
    {
        private Octree octree; // �����؂̃C���X�^���X

        [SerializeField] private Vector3 octreeCenter = Vector3.zero; // �����؂̒��S
        [SerializeField] private Vector3 octreeSize = new Vector3(100, 100, 100); // �����؂̃T�C�Y
        [SerializeField] private float minNodeSize = 1.0f; // �m�[�h�̍ŏ��T�C�Y

        void Start()
        {
            // �����؂�������
            octree = new Octree(octreeCenter, octreeSize, minNodeSize);

            // �V�[�����̃I�u�W�F�N�g�𔪕��؂ɒǉ�
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Trackable");
            foreach (var obj in objects)
            {
                octree.Insert(obj);
            }

            Debug.Log("Octree Initialized");
        }

        void Update()
        {
            // �T���͈�
            Bounds searchBounds = new Bounds(transform.position, new Vector3(10, 10, 10));

            // �͈͓��̃I�u�W�F�N�g���擾
            List<GameObject> foundObjects = octree.Query(searchBounds);

            Debug.Log($"Found {foundObjects.Count} objects in search bounds.");
        }
    }

}