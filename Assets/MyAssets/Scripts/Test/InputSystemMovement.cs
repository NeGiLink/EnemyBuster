using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemMovement : MonoBehaviour
{
    public InputAction moveAction; // Input System��InputAction���g�p

    private Vector2 inputDirection; // ���͂̕���

    void OnEnable()
    {
        // InputAction��L���ɂ���
        moveAction.Enable();
    }

    void OnDisable()
    {
        // InputAction�𖳌��ɂ���
        moveAction.Disable();
    }

    void Update()
    {
        // GetAxis�̑����InputAction���玲�̒l���擾
        inputDirection = moveAction.ReadValue<Vector2>();

        // ���͒l�����O�o��
        Debug.Log($"�ړ��� : {inputDirection}");

        // ���͕����Ɋ�Â��Ĉړ�������
        //transform.Translate(inputDirection * Time.deltaTime);
    }
}
