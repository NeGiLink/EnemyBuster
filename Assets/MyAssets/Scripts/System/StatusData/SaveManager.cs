using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MyAssets
{
    /*
     * ステータスのデータを保存するクラス
     * jsonで保存して指定したファイルパス(Application.persistentDataPath)
     * に保存する
     */
    public class SaveManager : MonoBehaviour
    {

        private string filePath;

        private void Awake()
        {
            filePath = Path.Combine(Application.persistentDataPath, "saveStatusData.json");
        }
        //ステータスの変更を保存する
        public void Save(List<SaveStatusData> datas)
        {
            string json = JsonUtility.ToJson(new Wrapper<SaveStatusData>(datas)); // リストをラップして保存
            File.WriteAllText(filePath, json);
        }
        //保存したステータスを読み込む
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
     * ステータスのデータを配列でまとめるクラス
     */
    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> items;
        public Wrapper(List<T> items) { this.items = items; }
    }
}
