using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets._Project.Helpers
{
    public class SpriteNameComparer : IComparer<Sprite>
    {
        private readonly Regex _regex = new(@"(\d+)([a-z]?)");

        public int Compare(Sprite x, Sprite y)
        {
            Match m1 = _regex.Match(x.name);
            Match m2 = _regex.Match(y.name);
            string num1 = m1.Groups[1].Value;
            string num2 = m2.Groups[1].Value;
            if (num1.Length < num2.Length)
                return -1;
            if (num1.Length > num2.Length)
                return 1;
            int cmp = string.Compare(num1, num2);
            if (cmp != 0)
                return cmp;
            return string.Compare(m1.Groups[2].Value, m2.Groups[2].Value);
        }
    }
}
