using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    using UnityEngine;
    using System.Collections.Generic;

    public class OctreeManager : MonoBehaviour
    {
        private Octree octree; // 八分木のインスタンス

        [SerializeField] private Vector3 octreeCenter = Vector3.zero; // 八分木の中心
        [SerializeField] private Vector3 octreeSize = new Vector3(100, 100, 100); // 八分木のサイズ
        [SerializeField] private float minNodeSize = 1.0f; // ノードの最小サイズ

        void Start()
        {
            // 八分木を初期化
            octree = new Octree(octreeCenter, octreeSize, minNodeSize);

            // シーン内のオブジェクトを八分木に追加
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Trackable");
            foreach (var obj in objects)
            {
                octree.Insert(obj);
            }

            Debug.Log("Octree Initialized");
        }

        void Update()
        {
            // 探索範囲
            Bounds searchBounds = new Bounds(transform.position, new Vector3(10, 10, 10));

            // 範囲内のオブジェクトを取得
            List<GameObject> foundObjects = octree.Query(searchBounds);

            Debug.Log($"Found {foundObjects.Count} objects in search bounds.");
        }
    }

}