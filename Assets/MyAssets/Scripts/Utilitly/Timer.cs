

namespace MyAssets
{
    [System.Serializable ]
    public class Timer
    {
        private float count = 0.0f;

        private bool isStart = false;

        public void Start(float time)
        {
            count = time;

            isStart = true;
        }

        public void DoRefresh(float time)
        {
            if (!isStart) { return; }
            count -= time;
        }

        public bool IsCounting() => isStart && count > 0.0f;

        public bool IsEnd() => isStart && count < 0.0f;

        public void End()
        {
            count = 0.0f;
            isStart = false;
        }
    }
}
