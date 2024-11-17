using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class BaseStautsProperty
    {
        [SerializeField]
        private int hp;
        public int HP => hp;
        public void Recovery(int h)
        {
            hp += h;
        }

        public bool DecreaseAndDeathCheck(int d)
        {
            hp -= d;
            if(hp <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
