using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IBaseStauts
    {
        int MaxHP { get; }
        int HP { get; }
        void Recovery(int h);
        bool DecreaseAndDeathCheck(int d);
        int MaxStoredDamage {  get; }
        int StoredDamage {  get; }
        bool IsMaxStoredDamage(int damage);
        void ClearStoredDamage();

        public Timer InvincibilityTimer {  get; }

        void DoUpdate(float time);
    }
    public interface IPlayerStauts : IBaseStauts
    {
        int SP {  get; }
    }
    public interface ISlimeStauts : IBaseStauts
    {

    }
    public interface IMushroomStauts : IBaseStauts
    {

    }
    public interface INPCStauts : IBaseStauts
    {

    }
}
