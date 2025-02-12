using UnityEngine;

namespace MyAssets
{
    /*
     * ����|�C���g��z��ŕێ�����N���X
     */
    public class PatrplPointContainer : MonoBehaviour
    {
        [Header("Transform����ɂ�������|�C���g���W")]
        [SerializeField]
        private Vector3[]   patrolPoints;
        public Vector3[]    PatrolPoints => patrolPoints;
        [SerializeField]
        private int         currentPoint = 0;
        public int          CurrentPoint => currentPoint;

        private bool        stop = false;
        public bool         IsStop => stop;
        public void SetCurrentPoint(int point) {  currentPoint = point; }
        public void SetStop(bool s) { stop = s; }

        private void Awake()
        {
            // ����|�C���g����I�u�W�F�N�g�̈ʒu����ɏ�����
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                patrolPoints[i] += transform.position;
            }
        }
    }
}
