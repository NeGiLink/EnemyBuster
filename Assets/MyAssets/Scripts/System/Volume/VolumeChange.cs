using UnityEngine;

namespace MyAssets
{
    /*
     * �{�����[����ύX����UI����\���ɂȂ������Ƀ{�����[����ύX����N���X
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
