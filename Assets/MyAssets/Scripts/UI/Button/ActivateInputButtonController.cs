using UnityEngine;

namespace MyAssets
{
    /*
 * シーン内にInputButtonControllerが二つあった場合
 *自分のとは違うInputButtonControllerをオフにする
 *これならこのコンポーネントをつけたメニューを二つ、三つと付けても
 *その開いたメニューだけ選択できる
 */
    public class ActivateInputButtonController : MonoBehaviour
    {
        private InputButtonController thisInputButtonController;

        private InputButtonController[] inputButtonController;
        //表示時に現在使えるボタンだけを有効にする
        private void OnEnable()
        {
            thisInputButtonController = GetComponent<InputButtonController>();
            inputButtonController = FindObjectsOfType<InputButtonController>();
            for (int i = 0; i < inputButtonController.Length; i++)
            {
                if (inputButtonController[i] != thisInputButtonController)
                {
                    inputButtonController[i].enabled = false;
                }
            }
        }
        //非表示に自分以外のボタンを有効にする
        private void OnDisable()
        {
            for (int i = 0; i < inputButtonController.Length; i++)
            {
                if (inputButtonController[i] != thisInputButtonController)
                {
                    inputButtonController[i].ActivateStart();
                }
            }
        }
    }
}
