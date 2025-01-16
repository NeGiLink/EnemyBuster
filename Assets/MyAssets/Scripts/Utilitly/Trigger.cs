using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * 
     */
    public class Trigger
    {
        private bool trigger;
        public bool IsTrigger => trigger;
        public void SetTrigger(bool t) {  trigger = t; }
    }
}
