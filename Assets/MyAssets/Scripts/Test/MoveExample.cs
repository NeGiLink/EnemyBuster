using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveExample : MonoBehaviour
{
    // Actionをインスペクターから編集できるようにする
    [SerializeField] private InputAction _action;

    // 有効化
    private void OnEnable()
    {
        // Actionのコールバックを登録
        _action.performed += OnMove;
        _action.canceled += OnMove;

        // InputActionを有効化
        // これをしないと入力を受け取れないことに注意
        _action?.Enable();
    }

    // 無効化
    private void OnDisable()
    {
        // Actionのコールバックを解除
        _action.performed -= OnMove;
        _action.canceled -= OnMove;

        // 自身が無効化されるタイミングなどで
        // Actionを無効化する必要がある
        _action?.Disable();
    }

    // コールバックを受け取ったときの処理
    private void OnMove(InputAction.CallbackContext context)
    {
        // Actionの入力値を読み込む
        var value = context.ReadValue<Vector2>();

        // 入力値をログ出力
        Debug.Log($"移動量 : {value}");
    }
}
