using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace HttpVehicle
{
    class ParsersExtractors
    {
        public const string PatternAnyString = Validators.AnyStringPattern;
        public const string PatternInvalidUrl = Validators.InvalidUrlPattern;
        public const string PatternIp = Validators.IpPattern;
        public const string PatternIpPort = Validators.IpPortPattern;
        public const string PatternIpPortUser = Validators.IpPortUserPattern;
        public const string PatternIpPortUserPass = Validators.IpPortUserPassPattern;
        public const string PatternEmail = Validators.EmailPattern;
        public const string PatternEmailPass = Validators.EmailPassPattern;


        //Collections
        internal static object Join(object o, string delimiter = "")
        {
            if (o is string[])
            {
                return string.Join(delimiter, o as string[]);
            }
            else if (o is IList<string>)
            {
                return string.Join(delimiter, o as IList<string>);
            }

            return o;
        }
        internal static object Split(object o, string delimiter)
        {
            if (o is string)
            {
                return (o as string).Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            }

            return o;
        }
        internal static object Add(object o, object add)
        {
            if (o is object[])
            {
                List<object> li = (o as object[]).ToList();
                if (add is object)
                {
                    li.Add(add);
                }
                else if (add is object[])
                {
                    object[] lio = add as object[];
                    foreach (object obj in lio)
                    {
                        li.Add(obj);
                    }
                }
                else if (add is IList<object>)
                {
                    IList<object> lio = add as IList<object>;
                    foreach (object obj in lio)
                    {
                        li.Add(obj);
                    }
                }
                return li.ToArray();
            }
            else if (o is IList<object>)
            {
                IList<object> li = o as IList<object>;
                if (add is object)
                {
                    li.Add(add);
                }
                else if (add is object[])
                {
                    object[] lio = add as object[];
                    foreach (object obj in lio)
                    {
                        li.Add(obj);
                    }
                }
                else if (add is IList<object>)
                {
                    IList<object> lio = add as IList<object>;
                    foreach (object obj in lio)
                    {
                        li.Add(obj);
                    }
                }
                return li;
            }

            return o;
        }
        internal static object GetElement(object o, int n)
        {
            if (o is object[])
            {
                object[] cols = o as object[];
                if (cols.Count() < n) return null;
                return cols[n];
            }
            if (o is IList<object>)
            {
                IList<object> cols = o as IList<object>;
                if (cols.Count() < n) return null;
                return cols[n];
            }

            return null;
        }
        internal static object RemoveElement(object o, int n)
        {
            if (o is object[])
            {
                List<object> cols = (o as object[]).ToList();
                if (cols.Count() < n) return null;
                cols.Remove(cols[n]);
                return cols.ToArray();
            }
            if (o is IList<object>)
            {
                IList<object> cols = o as IList<object>;
                if (cols.Count() < n) return null;
                cols.Remove(cols[n]);
                return cols;
            }

            return null;
        }

        internal static object GetElementContaining(object o, string content, bool firstOnly = false)
        {
            if (firstOnly == false)
            {
                if (o is string[])
                {
                    string[] cols = o as string[];

                    List<string> output = new List<string>();
                    foreach (string s in cols)
                    {
                        if (s.Contains(content)) output.Add(s);
                    }
                    return output.ToArray();
                }
                if (o is IList<string>)
                {
                    IList<string> cols = o as IList<string>;
                    foreach (string s in cols)
                    {
                        if (!s.Contains(content)) cols.Remove(s);
                    }
                    return cols;
                }
            }
            else
            {
                if (o is string[])
                {
                    string[] cols = o as string[];
                    foreach (string s in cols)
                    {
                        if (s.Contains(content)) return s;
                    }
                    return null;
                }
                if (o is IList<string>)
                {
                    IList<string> cols = o as IList<string>;
                    foreach (string s in cols)
                    {
                        if (s.Contains(content)) return s;
                    }
                    return null;
                }
            }

            return null;
        }
        internal static object GetElementNotContaining(object o, string content, bool firstOnly = false)
        {
            if (firstOnly == false)
            {
                if (o is string[])
                {
                    string[] cols = o as string[];

                    List<string> output = new List<string>();
                    foreach (string s in cols)
                    {
                        if (!s.Contains(content)) output.Add(s);
                    }
                    return output.ToArray();
                }
                if (o is IList<string>)
                {
                    IList<string> cols = o as IList<string>;
                    foreach (string s in cols)
                    {
                        if (s.Contains(content)) cols.Remove(s);
                    }
                    return cols;
                }
            }
            else
            {
                if (o is string[])
                {
                    string[] cols = o as string[];
                    foreach (string s in cols)
                    {
                        if (!s.Contains(content)) return s;
                    }
                    return null;
                }
                if (o is IList<string>)
                {
                    IList<string> cols = o as IList<string>;
                    foreach (string s in cols)
                    {
                        if (!s.Contains(content)) return s;
                    }
                    return null;
                }
            }

            return null;
        }
        internal static object GetElementByLength(object o, int min = 0, int max = 999999, bool firstOnly = false)
        {
            if (firstOnly == false)
            {
                if (o is string[])
                {
                    string[] cols = o as string[];

                    List<string> output = new List<string>();
                    foreach (string s in cols)
                    {
                        if (s.Length >= min && s.Length <= max) output.Add(s);
                    }
                    return output.ToArray();
                }
                if (o is IList<string>)
                {
                    IList<string> cols = o as IList<string>;
                    foreach (string s in cols)
                    {
                        if (s.Length <= min && s.Length >= max) cols.Remove(s);
                    }
                    return cols;
                }
            }
            else
            {
                if (o is string[])
                {
                    string[] cols = o as string[];
                    foreach (string s in cols)
                    {
                        if (s.Length >= min && s.Length <= max) return s;
                    }
                    return null;
                }
                if (o is IList<string>)
                {
                    IList<string> cols = o as IList<string>;
                    foreach (string s in cols)
                    {
                        if (s.Length >= min && s.Length <= max) return s;
                    }
                    return null;
                }
            }

            return null;
        }

        internal static object RemoveElementContaining(object o, string content)
        {
            if (o is string[])
            {
                string[] cols = o as string[];

                List<string> output = new List<string>();
                foreach (string s in cols)
                {
                    if (!s.Contains(content)) output.Add(s);
                }
                return output.ToArray();
            }
            if (o is IList<string>)
            {
                IList<string> cols = o as IList<string>;
                foreach (string s in cols)
                {
                    if (s.Contains(content)) cols.Remove(s);
                }
                return cols;
            }

            return null;
        }
        internal static object RemoveElementNotContaining(object o, string content)
        {
            if (o is string[])
            {
                string[] cols = o as string[];

                List<string> output = new List<string>();
                foreach (string s in cols)
                {
                    if (s.Contains(content)) output.Add(s);
                }
                return output;
            }
            if (o is IList<string>)
            {
                IList<string> cols = o as IList<string>;
                foreach (string s in cols)
                {
                    if (!s.Contains(content)) cols.Remove(s);
                }
                return cols;
            }

            return null;
        }
        internal static object RemoveElementByLength(object o, int min = 0, int max = 999999)
        {
            if (o is string[])
            {
                string[] cols = o as string[];

                List<string> output = new List<string>();
                foreach (string s in cols)
                {
                    if (s.Length <= min && s.Length >= max) output.Add(s);
                }
                return output.ToArray();
            }
            if (o is IList<string>)
            {
                IList<string> cols = o as IList<string>;
                foreach (string s in cols)
                {
                    if (s.Length >= min && s.Length <= max) cols.Remove(s);
                }
                return cols;
            }

            return null;
        }



        //Append
        internal static object Append(object o, string append, bool preAppend = true)
        {
            if (o is string)
            {
                if (preAppend) return append + (o as string); else return (o as string) + append;
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string result = (preAppend) ? append + str : str + append;
                    results.Add(result);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    results[i] = (preAppend) ? append + results[i] : results[i] + append;
                }
                return results;
            }

            return null;
        }
        internal static object ParseAsPostField(object o, string fieldName)
        {
            if (o is string)
            {
                return fieldName + '=' + UrlEncode(o as string);
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string result = fieldName + '=' + UrlEncode(str);
                    results.Add(result);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    results[i] = fieldName + '=' + UrlEncode(results[i]);
                }
                return results;
            }

            return null;
        }


        //Get
        internal static string ExtractCookieValue(CookieCollection allCookies, string name)
        {
            foreach (Cookie ck in allCookies)
            {
                if (ck.Name == name)
                {
                    return ck.Value;
                }
            }
            return null;
        }
        internal static CookieCollection ExtractAllCookies(CookieContainer container)
        {
            var allCookies = new CookieCollection();


            Hashtable table = (Hashtable)container.GetType().InvokeMember("m_domainTable",
                                                                         BindingFlags.NonPublic |
                                                                         BindingFlags.GetField |
                                                                         BindingFlags.Instance,
                                                                         null,
                                                                         container,
                                                                         new object[] { });

            foreach (var key in table.Keys)
            {
                string thekey = key.ToString()[0] == '.' 
                    ? key.ToString().Substring(1) 
                    : key.ToString();
                foreach (Cookie cookie in container.GetCookies(new Uri(string.Format("http://{0}/", thekey))))
                {
                    allCookies.Add(cookie);
                }
            }
            return allCookies;
        }
        internal static string[] ExtractAllCookies(CookieCollection collection)
        {
            List<string> otp = new List<string>();
            foreach (Cookie c in collection)
            {
                string s = "";
                s += (s == "") ? "Domain : " + c.Domain : Environment.NewLine + "Domain : " + c.Domain;
                s += (s == "") ? "Expires : " + c.Expires.ToLongTimeString() : Environment.NewLine + "Expires : " + c.Expires.ToLongTimeString();
                s += (s == "") ? "Name : " + c.Name : Environment.NewLine + "Name : " + c.Name;
                s += (s == "") ? "Path : " + c.Path : Environment.NewLine + "Path : " + c.Path;
                s += (s == "") ? "Secure : " + c.Secure : Environment.NewLine + "Secure : " + c.Secure;

                s += Environment.NewLine + "Value : " + c.Value;
                otp.Add(s);
            }
            return otp.ToArray();
        }
        internal static string ExtractProxy(WebProxy prox)
        {
            return (prox.Credentials == null) ?
                    string.Format("{0}:{1}", prox.Address.Host.ToString(), prox.Address.Port.ToString()) :
                    string.Format("{0}:{1}:{2}", prox.Address.Host.ToString(), prox.Address.Port.ToString(), prox.Credentials.ToString());
        }

        internal static object ExtractRegex(object o, string regex, bool firstOnly = false)
        {
            Regex Rx = new Regex(regex, RegexOptions.IgnoreCase);

            if (firstOnly == false)
            {
                if (o is string)
                {
                    string s = o as string;
                    List<string> results = new List<string>();
                    foreach (Match match in Rx.Matches(s))
                    {
                        results.Add(match.ToString());
                    }
                    if (results.Count() == 1) return results[0];
                    return results.ToArray();
                }
                else if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];

                    foreach (string str in s)
                    {
                        foreach (Match match in Rx.Matches(str))
                        {
                            results.Add(match.ToString());
                        }
                    }
                    if (results.Count() == 1) return results[0];
                    return results.ToArray();
                }
                else if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        foreach (Match match in Rx.Matches(results[i]))
                        {
                            results.Add(match.ToString());
                        }
                    }
                    return results;
                }
            }
            else
            {
                if (o is string)
                {
                    Match match = Rx.Match(o as string);
                    return match.ToString();
                }
                if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];
                    foreach (string str in s)
                    {
                        Match match = Rx.Match(o as string);
                        results.Add(match.ToString());
                    }
                    return results.ToArray();
                }
                if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        Match match = Rx.Match(o as string);
                        results[i] = match.ToString();
                    }
                    return results;
                }
            }

            return null;
        }
        internal static object ExtractRegexInverse(object o, string regex, string before = "", string after = "")
        {
            if (o is string)
            {
                if (!string.IsNullOrEmpty(before) && !string.IsNullOrEmpty(after))
                {
                    string s = o as string;
                    string[] sep = ExtractDelimited(o, before, after) as string[];
                    Regex Rx = new Regex(regex, RegexOptions.IgnoreCase);

                    List<string> results = new List<string>();
                    for (int i = 1; i < sep.Count(); i++)
                    {
                        int length = 0;
                        for (int j = 0; j < sep[i].Length; j++)
                        {
                            if (Rx.IsMatch(sep[i][j].ToString())) { break; }
                            length++;
                        }
                        results.Add(sep[i].Substring(0, length));
                    }
                    if (results.Count() == 1) return results[0];
                    return results.ToArray();
                }
                else if (!string.IsNullOrEmpty(after))
                {
                    string s = o as string;
                    string[] sep = s.Split(new string[] { after }, StringSplitOptions.None);
                    Regex Rx = new Regex(regex, RegexOptions.IgnoreCase);

                    List<string> results = new List<string>();
                    for (int i = 0; i < sep.Count() - 1; i++)
                    {
                        int length = 0;
                        for (int j = sep[i].Length - 1; j >= 0; j--)
                        {
                            if (Rx.IsMatch(sep[i][j].ToString())) { break; }
                            length++;
                        }
                        results.Add(sep[i].Substring(sep[i].Length - length));
                    }
                    if (results.Count() == 1) return results[0];
                    return results.ToArray();
                }
                else if (!string.IsNullOrEmpty(before))
                {
                    string s = o as string;
                    string[] sep = s.Split(new string[] { before }, StringSplitOptions.None);
                    Regex Rx = new Regex(regex, RegexOptions.IgnoreCase);

                    List<string> results = new List<string>();
                    for (int i = 1; i < sep.Count(); i++)
                    {
                        int length = 0;
                        for (int j = 0; j < sep[i].Length; j++)
                        {
                            if (Rx.IsMatch(sep[i][j].ToString())) { break; }
                            length++;
                        }
                        results.Add(sep[i].Substring(0, length));
                    }
                    if (results.Count() == 1) return results[0];
                    return results.ToArray();
                }
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];

                foreach (string str in s)
                {
                    string[] res = ExtractRegexInverse(str, regex, before, after) as string[];
                    foreach (string r in res)
                    {
                        results.Add(r);
                    }
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    string[] res = ExtractRegexInverse(results[i], regex, before, after) as string[];
                    foreach (string r in res)
                    {
                        results.Add(r);
                    }
                }
                return results;
            }

            return null;
        }
        internal static object ExtractDelimited(object o, string before, string after, bool firstOnly = false)
        {
            if (firstOnly == false)
            {
                Regex Rx = new Regex(Regex.Escape(before) + "(.*?)" + Regex.Escape(after), RegexOptions.Singleline);

                if (o is string)
                {
                    string s = o as string;
                    List<string> results = new List<string>();
                    foreach (Match match in Rx.Matches(s))
                    {
                        results.Add(match.Groups[1].Value);
                    }
                    if (results.Count() == 1) return results[0];
                    return results.ToArray();
                }
                else if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];

                    foreach (string str in s)
                    {
                        foreach (Match match in Rx.Matches(str))
                        {
                            results.Add(match.Groups[1].Value);
                        }
                    }
                    if (results.Count() == 1) return results[0];
                    return results.ToArray();
                }
                else if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        foreach (Match match in Rx.Matches(results[i]))
                        {
                            results.Add(match.Groups[1].Value);
                        }
                    }
                    return results;
                }
            }
            else
            {
                if (o is string)
                {
                    string s = (o as string);
                    int pos = s.IndexOf(before);
                    if (pos > -1) s = s.Substring(pos + before.Length);
                    s = s.Split(new string[] { after }, StringSplitOptions.None)[0];

                    return s;
                }
                else if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];
                    foreach (string str in s)
                    {
                        string catched = (o as string);
                        int pos = catched.IndexOf(before);
                        if (pos > -1) catched = catched.Substring(pos + before.Length);
                        catched = catched.Split(new string[] { after }, StringSplitOptions.None)[0];
                        results.Add(catched);
                    }
                    return results.ToArray();
                }
                else if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        string catched = (o as string);
                        int pos = catched.IndexOf(before);
                        if (pos > -1) catched = catched.Substring(pos + before.Length);
                        catched = catched.Split(new string[] { after }, StringSplitOptions.None)[0];
                        results[i] = catched;
                    }
                    return results;
                }
            }

            return null;
        }


        //Replace/Remove
        internal static object Replace(object o, string oldstr, string newstr = "", bool firstOnly = false)
        {
            if (firstOnly == false)
            {
                if (o is string)
                {
                    return (o as string).Replace(oldstr, newstr);
                }
                else if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];
                    foreach (string str in s)
                    {
                        string result = str.Replace(oldstr, newstr);
                        results.Add(result);
                    }
                    return results.ToArray();
                }
                else if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        results[i] = results[i].Replace(oldstr, newstr);
                    }
                    return results;
                }
            }
            else
            {
                if (o is string)
                {
                    int pos = (o as string).IndexOf(oldstr);
                    if (pos < 0) { return o; }
                    return (o as string).Substring(0, pos) + (o as string).Substring(pos + oldstr.Length);
                }
                else if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];
                    foreach (string str in s)
                    {
                        int pos = str.IndexOf(oldstr);
                        string result = (pos < 0) ? str : str.Substring(0, pos) + str.Substring(pos + oldstr.Length);
                        results.Add(result);
                    }
                    return results.ToArray();
                }
                else if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        int pos = results[i].IndexOf(oldstr);
                        results[i] = (pos < 0) ? results[i] : results[i].Substring(0, pos) + results[i].Substring(pos + oldstr.Length);
                    }
                    return results;
                }

            }

            return null;
        }
        internal static object ReplaceDelimited(object o, string before, string after, string newstr = "", bool firstOnly = false)
        {
            if (firstOnly == false)
            {
                Regex Rx = new Regex(before + "(.*?)" + after);

                if (o is string)
                {
                    string s = o as string;
                    foreach (Match match in Rx.Matches(s))
                    {
                        string matched = match.ToString();
                        s.Replace(matched, before + newstr + after);
                    }
                    return s;
                }
                else if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];
                    foreach (string str in s)
                    {
                        string result = str;
                        foreach (Match match in Rx.Matches(str))
                        {
                            string matched = match.ToString();
                            result.Replace(matched, before + newstr + after);
                        }
                        results.Add(result);
                    }
                    return results.ToArray();
                }
                else if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        foreach (Match match in Rx.Matches(results[i]))
                        {
                            string matched = match.ToString();
                            results[i].Replace(matched, before + newstr + after);
                        }
                    }
                    return results;
                }
            }
            else
            {
                if (o is string)
                {
                    string oldstr = (o as string);
                    int pos0 = oldstr.IndexOf(before);
                    if (pos0 > -1) oldstr = oldstr.Substring(pos0 + before.Length);
                    oldstr = oldstr.Split(new string[] { after }, StringSplitOptions.None)[0];

                    int pos = (o as string).IndexOf(oldstr);
                    if (pos < 0) { return o; }
                    return (o as string).Substring(0, pos) + (o as string).Substring(pos + oldstr.Length);
                }
                else if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];
                    foreach (string str in s)
                    {
                        string oldstr = (o as string);
                        int pos0 = oldstr.IndexOf(before);
                        if (pos0 > -1) oldstr = oldstr.Substring(pos0 + before.Length);
                        oldstr = oldstr.Split(new string[] { after }, StringSplitOptions.None)[0];

                        int pos = str.IndexOf(oldstr);
                        string result = (pos < 0) ? str : str.Substring(0, pos) + str.Substring(pos + oldstr.Length);
                        results.Add(result);
                    }
                    return results.ToArray();
                }
                else if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        string oldstr = (o as string);
                        int pos0 = oldstr.IndexOf(before);
                        if (pos0 > -1) oldstr = oldstr.Substring(pos0 + before.Length);
                        oldstr = oldstr.Split(new string[] { after }, StringSplitOptions.None)[0];

                        int pos = results[i].IndexOf(oldstr);
                        results[i] = (pos < 0) ? results[i] : results[i].Substring(0, pos) + results[i].Substring(pos + oldstr.Length);
                    }
                    return results;
                }

            }

            return null;
        }
        internal static object ReplaceRegex(object o, string regex, string newstr = "", bool firstOnly = false)
        {
            Regex Rx = new Regex(regex, RegexOptions.IgnoreCase);

            if (firstOnly == false)
            {
                if (o is string)
                {
                    string s = o as string;
                    foreach (Match match in Rx.Matches(s))
                    {
                        string matched = match.ToString();
                        s.Replace(matched, newstr);
                    }
                }
                else if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];

                    foreach (string str in s)
                    {
                        foreach (Match match in Rx.Matches(str))
                        {
                            string matched = match.ToString();
                            str.Replace(matched, newstr);
                        }
                        results.Add(str);
                    }
                    return results.ToArray();
                }
                else if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        foreach (Match match in Rx.Matches(results[i]))
                        {
                            string matched = match.ToString();
                            results[i].Replace(matched, newstr);
                        }
                    }
                    return results;
                }
            }
            else
            {
                if (o is string)
                {
                    Match match = Rx.Match(o as string);

                    int pos = (o as string).IndexOf(match.ToString());
                    if (pos < 0) { return o; }
                    return (o as string).Substring(0, pos) + (o as string).Substring(pos + match.ToString().Length);
                }
                else if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];
                    foreach (string str in s)
                    {
                        Match match = Rx.Match(str);

                        int pos = str.IndexOf(match.ToString());
                        string result = (pos < 0) ? str : str.Substring(0, pos) + str.Substring(pos + match.ToString().Length);
                        results.Add(result);
                    }
                    return results.ToArray();
                }
                else if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        Match match = Rx.Match(results[i]);
                        int pos = results[i].IndexOf(match.ToString());
                        results[i] = (pos < 0) ? results[i] : results[i].Substring(0, pos) + results[i].Substring(pos + match.ToString().Length);
                    }
                    return results;
                }
            }

            return null;
        }
        internal static object ReplaceUrlQuery(object o, string newquery = "")
        {
            if (o is string)
            {
                string url = o as string;
                if (url.Contains('?'))
                {
                    url = url.Split('?')[0] + newquery;
                }
                return url;
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string result = str.Split('?')[0] + newquery;
                    results.Add(result);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    results[i] = results[i].Split('?')[0] + newquery;
                }
                return results;
            }
            return null;

        }


        //Parse
        internal static object UrlEncode(object o)
        {
            if (o is string)
            {
                return HttpUtility.UrlEncode((o as string));
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string result = HttpUtility.UrlEncode(str);
                    results.Add(result);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    results[i] = HttpUtility.UrlEncode(results[i]);
                }
                return results;
            }

            return null;
        }
        internal static object UrlDecode(object o)
        {
            if (o is string)
            {
                return HttpUtility.UrlDecode((o as string));
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string result = HttpUtility.UrlDecode(str);
                    results.Add(result);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    results[i] = HttpUtility.UrlDecode(results[i]);
                }
                return results;
            }

            return null;
        }
        internal static object BGtoLatin(object o)
        {
                if (o is string)
                {
                    string otp = "";
                    string s = o as string;
                    for (int i = 0; i < s.Count(); i++)
                    {
                        switch (s[i])
                        {
                            case 'А': otp += 'A'; break;
                            case 'а': otp += 'a'; break;
                            case 'Б': otp += 'B'; break;
                            case 'б': otp += 'b'; break;
                            case 'В': otp += 'V'; break;
                            case 'в': otp += 'v'; break;
                            case 'Г': otp += 'G'; break;
                            case 'г': otp += 'g'; break;
                            case 'Д': otp += 'D'; break;
                            case 'д': otp += 'd'; break;
                            case 'Е': otp += 'E'; break;
                            case 'е': otp += 'e'; break;
                            case 'Ж': otp += 'J'; break;
                            case 'ж': otp += 'j'; break;
                            case 'З': otp += 'Z'; break;
                            case 'з': otp += 'z'; break;
                            case 'И': otp += 'I'; break;
                            case 'и': otp += 'i'; break;
                            case 'Й': otp += 'I'; break;
                            case 'й': otp += 'i'; break;
                            case 'К': otp += 'K'; break;
                            case 'к': otp += 'k'; break;
                            case 'Л': otp += 'L'; break;
                            case 'л': otp += 'l'; break;
                            case 'М': otp += 'M'; break;
                            case 'м': otp += 'm'; break;
                            case 'Н': otp += 'N'; break;
                            case 'н': otp += 'n'; break;
                            case 'О': otp += 'O'; break;
                            case 'о': otp += 'o'; break;
                            case 'П': otp += 'P'; break;
                            case 'п': otp += 'p'; break;
                            case 'Р': otp += 'R'; break;
                            case 'р': otp += 'r'; break;
                            case 'С': otp += 'S'; break;
                            case 'с': otp += 's'; break;
                            case 'Т': otp += 'T'; break;
                            case 'т': otp += 't'; break;
                            case 'У': otp += 'U'; break;
                            case 'у': otp += 'u'; break;
                            case 'Ф': otp += 'F'; break;
                            case 'ф': otp += 'f'; break;
                            case 'Х': otp += 'H'; break;
                            case 'х': otp += 'h'; break;
                            case 'Ц': otp += "Ts"; break;
                            case 'ц': otp += "ts"; break;
                            case 'Ч': otp += "Ch"; break;
                            case 'ч': otp += "ch"; break;
                            case 'Ш': otp += "Sh"; break;
                            case 'ш': otp += "sh"; break;
                            case 'Щ': otp += "Sht"; break;
                            case 'щ': otp += "sht"; break;
                            case 'Ъ': otp += 'A'; break;
                            case 'ъ': otp += 'a'; break;
                            case 'Ь': otp += 'I'; break;
                            case 'ь': otp += 'i'; break;
                            case 'Ю': otp += "Iu"; break;
                            case 'ю': otp += "iu"; break;
                            case 'Я': otp += "Ia"; break;
                            case 'я': otp += "ia"; break;
                            default: otp += s[i]; break;
                        }
                    }
                    return otp;
                }
                else if (o is string[])
                {
                    List<string> results = new List<string>();
                    string[] s = o as string[];
                    foreach (string str in s)
                    {
                        string result = BGtoLatin(str) as string;
                        results.Add(result);
                    }
                    return results.ToArray();
                }
                else if (o is IList<string>)
                {
                    IList<string> results = o as IList<string>;
                    for (int i = 0; i < results.Count(); i++)
                    {
                        results[i] = BGtoLatin(results[i]) as string;
                    }
                    return results;
                }

                return null;
        }
        internal static object RUtoLatin(object o)
        {
            if (o is string)
            {
                string otp = "";
                string s = o as string;
                for (int i = 0; i < s.Count(); i++)
                {
                    switch (s[i])
                    {
                        case 'А': otp += 'A'; break;
                        case 'а': otp += 'a'; break;
                        case 'Б': otp += 'B'; break;
                        case 'б': otp += 'b'; break;
                        case 'В': otp += 'V'; break;
                        case 'в': otp += 'v'; break;
                        case 'Г': otp += 'G'; break;
                        case 'г': otp += 'g'; break;
                        case 'Д': otp += 'D'; break;
                        case 'д': otp += 'd'; break;
                        case 'E': otp += (i == 0 || s[i - 1] == 'Ь' || s[i - 1] == 'ь' || s[i - 1] == 'Ъ' || s[i - 1] == 'ъ') ? "Ye" : "E"; break;
                        case 'е': otp += (i == 0 || s[i - 1] == 'Ь' || s[i - 1] == 'ь' || s[i - 1] == 'Ъ' || s[i - 1] == 'ъ') ? "ye" : "e"; break;
                        case 'Ё': otp += "Yo"; break;
                        case 'ё': otp += "yo"; break;
                        case 'Ж': otp += "Zh"; break;
                        case 'ж': otp += "zh"; break;
                        case 'З': otp += 'Z'; break;
                        case 'з': otp += 'z'; break;
                        case 'И': otp += 'I'; break;
                        case 'и': otp += 'i'; break;
                        case 'Й': otp += 'I'; break;
                        case 'й': otp += 'i'; break;
                        case 'К': otp += 'K'; break;
                        case 'к': otp += 'k'; break;
                        case 'Л': otp += 'L'; break;
                        case 'л': otp += 'l'; break;
                        case 'М': otp += 'M'; break;
                        case 'м': otp += 'm'; break;
                        case 'Н': otp += 'N'; break;
                        case 'н': otp += 'n'; break;
                        case 'О': otp += 'O'; break;
                        case 'о': otp += 'o'; break;
                        case 'П': otp += 'P'; break;
                        case 'п': otp += 'p'; break;
                        case 'Р': otp += 'R'; break;
                        case 'р': otp += 'r'; break;
                        case 'С': otp += 'S'; break;
                        case 'с': otp += 's'; break;
                        case 'Т': otp += 'T'; break;
                        case 'т': otp += 't'; break;
                        case 'У': otp += 'U'; break;
                        case 'у': otp += 'u'; break;
                        case 'Ф': otp += 'F'; break;
                        case 'ф': otp += 'f'; break;
                        case 'Х': otp += "Kh"; break;
                        case 'х': otp += "kh"; break;
                        case 'Ц': otp += "Ts"; break;
                        case 'ц': otp += "ts"; break;
                        case 'Ч': otp += "Ch"; break;
                        case 'ч': otp += "ch"; break;
                        case 'Ш': otp += "Sh"; break;
                        case 'ш': otp += "sh"; break;
                        case 'Щ': otp += "Shch"; break;
                        case 'щ': otp += "shch"; break;
                        case 'Ъ': otp += (i > 0 && (
                                  s[i - 1] == 'А' || s[i - 1] == 'а' ||
                                  s[i - 1] == 'И' || s[i - 1] == 'и' ||
                                  s[i - 1] == 'О' || s[i - 1] == 'о' ||
                                  s[i - 1] == 'У' || s[i - 1] == 'у' ||
                                  s[i - 1] == 'Ы' || s[i - 1] == 'ы' ||
                                  s[i - 1] == 'З' || s[i - 1] == 'э')) ? "Y" : ""; break;
                        case 'ъ':
                            otp += (i > 0 && (
                                  s[i - 1] == 'А' || s[i - 1] == 'а' ||
                                  s[i - 1] == 'И' || s[i - 1] == 'и' ||
                                  s[i - 1] == 'О' || s[i - 1] == 'о' ||
                                  s[i - 1] == 'У' || s[i - 1] == 'у' ||
                                  s[i - 1] == 'Ы' || s[i - 1] == 'ы' ||
                                  s[i - 1] == 'З' || s[i - 1] == 'э')) ? "y" : ""; break;
                        case 'Ы': otp += 'Y'; break;
                        case 'ы': otp += 'y'; break;
                        case 'Ь':
                            otp += (i > 0 && (
                                  s[i - 1] == 'А' || s[i - 1] == 'а' ||
                                  s[i - 1] == 'И' || s[i - 1] == 'и' ||
                                  s[i - 1] == 'О' || s[i - 1] == 'о' ||
                                  s[i - 1] == 'У' || s[i - 1] == 'у' ||
                                  s[i - 1] == 'Ы' || s[i - 1] == 'ы' ||
                                  s[i - 1] == 'З' || s[i - 1] == 'э')) ? "Y" : ""; break;
                        case 'ь':
                            otp += (i > 0 && (
                                  s[i - 1] == 'А' || s[i - 1] == 'а' ||
                                  s[i - 1] == 'И' || s[i - 1] == 'и' ||
                                  s[i - 1] == 'О' || s[i - 1] == 'о' ||
                                  s[i - 1] == 'У' || s[i - 1] == 'у' ||
                                  s[i - 1] == 'Ы' || s[i - 1] == 'ы' ||
                                  s[i - 1] == 'З' || s[i - 1] == 'э')) ? "Y" : ""; break;
                        case 'Ю': otp += "Yu"; break;
                        case 'ю': otp += "yu"; break;
                        case 'Я': otp += "Ya"; break;
                        case 'я': otp += "ya"; break;
                        default: otp += s[i]; break;
                    }
                }
                return otp;
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string result = RUtoLatin(str) as string;
                    results.Add(result);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    results[i] = RUtoLatin(results[i]) as string;
                }
                return results;
            }

            return null;
        }
        internal static object ToLower(object o)
        {
            if (o is string)
            {
                return (o as string).ToLower();
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string result = str.ToLower();
                    results.Add(result);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    results[i] = results[i].ToLower();
                }
                return results;
            }

            return null;
        }
        internal static object ToUpper(object o)
        {
            if (o is string)
            {
                return (o as string).ToUpper();
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string result = str.ToUpper();
                    results.Add(result);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    results[i] = results[i].ToUpper();
                }
                return results;
            }

            return null;
        }
        internal static object SwitchCases(object o)
        {
            if (o is string)
            {
                string s = o as string;
                string otp = "";
                foreach (char c in s)
                {
                    if (char.IsUpper(c)) otp += char.ToLower(c);
                    else if (char.IsLower(c)) otp += char.ToUpper(c);
                    else otp += c;
                }

                return otp;
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string otp = "";
                    foreach (char c in str)
                    {
                        if (char.IsUpper(c)) otp += char.ToLower(c);
                        else if (char.IsLower(c)) otp += char.ToUpper(c);
                        else otp += c;
                    }
                    results.Add(otp);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    string otp = "";
                    foreach (char c in results[i])
                    {
                        if (char.IsUpper(c)) otp += char.ToLower(c);
                        else if (char.IsLower(c)) otp += char.ToUpper(c);
                        else otp += c;
                    }

                    results[i] = otp;
                }
                return results;
            }

            return null;
        }
        internal static object Passwordize(object o)
        {
            Random rand = new Random();
            if (o is string)
            {
                string s = o as string;
                string otp = "";
                foreach (char c in s)
                {
                    if (char.ToLower(c) == 'a') otp += '@';
                    else if (char.ToLower(c) == 'c') otp += '(';
                    else if (char.ToLower(c) == 'e') otp += '#';
                    else if (char.ToLower(c) == 'i') otp += '!';
                    else if (char.ToLower(c) == 'o') otp += '0';
                    else if (char.ToLower(c) == 's') otp += '5';
                    else if (char.ToLower(c) == 't') otp += '7';
                    else if (char.ToLower(c) == 'z') otp += '2';
                    else if (char.IsLower(c)) otp += rand.Next(0, 5) == 1 ? char.ToUpper(c) : c;
                    else otp += c;
                }

                return otp;
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string otp = "";
                    foreach (char c in str)
                    {
                        if (char.ToLower(c) == 'a') otp += '@';
                        else if (char.ToLower(c) == 'c') otp += '(';
                        else if (char.ToLower(c) == 'e') otp += '#';
                        else if (char.ToLower(c) == 'i') otp += '!';
                        else if (char.ToLower(c) == 'o') otp += '0';
                        else if (char.ToLower(c) == 's') otp += '5';
                        else if (char.ToLower(c) == 't') otp += '7';
                        else if (char.ToLower(c) == 'z') otp += '2';
                        else if (char.IsLower(c)) otp += rand.Next(0, 5) == 1 ? char.ToUpper(c) : c;
                        else otp += c;
                    }
                    results.Add(otp);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    string otp = "";
                    foreach (char c in results[i])
                    {
                        if (char.ToLower(c) == 'a') otp += '@';
                        else if (char.ToLower(c) == 'c') otp += '(';
                        else if (char.ToLower(c) == 'e') otp += '#';
                        else if (char.ToLower(c) == 'i') otp += '!';
                        else if (char.ToLower(c) == 'o') otp += '0';
                        else if (char.ToLower(c) == 's') otp += '5';
                        else if (char.ToLower(c) == 't') otp += '7';
                        else if (char.ToLower(c) == 'z') otp += '2';
                        else if (char.IsLower(c)) otp += rand.Next(0, 5) == 1 ? char.ToUpper(c) : c;
                        else otp += c;
                    }

                    results[i] = otp;
                }
                return results;
            }

            return null;
        }
        internal static object CleanFileName(object o)
        {
            if (o is string)
            {
                string s = o as string;
                string otp = "";
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.ToLower(s[i]) != '#' &&
                        char.ToLower(s[i]) != '%' &&
                        char.ToLower(s[i]) != '&' &&
                        char.ToLower(s[i]) != '{' &&
                        char.ToLower(s[i]) != '}' &&
                        char.ToLower(s[i]) != '\\' &&
                        char.ToLower(s[i]) != '/' &&
                        char.ToLower(s[i]) != '<' &&
                        char.ToLower(s[i]) != '>' &&
                        char.ToLower(s[i]) != '^' &&
                        char.ToLower(s[i]) != '*' &&
                        char.ToLower(s[i]) != '?' &&
                        char.ToLower(s[i]) != '$' &&
                        char.ToLower(s[i]) != '!' &&
                        char.ToLower(s[i]) != '\'' &&
                        char.ToLower(s[i]) != '"' &&
                        char.ToLower(s[i]) != ':' &&
                        char.ToLower(s[i]) != '@' &&
                        char.ToLower(s[i]) != '+' &&
                        char.ToLower(s[i]) != '`' &&
                        char.ToLower(s[i]) != '=' &&
                        char.ToLower(s[i]) != '|' &&
                        (i > 0 || char.ToLower(s[i]) != ' ') &&
                        (i > 0 || char.ToLower(s[i]) != '.') &&
                        (i > 0 || char.ToLower(s[i]) != '-') &&
                        (i > 0 || char.ToLower(s[i]) != '_'))
                        otp += s[i];
                }
                return otp;
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string otp = CleanFileName(str) as string;
                    results.Add(otp);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    string otp = CleanFileName(results[i]) as string;
                    results[i] = otp;
                }
                return results;
            }

            return null;
        }
        internal static object GetHost(object o)
        {
            if (o is string)
            {
                string url = o as string;
                Uri myUri = new Uri(url);
                string host = myUri.Host;
                return host;
            }
            else if (o is string[])
            {
                List<string> results = new List<string>();
                string[] s = o as string[];
                foreach (string str in s)
                {
                    string otp = GetHost(str) as string;
                    results.Add(otp);
                }
                return results.ToArray();
            }
            else if (o is IList<string>)
            {
                IList<string> results = o as IList<string>;
                for (int i = 0; i < results.Count(); i++)
                {
                    string otp = GetHost(results[i]) as string;
                    results[i] = otp;
                }
                return results;
            }

            return null;
        }
    }
}