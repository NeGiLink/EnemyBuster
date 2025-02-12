using UnityEngine;

namespace MyAssets
{
    public enum StageType
    {
        Stage00,
        Stage01,

        Count
    }
    /*
     * スクリプタブルオブジェクトに設定されているステージオブジェクトをランダムに生成する
     */
    public class StageCreater : MonoBehaviour
    {
        [SerializeField]
        private StageLedger     stageLedger;

        private GameObject      stageObject;
        //ゲームが始まったら処理開始
        private void Start()
        {
            int index = Random.Range(0, (int)StageType.Count);
            stageObject = stageLedger[index];

            Instantiate(stageObject);

            Destroy(gameObject);
        }
    }
}
