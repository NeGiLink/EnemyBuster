using UnityEngine;

namespace MyAssets
{
    /*
     * アイテムをオブジェクトプールで生成するクラス
     */
    public class ItemCreateMachine : MonoBehaviour
    {
        //アイテム
        [SerializeField]
        private RecoveryHealth  health;

        //itemを保持（プーリング）する空のオブジェクト
        private Transform       items;


        private void Start()
        {
            //アイテムを保持する空のオブジェクトを生成
            items = new GameObject("ItemPool").transform;
            items.SetParent(transform);
        }

        /// <summary>
        /// アイテム生成関数
        /// </summary>
        /// <param name="pos">生成位置</param>
        /// <param name="rotation">生成時の回転</param>
        public RecoveryHealth InstRecoveryHealth(Vector3 pos, Quaternion rotation)
        {
            //アクティブでないオブジェクトをbulletsの中から探索
            foreach (Transform t in items)
            {
                if (!t.gameObject.activeSelf)
                {
                    //非アクティブなオブジェクトの位置と回転を設定
                    t.SetPositionAndRotation(pos, rotation);
                    //アクティブにする
                    t.gameObject.SetActive(true);
                    return t.GetComponent<RecoveryHealth>();
                }
            }
            //非アクティブなオブジェクトがない場合新規生成

            //生成時にアイテムの子オブジェクトにする
            return Instantiate(health, pos, rotation, items);
        }
    }
}
