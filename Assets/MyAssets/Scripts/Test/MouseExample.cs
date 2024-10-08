using UnityEngine;
using UnityEngine.InputSystem;

public class MouseExample : MonoBehaviour
{
    private void Update()
    {
        // 現在のマウス情報
        var current = Mouse.current;

        // マウス接続チェック
        if (current == null)
        {
            // マウスが接続されていないと
            // Mouse.currentがnullになる
            return;
        }

        // マウスカーソル位置取得
        var cursorPosition = current.position.ReadValue();

        // 左ボタンの入力状態取得
        var leftButton = current.leftButton;

        // 左ボタンが押された瞬間かどうか
        if (leftButton.wasPressedThisFrame)
        {
            Debug.Log($"左ボタンが押された！ {cursorPosition}");
        }

        // 左ボタンが離された瞬間かどうか
        if (leftButton.wasReleasedThisFrame)
        {
            Debug.Log($"左ボタンが離された！{cursorPosition}");
        }

        // 左ボタンが押されているかどうか
        if (leftButton.isPressed)
        {
            Debug.Log($"左ボタンが押されている！{cursorPosition}");
        }
    }
}
