using UnityEngine;

namespace MyAssets
{
    /*
     * プレイヤーに関係するUIを生成するクラス
     * プレイヤーに関係するUIはプレイヤーが移動できるようになったら生成するという考えから
     * プレイヤーにアタッチして使用
     */
    public class PlayerUIHandler : MonoBehaviour
    {
        [SerializeField]
        private GageUI      hpGage;
        public GageUI       HPgage => hpGage;
        [SerializeField]
        private GageUI      spGage;
        public GageUI       SpGage => spGage;

        [SerializeField]
        private LockOnUI    lockOnUI;
        public LockOnUI     LockOnUI => lockOnUI;

        public void Create()
        {

            GameCanvas gameCanvas = FindObjectOfType<GameCanvas>();

            GageUI gage = Instantiate(hpGage, gameCanvas.UILayer[(int)UILayer.Player].transform);
            hpGage = gage;
            gage = Instantiate(spGage, gameCanvas.UILayer[(int)UILayer.Player].transform);
            spGage = gage;
            LockOnUI lockOn = Instantiate(lockOnUI, gameCanvas.UILayer[(int)UILayer.Player].transform);
            lockOnUI = lockOn;
            Destroy(this);
        }
    }
}
