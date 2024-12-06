using UnityEngine;

namespace MyAssets
{
    public class SubjugationManager : MonoBehaviour
    {
        [SerializeField]
        private int targetIndex;

        private void Start()
        {
            int index = transform.childCount;
            targetIndex = index;
            SlimeController[] targets = GetComponentsInChildren<SlimeController>();
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].gameObject.AddComponent<SubjugationTarget>();
                SubjugationTarget subjugationTarget = targets[i].GetComponent<SubjugationTarget>();
                subjugationTarget.SetSubjugationManager(this);
            }
        }

        public void Decrease()
        {
            targetIndex--;
            if(targetIndex <= 0)
            {
                Quest.Instance.NotSubjugation();
                Destroy(gameObject);
            }
        }
    }
}
