using UnityEngine;

namespace MyAssets
{
    public class PatrplPointContainer : MonoBehaviour
    {
        [Header("Transformを基準にした巡回ポイント座標")]
        [SerializeField]
        private Vector3[] patrolPoints;
        public Vector3[] PatrolPoints => patrolPoints;
        [SerializeField]
        int currentPoint = 0;
        public int CurrentPoint => currentPoint;
        public void SetCurrentPoint(int point) {  currentPoint = point; }

        private bool stop = false;
        public bool IsStop => stop;
        public void SetStop(bool s) { stop = s; }

        private void Awake()
        {
            // 巡回ポイントを基準オブジェクトの位置を基に初期化
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                patrolPoints[i] += transform.position;
            }
        }
    }
}
