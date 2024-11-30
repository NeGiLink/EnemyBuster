using UnityEngine;

namespace MyAssets
{
    public enum JudgmentTag
    {
        MaxRay,
        MinRay,
        Count
    }
    [System.Serializable]
    public class ObstacleJudgment : IObstacleJudgment,IPlayerComponent
    {

        [SerializeField]
        private Transform transform;

        [Header("ヒットフラグ"), SerializeField]
        private bool[] cliffHits = new bool[(int)JudgmentTag.Count];

        [Header("前方に飛ばすレイの位置"), SerializeField]
        private float[] cliffCheckOffsets = new float[]{};

        [Header("レイの長さ"),SerializeField]
        private float[] cliffDistances = new float[]{};

        [SerializeField]
        private bool climbStart;
        public bool IsClimbStart => climbStart;

        [SerializeField]
        private LayerMask targetLayer;

        [SerializeField]
        private Transform rayTransform;

        // 値の範囲
        private const float MinValue = -0.6f;
        private const float MaxValue = -0.1f;

        // 周期（値が増減する速さを調整）
        [SerializeField]
        private float speed = 1.0f;

        // 現在の値（範囲内で増減する値）
        private float currentValue;

        public void DoSetup(IPlayerSetup player)
        {
            transform = player.gameObject.transform;
        }


        public void RayCheck()
        {
            climbStart = false;
            Ray[] cliffRays = new Ray[(int)JudgmentTag.Count];

            RaycastHit[] hit = new RaycastHit[(int)JudgmentTag.Count];
            for(int i = 0; i < cliffRays.Length; i++)
            {
                float upOffset = 0;
                if(i == (int)JudgmentTag.MaxRay)
                {
                    upOffset = cliffCheckOffsets[i];
                }
                else
                {
                    currentValue = Mathf.PingPong(Time.time * speed, MaxValue - MinValue) + MinValue;
                    upOffset = currentValue;
                }
                //光線作成
                cliffRays[i] = new Ray(rayTransform.position + Vector3.up * upOffset, transform.forward);
                cliffHits[i] = Physics.Raycast(cliffRays[i], out hit[i], cliffDistances[i], targetLayer);
                Debug.DrawRay(cliffRays[i].origin, cliffRays[i].direction * cliffDistances[i], Color.red);
            }

            for(int i = 0; i < cliffHits.Length; i++)
            {
                if (hit[i].collider == null) { continue; }
                if (hit[i].collider.gameObject.tag == "Player") { return; }
            }

            if (!cliffHits[(int)JudgmentTag.MaxRay]&& cliffHits[(int)JudgmentTag.MinRay])
            {
                climbStart = true;
            }

        }

        public void InitRay()
        {
            for(int i = 0; i < cliffHits.Length; i++)
            {
                cliffHits[i] = false;
            }
            climbStart = false;
        }
    }
}
