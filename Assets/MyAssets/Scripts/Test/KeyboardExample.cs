using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardExample : MonoBehaviour
{
    private void Update()
    {
        // ���݂̃L�[�{�[�h���
        var current = Keyboard.current;

        // �L�[�{�[�h�ڑ��`�F�b�N
        if (current == null)
        {
            // �L�[�{�[�h���ڑ�����Ă��Ȃ���
            // Keyboard.current��null�ɂȂ�
            return;
        }

        // A�L�[�̓��͏�Ԏ擾
        var aKey = current.aKey;

        // A�L�[�������ꂽ�u�Ԃ��ǂ���
        if (aKey.wasPressedThisFrame)
        {
            Debug.Log("A�L�[�������ꂽ�I");
        }

        // A�L�[�������ꂽ�u�Ԃ��ǂ���
        if (aKey.wasReleasedThisFrame)
        {
            Debug.Log("A�L�[�������ꂽ�I");
        }

        // A�L�[��������Ă��邩�ǂ���
        if (aKey.isPressed)
        {
            Debug.Log("A�L�[��������Ă���I");
        }
    }
}
