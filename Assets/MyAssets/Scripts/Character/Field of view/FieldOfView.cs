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
        //null����Ȃ��̂Ȃ�true�Anull�Ȃ�false
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

        // ���E�͈͓��̃I�u�W�F�N�g���X�g
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

            //���ǂ������Ă�I�u�W�F�N�g��������
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
            yield return new WaitForSecondsRealtime(1f); // 1�t���[���҂�
            allSearch = false;
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
                                find = true;
                                insideObjects.Add(obj); // �I�u�W�F�N�g�����E�����X�g�ɒǉ�
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
                            // Raycast�ŕǉz��������
                            if (Physics.Raycast(transform.position, directionToObject, out RaycastHit hit, range, targetObjectLayer))
                            {
                                if (hit.transform.gameObject == obj||hit.collider.gameObject.layer == 9)
                                {
                                    find = true;
                                    insideObjects.Add(obj); // �I�u�W�F�N�g�����E�����X�g�ɒǉ�
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

