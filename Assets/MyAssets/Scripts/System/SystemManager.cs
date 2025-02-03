using UnityEngine;

namespace MyAssets
{
    public class SystemManager : MonoBehaviour
    {
        private static bool pause;
        public static bool IsPause => pause;
        public static void SetPause(bool p) { pause = p; }

        private static float bgmVolume = 1f;

        public static float BGMVolume => bgmVolume;

        public static void SetBGMVolume(float volume) {  bgmVolume = volume; }

        private static float seVolume = 1f;

        public static float SEVolume => seVolume;
        public static void SetSEVolume(float volume) {  seVolume = volume; }
    }
}
