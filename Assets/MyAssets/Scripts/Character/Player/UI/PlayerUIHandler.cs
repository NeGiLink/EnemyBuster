using System.Collections;
using UnityEngine;

namespace MyAssets
{
    public class PlayerUIHandler : MonoBehaviour
    {
        [SerializeField]
        private GageUI hpGage;
        public GageUI HPgage => hpGage;
        [SerializeField]
        private GageUI spGage;
        public GageUI SpGage => spGage;

        [SerializeField]
        private LockOnUI lockOnUI;
        public LockOnUI LockOnUI => lockOnUI;

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
