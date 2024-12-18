using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class SubSceneLoader : MonoBehaviour
    {
        private static SubSceneLoader instance;
        public static SubSceneLoader Instance => instance;
        [SerializeField]
        private new Camera camera;

        [SerializeField]
        private List<SubScene> subScenes = new List<SubScene>();
        [SerializeField]
        private List<Vector3> subScenesTransform = new List<Vector3>();
        [SerializeField]
        private float cameraRectWidthX = 100f; // 四角形の幅
        [SerializeField]
        private float cameraRectWidthZ = 100f; // 四角形の高さ
        [SerializeField]
        private float rectDistance = 100f; // カメラから四角形までの距離


        [SerializeField]
        private bool debug = false;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            Camera c = FindObjectOfType<Camera>();
            if(c != null)
            {
                camera = c;
            }

            SubScene[] subscenes = GetComponentsInChildren<SubScene>();
            subScenes = new List<SubScene>(subscenes);
            for(int i = 0;i < subscenes.Length; i++)
            {
                subScenesTransform.Add(subScenes[i].gameObject.transform.position);
            }
        }

        private void Update()
        {
            foreach(SubScene targetSubScene in subScenes)
            {
                targetSubScene.DoUpdate();
                if (NoCheckRectanglesOverlap(targetSubScene))
                {
                    if (WithinCameraRange(targetSubScene.transform))
                    {
                        targetSubScene.LoadAsset();
                    }
                    else
                    {
                        if (targetSubScene.IsLoading)
                        {
                            targetSubScene.UnloadAsset();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    if (DoRectanglesOverlap(GetCameraRectangle(), targetSubScene.GetObjectRectangle(targetSubScene)))
                    {
                        targetSubScene.LoadAsset();
                    }
                    else
                    {
                        targetSubScene.UnloadAsset();
                    }
                }
            }
        }

        private bool WithinCameraRange(Transform subScene)
        {
            // カメラからオブジェクトへの方向ベクトルを計算
            Vector3 directionToSubScene = (subScene.position - camera.transform.position).normalized;

            // カメラの前方向ベクトルを取得
            Vector3 cameraForward = camera.transform.forward;

            // カメラ前方向とオブジェクト方向の内積を計算
            float dot = Vector3.Dot(cameraForward, directionToSubScene);

            // 視野角のコサイン値を計算
            float halfFieldOfViewCos = Mathf.Cos(camera.fieldOfView * Mathf.Deg2Rad);

            // 内積が視野角のコサイン値以上なら視野範囲内
            return dot >= halfFieldOfViewCos;
        }

        private bool NoCheckRectanglesOverlap(SubScene subScene)
        {
            Vector3 dis = camera.transform.position - subScene.transform.position;
            dis.y = 0;
            return dis.magnitude > 200.0f;
        }

        /// <summary>
        /// カメラ前の四角形を取得
        /// </summary>
        /// <returns>カメラ前の四角形（中心とサイズ）</returns>
        Rect GetCameraRectangle()
        {
            if(camera == null) { return new Rect(0,0,0,0); }
            // カメラの向きに基づき四角形の中心を計算
            Vector3 rectCenter = camera.transform.position + (camera.transform.forward * 0.2f) * rectDistance;



            return new Rect(
                rectCenter.x - cameraRectWidthX / 2f,
                rectCenter.z - cameraRectWidthX / 2f,
                cameraRectWidthX,
                cameraRectWidthZ
            );
        }

        /// <summary>
        /// 2つの四角形が重なっているかを判定
        /// </summary>
        /// <param name="rectA">四角形A</param>
        /// <param name="rectB">四角形B</param>
        /// <returns>重なっているならtrue、そうでなければfalse</returns>
        bool DoRectanglesOverlap(Rect rectA, Rect rectB)
        {
            return rectA.xMin < rectB.xMax && rectA.xMax > rectB.xMin &&
                   rectA.yMin < rectB.yMax && rectA.yMax > rectB.yMin;
        }

        void OnDrawGizmos()
        {
            if (!debug) { return; }

            // カメラ前の四角形を描画
            DrawRectangle(GetCameraRectangle(), Color.green);

            foreach (SubScene targetSubScene in subScenes)
            {
                // オブジェクトの四角形を描画
                DrawRectangle(targetSubScene.GetObjectRectangle(targetSubScene), Color.blue);
            }
        }

        /// <summary>
        /// 四角形をシーンビューに描画
        /// </summary>
        void DrawRectangle(Rect rect, Color color)
        {
            if (camera == null) { return; }
            Gizmos.color = color;
            Vector3 bottomLeft = new Vector3(rect.xMin, 0, rect.yMin);
            Vector3 bottomRight = new Vector3(rect.xMax, 0, rect.yMin);
            Vector3 topLeft = new Vector3(rect.xMin, 0, rect.yMax);
            Vector3 topRight = new Vector3(rect.xMax, 0, rect.yMax);

            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.DrawLine(bottomRight, topRight);
            Gizmos.DrawLine(topRight, topLeft);
            Gizmos.DrawLine(topLeft, bottomLeft);
        }
    }
}
