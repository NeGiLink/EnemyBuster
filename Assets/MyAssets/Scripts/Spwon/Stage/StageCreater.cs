using UnityEngine;

namespace MyAssets
{
    public enum StageType
    {
        Stage00,
        Stage01,

        Count
    }
    public class StageCreater : MonoBehaviour
    {
        private GameObject stageObject;

        private void Start()
        {
            int index = Random.Range(0, (int)StageType.Count);
            stageObject = GameManager.Instance.StageLedger[index];

            Instantiate(stageObject);

            Destroy(gameObject);
        }
    }
}
