using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MyAssets
{
    public class SaveManager : MonoBehaviour
    {

        private string filePath;

        private void Awake()
        {
            filePath = Path.Combine(Application.persistentDataPath, "saveStatusData.json");
        }
        public void Save(List<SaveStatusData> datas)
        {
            string json = JsonUtility.ToJson(new Wrapper<SaveStatusData>(datas)); // リストをラップして保存
            File.WriteAllText(filePath, json);
        }
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

    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> items;
        public Wrapper(List<T> items) { this.items = items; }
    }
}
