using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    using UnityEngine;
    using System.Collections.Generic;

    public class OctreeNode
    {
        public Bounds Bounds; // ���̃m�[�h���Ǘ�����͈�
        public List<GameObject> Objects; // ���̃m�[�h���ێ�����I�u�W�F�N�g
        public OctreeNode[] Children; // �q�m�[�h�i�ő�8�j

        public OctreeNode(Bounds bounds)
        {
            Bounds = bounds;
            Objects = new List<GameObject>();
            Children = null; // �ŏ��͎q�m�[�h�����݂��Ȃ�
        }

        // ���̃m�[�h��8�̎q�m�[�h�ɕ���
        public void Subdivide()
        {
            Children = new OctreeNode[8];
            Vector3 size = Bounds.size / 2f;
            Vector3 center = Bounds.center;

            // �e�q�m�[�h�̒��S���v�Z
            for (int i = 0; i < 8; i++)
            {
                Vector3 newCenter = center + new Vector3(
                    ((i & 1) == 0 ? -1 : 1) * size.x / 2f,
                    ((i & 2) == 0 ? -1 : 1) * size.y / 2f,
                    ((i & 4) == 0 ? -1 : 1) * size.z / 2f
                );
                Children[i] = new OctreeNode(new Bounds(newCenter, size));
            }
        }
    }


    public class Octree
    {
        public OctreeNode RootNode; // ���[�g�m�[�h
        public float MinNodeSize;   // �������~�߂�ŏ��T�C�Y

        public Octree(Vector3 center, Vector3 size, float minNodeSize)
        {
            // ��ԑS�̂̃��[�g�m�[�h��������
            RootNode = new OctreeNode(new Bounds(center, size));
            MinNodeSize = minNodeSize;
        }

        public void Insert(GameObject obj)
        {
            // �I�u�W�F�N�g�����[�g�m�[�h�ɒǉ�
            InsertRecursive(RootNode, obj);
        }

        private void InsertRecursive(OctreeNode node, GameObject obj)
        {
            // �I�u�W�F�N�g���m�[�h�͈͓̔��ɂȂ��ꍇ�͖���
            if (!node.Bounds.Contains(obj.transform.position))
                return;

            // �����̕K�v���Ȃ����A�ŏ��T�C�Y�ɒB�����ꍇ
            if (node.Children == null && node.Bounds.size.magnitude <= MinNodeSize)
            {
                node.Objects.Add(obj);
                return;
            }

            // �m�[�h�𕪊�
            if (node.Children == null)
                node.Subdivide();

            // �q�m�[�h�ɍċA�I�ɒǉ�
            foreach (var child in node.Children)
            {
                InsertRecursive(child, obj);
            }
        }

        public List<GameObject> Query(Bounds searchBounds)
        {
            // �͈͂Ɋ܂܂��I�u�W�F�N�g���擾
            return QueryRecursive(RootNode, searchBounds);
        }

        private List<GameObject> QueryRecursive(OctreeNode node, Bounds searchBounds)
        {
            List<GameObject> results = new List<GameObject>();

            // �m�[�h���T���͈͂ƌ������Ȃ��ꍇ�̓X�L�b�v
            if (!node.Bounds.Intersects(searchBounds))
                return results;

            // ���̃m�[�h�̃I�u�W�F�N�g��ǉ�
            foreach (var obj in node.Objects)
            {
                if (searchBounds.Contains(obj.transform.position))
                    results.Add(obj);
            }

            // �q�m�[�h��T��
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    results.AddRange(QueryRecursive(child, searchBounds));
                }
            }

            return results;
        }
    }
}

