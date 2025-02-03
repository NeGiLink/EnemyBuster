using UnityEngine;

namespace MyAssets
{
    public class OptionChangeController : MonoBehaviour
    {
        [SerializeField]
        private OptionPanel[] optionPanel;

        private SEHandler seHandler;

        private void Awake()
        {
            seHandler = GetComponent<SEHandler>();
        }

        public void Change(int index)
        {
            optionPanel[index].Enable();
            seHandler.Play((int)ButtonSETag.Decide2);
            for(int i = 0; i < optionPanel.Length; i++)
            {
                if(i == index) { continue; }
                optionPanel[i].Disable();
            }
        }
    }
}
