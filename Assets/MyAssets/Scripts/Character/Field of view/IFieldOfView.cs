using UnityEngine;

namespace MyAssets
{
    /*
     * FieldOfViewのインターフェース
     * 外部からFieldOfViewの宣言を取得したいものをここに宣言
     */
    public interface IFieldOfView
    {
        GameObject  TargetObject { get; }
        Vector3     TargetLastPoint { get; }

        void DoUpdate();
    }
}
