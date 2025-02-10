

namespace MyAssets
{
    /*
     * 武器を装備、収納しているかフラグをまとめたインターフェース
     */
    public interface IToolInputProvider
    {
        bool WeaponEquipment { get; }

        float Equipmenting {  get; }

    }

}
