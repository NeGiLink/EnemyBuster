using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class TestRotate : MonoBehaviour
    {


        private void Update()
        {
            transform.Rotate(0, 20, 0);
        }
    }
}
