using UnityEngine;

namespace MyAssets
{
    /*
     * ����UI�̐������s���N���X
     */
    public class ResultCreater : MonoBehaviour
    {
        //��\���ɂȂ����琶��
        private void OnDisable()
        {
            GameUIController.Instance.CreateResultUI();
        }
    }
}
