using UnityEngine;

namespace MyAssets
{
    /*
     * オプションの処理にアタッチして使うオプション入力クラス
     */
    public class OptionInput : MonoBehaviour
    {
        private MenuCaller  caller;

        private bool        open = false;

        private void Awake()
        {
            caller = GetComponent<MenuCaller>();
        }

        private void Start()
        {
            open = false;
        }

        private void Update()
        {
            if (!InputUIAction.Instance.Pause) { return; }
            open = !open;
            if (open)
            {
                caller.Call();
                InputManager.SetFreeCursor();
            }
            else
            {
                caller.Close();
                InputManager.SetLockCursor();
            }
        }
    }
}
