using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets 
{
    public class DamageTextCreator : MonoBehaviour
    {
        [SerializeField]
        private GameObject textMesh;
        public void Crate(Transform damager,int damage)
        {
            DamageText3DUI damageText3DUI = Instantiate(textMesh,damager.position,Quaternion.identity).GetComponent<DamageText3DUI>();
            if(damageText3DUI != null)
            {
                damageText3DUI.Setup();
                damageText3DUI.NowText.text = damage.ToString();
            }
        }
    }
}
