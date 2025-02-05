using UnityEngine;

namespace MyAssets 
{
    //ダメージ発生時にテキストを出力するためのシングルトンのクラス
    //キャラクター全員が使うためシングルトン
    public class DamageTextCreator : MonoBehaviour
    {
        private static DamageTextCreator    instance;
        public static DamageTextCreator     Instance => instance;

        [SerializeField]
        private GameObject                  textMesh;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        public void Crate(Transform damager,int damage,Color color)
        {
            DamageText3DUI damageText3DUI = Instantiate(textMesh,damager.position,Quaternion.identity).GetComponent<DamageText3DUI>();
            if(damageText3DUI != null)
            {
                damageText3DUI.Setup();
                damageText3DUI.NowText.text = damage.ToString();
                damageText3DUI.NowText.color = color;
            }
        }
    }
}
