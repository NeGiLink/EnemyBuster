using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveExample : MonoBehaviour
{
    // Action���C���X�y�N�^�[����ҏW�ł���悤�ɂ���
    [SerializeField] private InputAction _action;

    // �L����
    private void OnEnable()
    {
        // Action�̃R�[���o�b�N��o�^
        _action.performed += OnMove;
        _action.canceled += OnMove;

        // InputAction��L����
        // ��������Ȃ��Ɠ��͂��󂯎��Ȃ����Ƃɒ���
        _action?.Enable();
    }

    // ������
    private void OnDisable()
    {
        // Action�̃R�[���o�b�N������
        _action.performed -= OnMove;
        _action.canceled -= OnMove;

        // ���g�������������^�C�~���O�Ȃǂ�
        // Action�𖳌�������K�v������
        _action?.Disable();
    }

    // �R�[���o�b�N���󂯎�����Ƃ��̏���
    private void OnMove(InputAction.CallbackContext context)
    {
        // Action�̓��͒l��ǂݍ���
        var value = context.ReadValue<Vector2>();

        // ���͒l�����O�o��
        Debug.Log($"�ړ��� : {value}");
    }
}
