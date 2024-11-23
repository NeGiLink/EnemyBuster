using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    using UnityEngine;
    using System.Collections.Generic;

    public class OctreeNode
    {
        public Bounds Bounds; // このノードが管理する範囲
        public List<GameObject> Objects; // このノードが保持するオブジェクト
        public OctreeNode[] Children; // 子ノード（最大8個）

        public OctreeNode(Bounds bounds)
        {
            Bounds = bounds;
            Objects = new List<GameObject>();
            Children = null; // 最初は子ノードが存在しない
        }

        // このノードを8つの子ノードに分割
        public void Subdivide()
        {
            Children = new OctreeNode[8];
            Vector3 size = Bounds.size / 2f;
            Vector3 center = Bounds.center;

            // 各子ノードの中心を計算
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
        public OctreeNode RootNode; // ルートノード
        public float MinNodeSize;   // 分割を止める最小サイズ

        public Octree(Vector3 center, Vector3 size, float minNodeSize)
        {
            // 空間全体のルートノードを初期化
            RootNode = new OctreeNode(new Bounds(center, size));
            MinNodeSize = minNodeSize;
        }

        public void Insert(GameObject obj)
        {
            // オブジェクトをルートノードに追加
            InsertRecursive(RootNode, obj);
        }

        private void InsertRecursive(OctreeNode node, GameObject obj)
        {
            // オブジェクトがノードの範囲内にない場合は無視
            if (!node.Bounds.Contains(obj.transform.position))
                return;

            // 分割の必要がないか、最小サイズに達した場合
            if (node.Children == null && node.Bounds.size.magnitude <= MinNodeSize)
            {
                node.Objects.Add(obj);
                return;
            }

            // ノードを分割
            if (node.Children == null)
                node.Subdivide();

            // 子ノードに再帰的に追加
            foreach (var child in node.Children)
            {
                InsertRecursive(child, obj);
            }
        }

        public List<GameObject> Query(Bounds searchBounds)
        {
            // 範囲に含まれるオブジェクトを取得
            return QueryRecursive(RootNode, searchBounds);
        }

        private List<GameObject> QueryRecursive(OctreeNode node, Bounds searchBounds)
        {
            List<GameObject> results = new List<GameObject>();

            // ノードが探索範囲と交差しない場合はスキップ
            if (!node.Bounds.Intersects(searchBounds))
                return results;

            // このノードのオブジェクトを追加
            foreach (var obj in node.Objects)
            {
                if (searchBounds.Contains(obj.transform.position))
                    results.Add(obj);
            }

            // 子ノードを探索
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

