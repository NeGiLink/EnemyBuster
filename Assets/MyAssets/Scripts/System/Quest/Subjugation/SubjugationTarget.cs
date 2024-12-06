using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class SubjugationTarget : MonoBehaviour
    {
        private SubjugationManager subjugationManager;

        public void SetSubjugationManager(SubjugationManager s)
        {
            subjugationManager = s;
        }

        private void OnDestroy()
        {
            subjugationManager.Decrease();
        }
    }
}
