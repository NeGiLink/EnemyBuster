using UnityEngine;

namespace MyAssets
{
    //�}�E�X�E�L�[�{�[�h�E�R���g���[���[�Ɋ֌W���鏈�����s���N���X
    public class InputManager
    {
        private static bool         inputStop = false;

        public static bool          InputStop => inputStop;

        private static DeviceInput  deviceInput = DeviceInput.Key;


        public static void SetInputStop(bool i) { inputStop = i; }
        public static void SetDeviceInput(DeviceInput device) { deviceInput = device; }
        public static DeviceInput GetDeviceInput() { return deviceInput; }

        public static void SetLockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public static void SetFreeCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
