using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class PatrplPointContainer : MonoBehaviour
    {
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
    }
}
