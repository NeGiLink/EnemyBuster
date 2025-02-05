using System.Collections;
using UnityEngine;

namespace MyAssets
{
    public class TrainingPreparation : MonoBehaviour
    {
        [SerializeField]
        private TrainingController trainingController;


        private PlayerCharacterInput playerCharacterInput;

        private void Awake()
        {
            playerCharacterInput = FindObjectOfType<PlayerCharacterInput>();

            StartCoroutine(PreparationCreate());
        }

        private void Start()
        {
            
        }

        private IEnumerator PreparationCreate()
        {
            yield return new WaitForSecondsRealtime(3f);
            TrainingController controller = Instantiate(trainingController);

        }

        public void GameStart()
        {
            playerCharacterInput.enabled = true;

            BGMHandler.Instance.SetPlayer(true, false, 0, true);
        }
    }
}
