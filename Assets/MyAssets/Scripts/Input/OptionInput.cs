using UnityEngine;

namespace MyAssets
{
    public class OptionInput : MonoBehaviour
    {
        private MenuCaller caller;

        private bool open = false;

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
                GameManager.Instance.SetFreeCursor();
            }
            else
            {
                caller.Close();
                GameManager.Instance.SetLockCursor();
            }
        }
    }
}
