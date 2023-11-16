using Assets._Project.Local_Asset_Load;
using UnityEngine;

namespace Assets._Project.Money
{
    public class ResourceSpriteLoader : LocalSingleAssetLoader<Sprite>
    {
        public override object Key => "Resource Sprite";
    }
}
