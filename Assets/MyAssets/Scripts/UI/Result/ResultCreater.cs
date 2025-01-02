using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class ResultCreater : MonoBehaviour
    {
        private void OnDisable()
        {
            GameUIController.Instance.CreateResultUI();
        }
    }
}
