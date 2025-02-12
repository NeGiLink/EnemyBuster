using UnityEngine;

namespace MyAssets
{
    /*
     * ボリュームを変更するUIが非表示になった時にボリュームを変更するクラス
     */
    public class VolumeChange : MonoBehaviour
    {
        private void OnDisable()
        {
            if(BGMHandler.Instance == null) {  return; }
            BGMHandler.Instance.SetAudioVolume(SystemManager.BGMVolume);
        }
    }
}
