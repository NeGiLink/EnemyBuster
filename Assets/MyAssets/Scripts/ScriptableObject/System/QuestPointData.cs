using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [Serializable]
    public struct Point
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
    }

    [CreateAssetMenu(fileName = "QuestPointData", menuName = "ScriptableObjects/QuestPointData", order = 1)]
    public class QuestPointData : ScriptableObject
    {
        [SerializeField]
        private List<Point> points;
        public List<Point> Points => points;
    }
}
