

namespace MyAssets
{
    //MonoBehaviour���p��
    //�Q�[���S�̂ŕK�v�ȏ������s���N���X
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
