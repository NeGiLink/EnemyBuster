using UnityEngine;

namespace MyAssets
{
    /*
     * �_���[�W���󂯂����Ƀm�b�N�o�b�N��^���鏈�����s���N���X�ł��B
     */
    [System.Serializable]
    public class Damagement : IDamagement,ICharacterComponent<ICharacterSetup>
    {

        private IVelocityComponent velocity;


        public void DoSetup(ICharacterSetup chara)
        {
            velocity = chara.Velocity;
        }

        public void AddForceMove(Vector3 origin, Vector3 target, float power)
        {
            Vector3 sub = origin - target;
            velocity.Rigidbody.AddForce(sub * power, ForceMode.VelocityChange);
        }
    }
}
