using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IBaseStauts
    {
        int MaxHP { get; }
        int HP { get; }
        void RecoveryHP(int h);
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
        int MaxSP { get; }
        int SP {  get; }
        void DecreaseSP(int s);
        void RecoverySP(int s);
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
