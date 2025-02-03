using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MyAssets
{
    [RequireComponent(typeof(Pause))]
    public class PauseMenu : MonoBehaviour
    {
        public static PauseMenu Instance { get; private set; }


        //時を止めるためのコンポーネント
        private Pause pause;

        //効果音再生用のコンポーネント
        [SerializeField]
        private SEHandler seHandler;

        private void Awake()
        {
            //シングルトン用の変数に代入する。
            Instance = this;



            seHandler = GetComponent<SEHandler>();
            pause = GetComponent<Pause>();
        }

        private void Start()
        {

            //ポーズを有効化する。
            pause.Enable();

            SystemManager.SetPause(true);
        }

        private void OnDestroy()
        {
            //オブジェクトの破棄をシングルトン用変数に伝える。
            //万が一、自身以外が入っていた場合は何もしない。
            if (Instance == this)
            {
                Instance = null;
            }
        }



        //メニュー終了処理
        public void Close()
        {

            //オブジェクトの破棄
            Destroy(gameObject);
        }
    }
}
