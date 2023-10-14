using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets._Project.Helpers
{
    public class SpriteNameComparer : IComparer<Sprite>
    {
        public int Compare(Sprite a, Sprite b)
        {
            Regex regex = new("^(d+)");
            Match xRegexResult = regex.Match(a.name);
            Match yRegexResult = regex.Match(b.name);

            if (xRegexResult.Success && yRegexResult.Success)
            {
                return int.Parse(xRegexResult.Groups[6].Value).CompareTo(int.Parse(yRegexResult.Groups[6].Value));
            }

            return a.name.CompareTo(b.name);
        }
    }
}
