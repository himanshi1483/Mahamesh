using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mahamesh.Models
{
    public class FriendlyEnumMethods
    {
        public static string GetFriendlyCompEnums()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Components compEnum in Enum.GetValues(typeof(Components)))
            {
                stringBuilder.Append(compEnum.GetDescription() + "|");
            }
            return stringBuilder.ToString();
        }
    }
}