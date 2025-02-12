using UnityEngine;

namespace MyAssets
{
    /*
     * プレイヤーが敵に注目した時にUIを表示するためのクラス
     */
    public class LockOnUI : MonoBehaviour
    {
        //プレイヤーの視界
        private FieldOfView     playerFieldOfView;
        //カメラ
        [SerializeField]
        private new Camera      camera;
        //キャンバス
        [SerializeField]
        private Canvas          canvas;
        //キャンバスの矩形トランスフォーム
        [SerializeField]
        private RectTransform   canvasRectTransform;
        //注目UIのトランスフォーム
        [SerializeField]
        private RectTransform   thisRectTransform;

        private void Awake()
        {
            camera = Camera.main;
            canvas = FindObjectOfType<Canvas>();
            canvasRectTransform = canvas.GetComponent<RectTransform>();
            thisRectTransform = GetComponent<RectTransform>();
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerFieldOfView = playerController.GetComponent<FieldOfView>();
        }

        public void LockUpdate()
        {
            if(playerFieldOfView.TargetObject == null) { return; }
            Vector3 target = playerFieldOfView.TargetLastPoint;
            if (camera == null) { return; }
            // 3Dオブジェクトのワールド座標をスクリーン座標に変換
            Vector3 screenPosition = camera.WorldToScreenPoint(target);

            // スクリーン座標をキャンバスのローカル座標に変換
            Vector2 uiPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, null, out uiPosition);

            // UIオブジェクトの位置を設定

            thisRectTransform.localPosition = Vector2.Lerp(thisRectTransform.localPosition, uiPosition, Time.deltaTime * 100.0f);
        }
    }
}
