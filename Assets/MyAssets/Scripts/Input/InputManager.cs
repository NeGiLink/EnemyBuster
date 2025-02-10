using UnityEngine;

namespace MyAssets
{
    //マウス・キーボード・コントローラーに関係する処理を行うクラス
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
