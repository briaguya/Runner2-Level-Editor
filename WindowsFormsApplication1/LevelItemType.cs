using System.ComponentModel;

namespace WindowsFormsApplication1
{
    public enum LevelItemTypeEnum
    {
        Gold,
        Avoid1,
        StartChallenge,
        EndChallenge,
        Sound
    }

    public static class LevelItemTypeEnumExtensions
    {
        public static string ToFriendlyString(this LevelItemTypeEnum me)
        {
            switch (me)
            {
                case LevelItemTypeEnum.StartChallenge:
                    return "Start Challenge";
                case LevelItemTypeEnum.EndChallenge:
                    return "End Challenge";
                case LevelItemTypeEnum.Sound:
                    return "Play Sound";
                default:
                    return me.ToString();
            }
        }
    }

    public class LevelItemType
    {
        public byte[] bytes;
        public LevelItemTypeEnum type;
    }
}
