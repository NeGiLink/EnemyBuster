using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class FieldOfView : MonoBehaviour , IFieldOfView
    {
        private Timer       currentSearchinTimer = new Timer();
        public Timer        CurrentSearchinTimer => currentSearchinTimer;
        [SerializeField]
        private GameObject  targetObject;
        public GameObject   TargetObject => targetObject;
        //nullじゃないのならtrue、nullならfalse
        public bool         FindTarget => targetObject != null;

        public void SetTargetObject(GameObject t) {  targetObject = t; }
        [SerializeField]
        private Vector3     targetLastPoint;
        public Vector3      TargetLastPoint => targetLastPoint;


        [SerializeField]
        float               refreshTime = 0.1f;

        [SerializeField]
        float               range = 10.0f;

        [SerializeField]
        float               viewAngle = 45.0f;

        [SerializeField]
        LayerMask           targetObjectLayer = Physics.AllLayers;

        [SerializeField]
        private bool        allSearch = false;


        public void SetAllSearch(bool a) {  allSearch = a; }
        [SerializeField]
        private bool        find = false;

        public bool         Find => find;

        // 視界範囲内のオブジェクトリスト
        List<GameObject>    insideObjects = new List<GameObject>();

        public List<GameObject> InsideObjects => insideObjects;

        public Vector3 GetSubDistance => targetLastPoint - transform.position;

        public bool IsInside(GameObject obj) => insideObjects.Contains(obj);

        public bool TryGetFirstObject(out GameObject obj)
        {
            if (insideObjects.Count > 0)
            {
                obj = insideObjects[0];
                return true;
            }
            obj = null;
            return false;
        }

        public void DoUpdate()
        {
            currentSearchinTimer.Update(Time.deltaTime);

            //今追っかけてるオブジェクトが見える
            if (IsInside(targetObject))
            {
                if(targetObject != null)
                {
                    targetLastPoint = targetObject.transform.position;
                    currentSearchinTimer.End();
                }
                return;
            }
            else
            {
                targetObject = null;
            }

            //見えないなら

            //新しいオブジェクトが見えたらそちらを追いかけるように切り替える
            if (TryGetFirstObject(out var obj))
            {   
                targetObject = obj;
                targetLastPoint = targetObject.transform.position;
                currentSearchinTimer.End();
                return;
            }

            //新しいオブジェクトもいないなら,一定時間で終了するためタイマースタート
            if (currentSearchinTimer.IsEnd())
            {
                currentSearchinTimer.Start(1.0f);
            }
        }

        private float SubDistance(GameObject obj)
        {
            if(obj == null)
            {
                return 0.0f;
            }
            return (transform.position - obj.transform.position).magnitude;
        }

        public void AllSearchStart()
        {
            if (allSearch) { return; }
            allSearch = true;
            StartCoroutine(EndAllSearch());
        }
        private System.Collections.IEnumerator EndAllSearch()
        {
            yield return new WaitForSecondsRealtime(1f); // 1フレーム待つ
            allSearch = false;
        }

        IEnumerator UpdateRoutine()
        {
            var refreshWait = new WaitForSeconds(refreshTime);
            Collider[] colliders = new Collider[10]; // 予め一定数のコライダ用配列を用意
            while (true)
            {
                yield return refreshWait;

                // 範囲内のコライダを取得し、重複を避けるために一時リストに格納
                int hitCount = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, targetObjectLayer);

                insideObjects.Clear(); // 前の結果をクリア
                for (int i = 0; i < hitCount; i++)
                {
                    GameObject obj = colliders[i].gameObject;

                    // 視野角の範囲内かを確認
                    Vector3 directionToObject = (obj.transform.position - transform.position).normalized;
                    float angle = Vector3.Angle(transform.forward, directionToObject);

                    if (allSearch)
                    {
                        // Raycastで壁越しを除去
                        if (Physics.Raycast(transform.position, directionToObject, out RaycastHit hit, range, targetObjectLayer))
                        {
                            if (hit.transform.gameObject == obj || hit.collider.gameObject.layer == 9)
                            {
                                find = true;
                                insideObjects.Add(obj); // オブジェクトを視界内リストに追加
                            }
                            else
                            {
                                find = false;
                            }
                        }
                    }
                    else
                    {
                        if (angle <= viewAngle)
                        {
                            // Raycastで壁越しを除去
                            if (Physics.Raycast(transform.position, directionToObject, out RaycastHit hit, range, targetObjectLayer))
                            {
                                if (hit.transform.gameObject == obj||hit.collider.gameObject.layer == 9)
                                {
                                    find = true;
                                    insideObjects.Add(obj); // オブジェクトを視界内リストに追加
                                }
                                else
                                {
                                    find = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        void Start()
        {
            StartCoroutine(UpdateRoutine());
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            UnityEditor.Handles.color = new Color(1.0f, 0.0f, 0.0f, 0.3f);
            UnityEditor.Handles.DrawSolidArc(
                transform.position,
                Vector3.up,
                Quaternion.Euler(0, -viewAngle, 0) * transform.forward,
                viewAngle * 2.0f,
                range);
        }
#endif
    }
}

