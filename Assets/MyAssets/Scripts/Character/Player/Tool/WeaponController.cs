using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private Transform[] weaponTransforms = new Transform[2];

        private void Awake()
        {
            WeaponPosition[] weaponPosition = GetComponentsInChildren<WeaponPosition>();
            for(int i = 0; i < weaponTransforms.Length; i++)
            {
                if (weaponPosition[i].Tag == WeaponPositionTag.Hand)
                {
                    weaponTransforms[(int)WeaponPositionTag.Hand] = weaponPosition[i].ThisObject.transform;
                }
                else if (weaponPosition[i].Tag == WeaponPositionTag.Receipt)
                {
                    weaponTransforms[(int)WeaponPositionTag.Receipt] = weaponPosition[i].ThisObject.transform;
                }
            }
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
