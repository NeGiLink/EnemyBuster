using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public struct StatusInfo
    {
        public StatusType Type;
        public Sprite sprite;
    }

    [CreateAssetMenu(fileName = "StatusData", menuName = "ScriptableObjects/StatusData", order = 1)]
    public class StatusData : ScriptableObject
    {
        [SerializeField]
        private StatusInfo[] statusInfos;
        public StatusInfo[] StatusInfos => statusInfos;
    }
}
