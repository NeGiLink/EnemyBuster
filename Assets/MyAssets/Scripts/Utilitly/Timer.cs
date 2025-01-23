using System;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable ]
    public class Timer
    {
        public event Action OnceEnd;
        public event Action OnEnd;

        private float current = 0;

        public float Current => current;

        private bool up = false;

        public void SetCountUp( bool u)
        {
            up = u;
        }

        public void Start(float time)
        {
            current = time;
        }

        public void Update(float time)
        {
            if (up)
            {
                current += time;
            }
            else
            {
                if (current <= 0) { return; }
                current -= time;
                if (current <= 0)
                {
                    current = 0;
                    End();
                }
            }
        }

        public void End()
        {
            current = 0;
            OnEnd?.Invoke();
            OnceEnd?.Invoke();
            OnceEnd = null;
        }

        public bool IsEnd() { return current <= 0; }

        public int GetMinutes()
        {
            return (int)current / 60;
        }

        public int GetSecond()
        {
            return (int)current % 60;
        }
    }
}
