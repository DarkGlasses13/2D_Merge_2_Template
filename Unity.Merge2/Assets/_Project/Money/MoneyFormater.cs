using System;
using UnityEngine;

namespace Assets._Project.Money
{
    public class MoneyFormater
    {
        public string Format(ulong value)
        {
            string formatedValue = "";

            if (value > 1000000000)
                formatedValue += Math.Round((decimal)value / 1000000000, 1) + "B";

            if (value > 1000000)
                formatedValue += Math.Round((decimal)value / 1000000, 1) + "M";

            if (value < 1000000)
                formatedValue += (value % 1000 >= 100
                    ? Math.Round((decimal)value / 1000, 1)
                    : Mathf.RoundToInt(value / 1000)) + "K";

            if (value < 1000000 && value < 1000)
                formatedValue = value.ToString();

            return formatedValue;

            //float m = value / 1000000;
            //int k = value % 1000000 / 1000;
            //int u = value % 1000;
            //formatedValue += m > 0 ? m + "M" : "";
            //formatedValue += k > 0 ? "." + k + "k" : "";
            //formatedValue += u > 0 ? "." + u : "";

            //return value > _config.MoneyLimit
            //    ? "OVER"
            //    : formatedValue;
        }
    }
}
