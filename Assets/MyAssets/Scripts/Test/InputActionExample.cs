using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionExample : MonoBehaviour
{
    // Action���C���X�y�N�^�[����ҏW�ł���悤�ɂ���
    [SerializeField] private InputAction _action;

    [SerializeField]
    private float value = 0;
    // �L����
    private void OnEnable()
    {
        // InputAction��L����
        // ��������Ȃ��Ɠ��͂��󂯎��Ȃ����Ƃɒ���
        _action?.Enable();
    }

    // ������
    private void OnDisable()
    {
        // ���g�������������^�C�~���O�Ȃǂ�
        // Action�𖳌�������K�v������
        _action?.Disable();
    }

    private void Update()
    {
        if (_action == null) return;

        // Action�̓��͒l��ǂݍ���
        value = _action.ReadValue<float>();

        // ���͒l�����O�o��
        Debug.Log($"Action�̓��͒l : {value}");
    }
}
