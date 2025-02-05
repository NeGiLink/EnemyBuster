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
            IPlayerSetup player = other.GetComponent<IPlayerSetup>();
            if(player == null) { return; }
            int recovery = Random.Range(minRecoveryNum, maxRecoveryNum + 1);
            player.BaseStauts.RecoveryHP(recovery);
            player.SEHandler.Play((int)PlayerSETag.Recovery);
            gameObject.SetActive(false);
        }
    }
}
