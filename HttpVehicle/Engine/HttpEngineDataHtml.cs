using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        ///<summary>
        ///  Replace every number of whitespaces with a single space.
        ///</summary>
        public object ParseReduceWhitespaces(string s = "")
        {
            if (!Initial("ParseReduceWhitespaces()")) return null;

            if (s == "")
            {
                object o = GetS();
                //check
                s = o as string;
            }

            if (s is string == false )//&& s is string[] == false && s is IList<string> == false
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersHtml.HtmlReduceToSpace(s as string);

                if (s == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(s);
                    return s;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
    }
}
