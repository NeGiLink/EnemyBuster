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

        [Header("�q�b�g�t���O"), SerializeField]
        private bool[] cliffHits = new bool[(int)JudgmentTag.Count];

        [Header("�O���ɔ�΂����C�̈ʒu"), SerializeField]
        private float[] cliffCheckOffsets = new float[]
        {
            0.5f,
            0.0f,
        };

        [Header("���C�̒���"),SerializeField]
        private float[] cliffDistances = new float[]
        {
            0.2f,
            0.2f
        };

        [SerializeField]
        private bool climbStart;
        public bool IsClimbStart => climbStart;

        public void DoSetup(IPlayerSetup player)
        {
            transform = player.gameObject.transform;
        }

        [SerializeField]
        private Transform rayTransform;

        public void RayCheck()
        {
            climbStart = false;
            Ray[] cliffRays = new Ray[(int)JudgmentTag.Count];

            RaycastHit[] hit = new RaycastHit[(int)JudgmentTag.Count];
            for(int i = 0; i < cliffRays.Length; i++)
            {
                //�����쐬
                cliffRays[i] = new Ray(rayTransform.position + Vector3.up * cliffCheckOffsets[i],transform.forward);
                cliffHits[i] = Physics.Raycast(cliffRays[i],out hit[i], cliffDistances[i]);
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
