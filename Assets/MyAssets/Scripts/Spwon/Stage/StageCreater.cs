using UnityEngine;

namespace MyAssets
{
    public class StageCreater : MonoBehaviour
    {
        private GameObject stageObject;


        private void Start()
        {
            stageObject = GameManager.Instance.StageLedger[GameManager.Instance.StageCount];

            Instantiate(stageObject);

            Destroy(gameObject);
        }
    }
}
