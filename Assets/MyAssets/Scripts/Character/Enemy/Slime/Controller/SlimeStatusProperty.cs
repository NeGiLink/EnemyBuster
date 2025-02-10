
namespace MyAssets
{
    public enum SlimeSETag
    {
        Damage,
        Death
    }
    /*
     * スライム独自のステータス
     * スライム単体で使うステータスがないため空
     */
    [System.Serializable ]
    public class SlimeStatusProperty : BaseStautsProperty,ISlimeStauts
    {

    }
}
