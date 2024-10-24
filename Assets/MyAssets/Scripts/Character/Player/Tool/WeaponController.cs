using UnityEngine;

namespace MyAssets
{
    public class WeaponController : MonoBehaviour,IEquipment
    {
        [SerializeField]
        private Transform[] weaponTransforms = new Transform[(int)WeaponPositionTag.Count];

        [SerializeField]
        private Transform haveWeapon;

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

        public void SetInWeapon()
        {
            SetTransform(weaponTransforms[(int)WeaponPositionTag.Receipt]);
        }

        public void SetOutWeapon()
        {
            SetTransform(weaponTransforms[(int)WeaponPositionTag.Hand]);
        }

        private void SetTransform(Transform nextTransform)
        {
            haveWeapon.SetParent(null);
            haveWeapon.SetParent(nextTransform);
            haveWeapon.position = nextTransform.position;
            haveWeapon.rotation = nextTransform.rotation;
        }
    }
}
