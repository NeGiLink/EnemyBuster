using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class FieldOfView : MonoBehaviour
    {
        private Timer currentSearchinTimer = new Timer();
        [SerializeField]
        private GameObject targetObject;
        public GameObject TargetObject => targetObject;
        [SerializeField]
        private Vector3 targetLastPoint;


        [SerializeField]
        float refreshTime = 0.1f;

        [SerializeField]
        float range = 10.0f;

        [SerializeField]
        float viewAngle = 45.0f;

        [SerializeField]
        LayerMask targetObjectLayer = Physics.AllLayers;

        [SerializeField]
        private bool allSearch = false;

        // ���E�͈͓��̃I�u�W�F�N�g���X�g
        List<GameObject> insideObjects = new List<GameObject>();

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

            //���ǂ������Ă�I�u�W�F�N�g��������
            if (IsInside(targetObject))
            {
                targetLastPoint = targetObject.transform.position;
                currentSearchinTimer.End();
                return;
            }
            else
            {
                targetObject = null;
            }

            //�����Ȃ��Ȃ�

            //�V�����I�u�W�F�N�g���������炻�����ǂ�������悤�ɐ؂�ւ���
            if (TryGetFirstObject(out var obj))
            {
                targetObject = obj;
                targetLastPoint = targetObject.transform.position;
                currentSearchinTimer.End();
                return;
            }

            //�V�����I�u�W�F�N�g�����Ȃ��Ȃ�,��莞�ԂŏI�����邽�߃^�C�}�[�X�^�[�g
            if (currentSearchinTimer.IsEnd())
            {
                currentSearchinTimer.Start(1.0f);
            }
        }

        IEnumerator UpdateRoutine()
        {
            var refreshWait = new WaitForSeconds(refreshTime);
            Collider[] colliders = new Collider[10]; // �\�߈�萔�̃R���C�_�p�z���p��
            while (true)
            {
                yield return refreshWait;

                // �͈͓��̃R���C�_���擾���A�d��������邽�߂Ɉꎞ���X�g�Ɋi�[
                int hitCount = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, targetObjectLayer);

                insideObjects.Clear(); // �O�̌��ʂ��N���A
                for (int i = 0; i < hitCount; i++)
                {
                    GameObject obj = colliders[i].gameObject;

                    // ����p�͈͓̔������m�F
                    Vector3 directionToObject = (obj.transform.position - transform.position).normalized;
                    float angle = Vector3.Angle(transform.forward, directionToObject);

                    if (allSearch)
                    {
                        // Raycast�ŕǉz��������
                        if (Physics.Raycast(transform.position, directionToObject, out RaycastHit hit, range, targetObjectLayer))
                        {
                            if (hit.transform.gameObject == obj || hit.collider.gameObject.layer == 9)
                            {
                                insideObjects.Add(obj); // �I�u�W�F�N�g�����E�����X�g�ɒǉ�
                            }
                        }
                    }
                    else
                    {
                        if (angle <= viewAngle)
                        {
                            // Raycast�ŕǉz��������
                            if (Physics.Raycast(transform.position, directionToObject, out RaycastHit hit, range, targetObjectLayer))
                            {
                                if (hit.transform.gameObject == obj||hit.collider.gameObject.layer == 9)
                                {
                                    insideObjects.Add(obj); // �I�u�W�F�N�g�����E�����X�g�ɒǉ�
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

