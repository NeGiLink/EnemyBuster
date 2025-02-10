using UnityEngine;

namespace MyAssets
{
    public class RecoveryHealth : MonoBehaviour
    {

        [SerializeField]
        private int minRecoveryNum;
        [SerializeField]
        private int maxRecoveryNum;

        private void OnTriggerEnter(Collider other)
        {
            IPlayerSetup actor = other.GetComponent<IPlayerSetup>();
            if(actor == null) { return; }
            int recovery = Random.Range(minRecoveryNum, maxRecoveryNum + 1);
            actor.BaseStauts.RecoveryHP(recovery);
            actor.SEHandler.Play((int)PlayerSETag.Recovery);
            gameObject.SetActive(false);
        }
    }
}
