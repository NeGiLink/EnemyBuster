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
        private float cameraRectWidthX = 100f; // �l�p�`�̕�
        [SerializeField]
        private float cameraRectWidthZ = 100f; // �l�p�`�̍���
        [SerializeField]
        private float rectDistance = 100f; // �J��������l�p�`�܂ł̋���


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
            // �J��������I�u�W�F�N�g�ւ̕����x�N�g�����v�Z
            Vector3 directionToSubScene = (subScene.position - camera.transform.position).normalized;

            // �J�����̑O�����x�N�g�����擾
            Vector3 cameraForward = camera.transform.forward;

            // �J�����O�����ƃI�u�W�F�N�g�����̓��ς��v�Z
            float dot = Vector3.Dot(cameraForward, directionToSubScene);

            // ����p�̃R�T�C���l���v�Z
            float halfFieldOfViewCos = Mathf.Cos(camera.fieldOfView * Mathf.Deg2Rad);

            // ���ς�����p�̃R�T�C���l�ȏ�Ȃ王��͈͓�
            return dot >= halfFieldOfViewCos;
        }

        private bool NoCheckRectanglesOverlap(SubScene subScene)
        {
            Vector3 dis = camera.transform.position - subScene.transform.position;
            dis.y = 0;
            return dis.magnitude > 200.0f;
        }

        /// <summary>
        /// �J�����O�̎l�p�`���擾
        /// </summary>
        /// <returns>�J�����O�̎l�p�`�i���S�ƃT�C�Y�j</returns>
        Rect GetCameraRectangle()
        {
            if(camera == null) { return new Rect(0,0,0,0); }
            // �J�����̌����Ɋ�Â��l�p�`�̒��S���v�Z
            Vector3 rectCenter = camera.transform.position + (camera.transform.forward * 0.2f) * rectDistance;



            return new Rect(
                rectCenter.x - cameraRectWidthX / 2f,
                rectCenter.z - cameraRectWidthX / 2f,
                cameraRectWidthX,
                cameraRectWidthZ
            );
        }

        /// <summary>
        /// 2�̎l�p�`���d�Ȃ��Ă��邩�𔻒�
        /// </summary>
        /// <param name="rectA">�l�p�`A</param>
        /// <param name="rectB">�l�p�`B</param>
        /// <returns>�d�Ȃ��Ă���Ȃ�true�A�����łȂ����false</returns>
        bool DoRectanglesOverlap(Rect rectA, Rect rectB)
        {
            return rectA.xMin < rectB.xMax && rectA.xMax > rectB.xMin &&
                   rectA.yMin < rectB.yMax && rectA.yMax > rectB.yMin;
        }

        void OnDrawGizmos()
        {
            if (!debug) { return; }

            // �J�����O�̎l�p�`��`��
            DrawRectangle(GetCameraRectangle(), Color.green);

            foreach (SubScene targetSubScene in subScenes)
            {
                // �I�u�W�F�N�g�̎l�p�`��`��
                DrawRectangle(targetSubScene.GetObjectRectangle(targetSubScene), Color.blue);
            }
        }

        /// <summary>
        /// �l�p�`���V�[���r���[�ɕ`��
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
