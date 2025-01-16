using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class SwordEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private TrailRenderer   slachEffect;

        [SerializeField]
        private Vector3         slachEffectOffset;

        [SerializeField]
        private Vector3         slachEffectRotation;

        private TrailRenderer   keepSlachEffect;

        // Start is called before the first frame update
        void Start()
        {
            keepSlachEffect = Instantiate(slachEffect,transform);
            Transform slach = keepSlachEffect.transform;
            Vector3 pos = slach.localPosition;
            pos += slachEffectOffset;
            slach.localPosition = pos;
            Vector3 rot = slach.localRotation.eulerAngles;
            rot += slachEffectRotation;
            slach.eulerAngles = rot;
        }

        public void ActivateSlachEffect(bool activate)
        {
            if (keepSlachEffect.enabled == activate) { return; }
            keepSlachEffect.enabled = activate;
        }

    }
}
