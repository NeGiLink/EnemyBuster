using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    [System.Serializable]
    public class StautsGageUI : MonoBehaviour
    {
        private IPlayerSetup playerSetup;
        [SerializeField]
        private Image hpImage;

        [SerializeField]
        private Image spImage;

        private void Awake()
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if(player != null)
            {
                playerSetup = player.GetComponent<IPlayerSetup>();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            GageUpdate();
        }

        private void GageUpdate()
        {
            IPlayerStauts baseStauts = playerSetup.Stauts;
            hpImage.fillAmount = (float)baseStauts.HP / baseStauts.MaxHP;

            spImage.fillAmount = (float)baseStauts.SP / baseStauts.MaxSP;
        }

        // Update is called once per frame
        void Update()
        {
            GageUpdate();
        }
    }
}
