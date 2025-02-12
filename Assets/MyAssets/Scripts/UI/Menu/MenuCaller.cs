using UnityEngine;

namespace MyAssets
{
    public class MenuCaller : MonoBehaviour
    {
        //メニューを召喚するためのキャンバス
        //キャンバスが複数ある場合など、勝手に設定されると困る場合は手動で設定。
        //キャンバスが単体なら未設定でも参照して取得。
        [SerializeField]
        private Canvas canvas;

        private Canvas TargetCanvas
        {
            get
            {
                if (canvas == null)
                {
                    /*トランジション用のキャンバスを除外して選択*/

                    Canvas c = FindObjectOfType<Canvas>();
                    if (c != null)
                    {
                        canvas = c;
                    }
                }
                return canvas;
            }
        }

        //召喚するメニューのプレハブ
        [SerializeField]
        private PauseMenu menuPrefab;

        //メニュー呼び出し
        public void Call()
        {
            //publicで作成しているためnullチェックを挟む。
            if (PauseMenu.Instance != null)
            {
                return;
            }
            //対象のキャンバスに生成する。
            PauseMenu menu = Instantiate(menuPrefab, TargetCanvas.transform);
        }

        //メニューの終了
        public void Close()
        {
            //publicで作成しているためnullチェックを挟みながら閉じる。
            PauseMenu.Instance?.Close();
        }
    }
}
