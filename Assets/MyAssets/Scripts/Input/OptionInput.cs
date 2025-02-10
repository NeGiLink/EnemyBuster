using UnityEngine;

namespace MyAssets
{
    /*
     * �I�v�V�����̏����ɃA�^�b�`���Ďg���I�v�V�������̓N���X
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
