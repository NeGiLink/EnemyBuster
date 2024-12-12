using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    [System.Serializable]
    public class HPUI : MonoBehaviour
    {
        private IPlayerSetup playerSetup;
        [SerializeField]
        private Image image;

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
            IBaseStauts baseStauts = playerSetup.BaseStauts;
            image.fillAmount = baseStauts.HP / baseStauts.MaxHP;
        }

        // Update is called once per frame
        void Update()
        {
            IBaseStauts baseStauts = playerSetup.BaseStauts;
            image.fillAmount = (float)baseStauts.HP / baseStauts.MaxHP;
        }
    }
}
