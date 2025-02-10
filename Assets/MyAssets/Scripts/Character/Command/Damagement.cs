using UnityEngine;

namespace MyAssets
{
    /*
     * ダメージを受けた時にノックバックを与える処理を行うクラスです。
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
