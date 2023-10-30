using Assets._Project.Local_Asset_Load;

namespace Assets._Project
{
    public class GameConfigLoader : LocalSingleAssetLoader<GameConfig>
    {
        public override object Key => "Game Config";
    }
}
