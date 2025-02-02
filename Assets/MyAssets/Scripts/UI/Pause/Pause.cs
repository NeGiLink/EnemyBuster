using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class Pause : MonoBehaviour
    {
        //ポーズ解除までの待機時間
        private float wait = 0;


        private void Update()
        {
            //解除の待機中以外はスキップするようにしている。
            if (wait <= 0)
            {
                return;
            }

            //Time.timeScaleの影響を受けない、正確な時間を取得してカウントダウンを行う。
            wait -= Time.unscaledDeltaTime;

            //待機時間が終了したらポーズを無効化する
            if (wait <= 0)
            {
                Disable();
            }
        }

        //破棄時にポーズを無効化する。
        private void OnDestroy()
        {
            Disable();
        }

        //ポーズの有効化
        //Time.timeScaleを0に設定することで、ほぼ全てのオブジェクトの動作を停止できる。
        public void Enable()
        {
            Time.timeScale = 0.0f;
        }

        //ポーズの無効化
        //即座にポーズ状態を解除する。
        public void Disable()
        {
            Time.timeScale = 1.0f;
            SystemManager.SetPause(false);
        }

        //ポーズの無効化
        //引数の時間だけ待機してからポーズ状態を解除する。
        public void Disable(float w)
        {
            //0以下を受け取った場合は即座に解除する。
            if (w <= 0)
            {
                Disable();
                return;
            }

            //待機時間を設定し、残りはUpdateに任せる。
            wait = w;
        }
    }
}
