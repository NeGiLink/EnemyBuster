using System;
using UnityEngine;

namespace MyAssets
{
    public class CreditController : MonoBehaviour
    {
        private static CreditController instance;
        public static CreditController Instance => instance;

        [SerializeField]
        private CreditObject[] creditObjects;
        [SerializeField]
        private float creditObjectMoveOffset = 10f;
        [SerializeField]
        private int currentIndex = 0;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                Debug.LogWarning(string.Format("{0}ÇÕÇ∑Ç≈Ç…ë∂ç›Ç∑ÇÈÇΩÇﬂçÌèúÇ≥ÇÍÇ‹ÇµÇΩÅB", gameObject.name), gameObject);
                return;
            }
            instance = this;

            //creditObjects = FindObjectsOfType<CreditObject>();
        }

        private void Start()
        {
            GameManager.Instance.SetSceneList(SceneList.Credit);
            InputManager.SetFreeCursor();
            ChangeCreditObject(0);
        }

        public void ChangeCreditObject(int index)
        {
            currentIndex += index;
            if(currentIndex < 0)
            {
                currentIndex = creditObjects.Length - 1;
            }
            else if(currentIndex > creditObjects.Length - 1)
            {
                currentIndex = 0;
            }
            creditObjects[currentIndex].gameObject.SetActive(true);
            Vector3 pos = creditObjects[currentIndex].transform.position;
            pos.x = 0;
            creditObjects[currentIndex].transform.position = pos;

            for (int i = 0; i < creditObjects.Length; i++)
            {
                if (i == currentIndex) { continue; }
                creditObjects [i].gameObject.SetActive(false);
                pos = creditObjects[i].transform.position;
                pos.x = creditObjectMoveOffset;
                creditObjects[i].transform.position = pos;
            }
        }
    }
}
