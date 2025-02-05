

namespace MyAssets
{
    //MonoBehaviourを継承
    //ゲーム全体で必要な処理を行うクラス
    public class SystemManager
    {
        private static bool         pause;
        public static bool          IsPause => pause;


        private static float        bgmVolume = 1f;

        public static float         BGMVolume => bgmVolume;


        private static float        seVolume = 1f;

        public static float         SEVolume => seVolume;

        public static void SetPause(bool p) { pause = p; }
        public static void SetBGMVolume(float volume) {  bgmVolume = volume; }
        public static void SetSEVolume(float volume) {  seVolume = volume; }
    }
}
