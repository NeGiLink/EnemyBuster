namespace MyAssets
{
    /*
     * 敵の行動選択部分で使用してるクラス
     * 敵の行動をフラグ形式で決めるために使用
     */
    public class Trigger
    {
        private bool trigger;
        public bool IsTrigger => trigger;
        public void SetTrigger(bool t) {  trigger = t; }
    }
}
