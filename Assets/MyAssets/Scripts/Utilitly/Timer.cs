using System;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable ]
    public class Timer
    {
        public event Action OnEnd;

        private float current = 0;

        public float Current => current;

        public void Start(float time)
        {
            current = time;
        }

        public void Update(float time)
        {
            if (current <= 0) { return; }
            current -= time;
            if (current <= 0)
            {
                current = 0;
                End();
            }
        }

        public void End()
        {
            current = 0;
            OnEnd?.Invoke();
            OnEnd = null;
        }

        public bool IsEnd() { return current <= 0; }
    }
}
