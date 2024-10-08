using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IPlayerComponent
    {
        void DoSetup(IPlayerSetup player);
    }
    public interface IPlayerState<TKey> : IState<TKey>, IPlayerComponent { }
    public interface IPlayerStateTransition<TKey>
    {
        bool IsTransition();
        void DoTransition();
    }
}
