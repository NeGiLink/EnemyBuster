using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MyAssets
{
    /*
     * �X�e�[�^�X�̃f�[�^��ۑ�����N���X
     * json�ŕۑ����Ďw�肵���t�@�C���p�X(Application.persistentDataPath)
     * �ɕۑ�����
     */
    public class SaveManager : MonoBehaviour
    {

        private string filePath;

        private void Awake()
        {
            filePath = Path.Combine(Application.persistentDataPath, "saveStatusData.json");
        }
        //�X�e�[�^�X�̕ύX��ۑ�����
        public void Save(List<SaveStatusData> datas)
        {
            string json = JsonUtility.ToJson(new Wrapper<SaveStatusData>(datas)); // ���X�g�����b�v���ĕۑ�
            File.WriteAllText(filePath, json);
        }
        //�ۑ������X�e�[�^�X��ǂݍ���
        public List<SaveStatusData> LoadGame()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Wrapper<SaveStatusData> wrapper = JsonUtility.FromJson<Wrapper<SaveStatusData>>(json);
                return wrapper.items;
            }
            else
            {
                Debug.Log("Save file not found.");
                return new List<SaveStatusData>();
            }
        }

        public void MyDestory()
        {
            Destroy(this);
        }
    }
    /*
     * �X�e�[�^�X�̃f�[�^��z��ł܂Ƃ߂�N���X
     */
    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> items;
        public Wrapper(List<T> items) { this.items = items; }
    }
}
