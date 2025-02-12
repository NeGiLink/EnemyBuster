using UnityEngine;

namespace MyAssets
{
    /*
     * �v���C���[���G�ɒ��ڂ�������UI��\�����邽�߂̃N���X
     */
    public class LockOnUI : MonoBehaviour
    {
        //�v���C���[�̎��E
        private FieldOfView     playerFieldOfView;
        //�J����
        [SerializeField]
        private new Camera      camera;
        //�L�����o�X
        [SerializeField]
        private Canvas          canvas;
        //�L�����o�X�̋�`�g�����X�t�H�[��
        [SerializeField]
        private RectTransform   canvasRectTransform;
        //����UI�̃g�����X�t�H�[��
        [SerializeField]
        private RectTransform   thisRectTransform;

        private void Awake()
        {
            camera = Camera.main;
            canvas = FindObjectOfType<Canvas>();
            canvasRectTransform = canvas.GetComponent<RectTransform>();
            thisRectTransform = GetComponent<RectTransform>();
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerFieldOfView = playerController.GetComponent<FieldOfView>();
        }

        public void LockUpdate()
        {
            if(playerFieldOfView.TargetObject == null) { return; }
            Vector3 target = playerFieldOfView.TargetLastPoint;
            if (camera == null) { return; }
            // 3D�I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ɕϊ�
            Vector3 screenPosition = camera.WorldToScreenPoint(target);

            // �X�N���[�����W���L�����o�X�̃��[�J�����W�ɕϊ�
            Vector2 uiPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, null, out uiPosition);

            // UI�I�u�W�F�N�g�̈ʒu��ݒ�

            thisRectTransform.localPosition = Vector2.Lerp(thisRectTransform.localPosition, uiPosition, Time.deltaTime * 100.0f);
        }
    }
}
