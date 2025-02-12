using UnityEngine;

namespace MyAssets
{
    public enum SwordEffectType
    {
        Hit
    }
    /*
     * トレイルエフェクトの処理を行うクラス
     */
    public class SwordEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private TrailRenderer   slachEffect;

        [SerializeField]
        private Vector3         slachEffectOffset;

        [SerializeField]
        private Vector3         slachEffectRotation;

        [SerializeField]
        private EffectLedger    effectLedger;
        public  EffectLedger    EffectLedger => effectLedger;

        private TrailRenderer   keepSlachEffect;


        private void Start()
        {
            keepSlachEffect = Instantiate(slachEffect,transform);
            Transform slach = keepSlachEffect.transform;
            Vector3 pos = slach.localPosition;
            pos += slachEffectOffset;
            slach.localPosition = pos;
            Vector3 rot = slach.localRotation.eulerAngles;
            rot += slachEffectRotation;
            slach.eulerAngles = rot;

            ActivateSlachEffect(false);
        }

        public void ActivateSlachEffect(bool activate)
        {
            if(keepSlachEffect == null) { return; }
            if (keepSlachEffect.enabled == activate) { return; }
            keepSlachEffect.enabled = activate;
        }

    }
}
