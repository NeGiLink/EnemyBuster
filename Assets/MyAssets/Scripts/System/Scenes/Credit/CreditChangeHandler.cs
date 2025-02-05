using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    public enum CreditHandel
    {
        Left = -1,
        Right = 1
    }
    public class CreditChangeHandler : MonoBehaviour
    {
        [SerializeField]
        private CreditHandel handel;

        private Button button;

        private void OnEnable()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Change);
        }

        public void Change()
        {
            CreditController.Instance.ChangeCreditObject((int)handel);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Change);
        }
    }
}
