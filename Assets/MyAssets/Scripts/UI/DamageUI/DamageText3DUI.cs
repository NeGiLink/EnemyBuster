using UnityEngine;

namespace MyAssets
{
    /*
     * �_���[�W�������Ƀ_���[�W�̐��l��TextMesh�ŏo�͂���N���X
     */
    public class DamageText3DUI : MonoBehaviour
    {
        //������܂ł̎���
        [SerializeField]
        private float       DeleteTime = 1.0f;
        //Y�����ɓ����͈�
        [SerializeField]
        private float       MoveRange = 1.0f;
        //�����鎞�̃A���t�@�l
        [SerializeField]
        private float       EndAlpha = 0;
        //���݂̌o�ߎ��Ԃ��擾���邽�߂̕ύX
        private float       timeCnt;
        //�e�L�X�g���b�V�����擾
        private TextMesh    nowText;
        public TextMesh     NowText => nowText;

        public void Setup()
        {
            timeCnt = 0.0f;
            Destroy(gameObject, DeleteTime);
            nowText = GetComponent<TextMesh>();
        }


        private void Update()
        {
            this.transform.LookAt(Camera.main.transform);
            timeCnt += Time.deltaTime;
            gameObject.transform.localPosition += new Vector3(0, MoveRange / DeleteTime * Time.deltaTime, 0);
            gameObject.transform.Rotate(0, -180.0f, 0);
            float _alpha = 1.0f - (1.0f - EndAlpha) * (timeCnt / DeleteTime);
            if (_alpha <= 0.0f) _alpha = 0.0f;
            nowText.color = new Color(nowText.color.r, nowText.color.g, nowText.color.b, _alpha);
        }
    }
}

