using UnityEngine;

namespace MyAssets
{
    public enum PlayerEffectType
    {
        GroundHit,
        Damage,
        ChargeSlash,
        Charge
    }

    public class PlayerEffectController : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger        effectLedger;
        public EffectLedger         EffectLedger => effectLedger;
        [SerializeField]
        private ParticleSystem[]    chargeParticles;

        private ParticleSystem      chargeEffect;
        public ParticleSystem       ChargeEffect => chargeEffect;

        public void SetChargeEffect(bool active,Color color)
        {
            chargeEffect.gameObject.SetActive(active);
            for(int i = 0; i < chargeParticles.Length; i++) 
            {
                chargeParticles[i].startColor = color; 
            }
        }

        private void Start()
        {
            chargeEffect = Create(PlayerEffectType.Charge, ChargePosition, ChargeRotation);
            chargeParticles = chargeEffect.GetComponentsInChildren<ParticleSystem>();
            chargeEffect.gameObject.SetActive(false);
        }


        public Vector3 ChargeSlashRotation => new Vector3(-14f, -90f, 180f);
        public Vector3 ChargeRotation => new Vector3(0f, 180f, 0f);
        public Vector3 ChargePosition => new Vector3(0f, 1.044f, 0f);
        public ParticleSystem Create(PlayerEffectType type)
        {
            return Instantiate(effectLedger[(int)type],transform.position, effectLedger[(int)type].transform.rotation);
        }

        public ParticleSystem Create(PlayerEffectType type,Vector3 pos, Vector3 rot)
        {
            Quaternion r = Quaternion.Euler(rot);
            ParticleSystem particle = Instantiate(effectLedger[(int)type], transform);
            particle.transform.localPosition = pos;
            particle.transform.localRotation = r;
            return particle;
        }


        public ParticleSystem Create(PlayerEffectType type,Vector3 rot)
        {
            Quaternion r = Quaternion.Euler(rot);
            ParticleSystem particle = Instantiate(effectLedger[(int)type],transform);
            particle.transform.localRotation = r;
            return particle;
        }
    }
}

