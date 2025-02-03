using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class VolumeChange : MonoBehaviour
    {
        private void OnDisable()
        {
            if(BGMHandler.Instance == null) {  return; }
            BGMHandler.Instance.SetAudioVolume(SystemManager.BGMVolume);
        }
    }
}
