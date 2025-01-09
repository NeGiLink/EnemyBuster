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

        private void Awake()
        {
            StartCoroutine(Setup());
        }

        private IEnumerator Setup()
        {
            yield return new WaitForSecondsRealtime(0.1f);

            GameCanvas gameCanvas = FindObjectOfType<GameCanvas>();

            GageUI gage = Instantiate(hpGage, gameCanvas.UILayer[(int)UILayer.Player].transform);
            hpGage = gage;
            gage = Instantiate(spGage, gameCanvas.UILayer[(int)UILayer.Player].transform);
            spGage = gage;
        }
    }
}
