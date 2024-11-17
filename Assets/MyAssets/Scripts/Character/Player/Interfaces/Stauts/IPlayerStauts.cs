using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IBaseStauts
    {
        public int HP { get; }
        public void Recovery(int h);
        public bool DecreaseAndDeathCheck(int d);
    }
    public interface IPlayerStauts : IBaseStauts
    {
        public int SP {  get; }
    }
    public interface ISlimeStauts : IBaseStauts
    {

    }
}
