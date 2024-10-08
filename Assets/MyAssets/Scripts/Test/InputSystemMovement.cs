using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemMovement : MonoBehaviour
{
    public InputAction moveAction; // Input SystemのInputActionを使用

    private Vector2 inputDirection; // 入力の方向

    void OnEnable()
    {
        // InputActionを有効にする
        moveAction.Enable();
    }

    void OnDisable()
    {
        // InputActionを無効にする
        moveAction.Disable();
    }

    void Update()
    {
        // GetAxisの代わりにInputActionから軸の値を取得
        inputDirection = moveAction.ReadValue<Vector2>();

        // 入力値をログ出力
        Debug.Log($"移動量 : {inputDirection}");

        // 入力方向に基づいて移動を処理
        //transform.Translate(inputDirection * Time.deltaTime);
    }
}
