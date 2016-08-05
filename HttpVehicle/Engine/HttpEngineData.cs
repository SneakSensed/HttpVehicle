using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        ///<summary>
        /// Regex pattern for anything (lazy).
        ///</summary>
        public string patternAnyString
        {
            get { return Validators.AnyStringPattern; }
        }
        ///<summary>
        /// Regex pattern for invalid character in url.
        ///</summary>
        public string patternInvalidUrl
        {
            get { return Validators.InvalidUrlPattern; }
        }
        ///<summary>
        /// Regex pattern for Ip address.
        ///</summary>
        public string patternIp
        {
            get { return Validators.IpPattern; }
        }
        ///<summary>
        /// Regex pattern for Ip:port.
        ///</summary>
        public string patternIpPort
        {
            get { return Validators.IpPortPattern; }
        }
        ///<summary>
        /// Regex pattern for Ip:port:user.
        ///</summary>
        public string patternIpPortUser
        {
            get { return Validators.IpPortUserPattern; }
        }
        ///<summary>
        /// Regex pattern for Ip:port:user:pass.
        ///</summary>
        public string patternIpPortUserPass
        {
            get { return Validators.IpPortUserPassPattern; }
        }
        ///<summary>
        /// Regex pattern for email.
        ///</summary>
        public string patternEmail
        {
            get { return Validators.EmailPattern; }
        }
        ///<summary>
        /// Regex pattern for email:pass.
        ///</summary>
        public string patternEmailPass
        {
            get { return Validators.EmailPassPattern; }
        }


        ///<summary>
        /// Extract cookie value.
        ///</summary>
        ///<param name="name">
        /// The name of the cookie.
        ///</param>
        public string ExtractCookieValue(string name)
        {
            if (!Initial("ExtractCookieValue()")) return null;
            bool hasCookie = false;
            CookieCollection col = Cookies;
            foreach (Cookie ck in col)
            {
                if (ck.Name == name)
                {
                    hasCookie = true;
                    break;
                }
            }
            if (!hasCookie)
            {
                Fault("Cookie with the given name not found!");
                return null;
            }
            try
            {
                string s = ParsersExtractors.ExtractCookieValue(col, name);

                AddS(s);
                Success("Stored : " + s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Extract all cookes as array of strings.
        ///</summary>
        public string[] ExtractAllCookies()
        {
            if (!Initial("ExtractAllCookies()")) return null;
            CookieCollection col = Cookies;
            if (col.Count < 1)
            {
                Fault("There are no cookies set!");
                return null;
            }
            try
            {
                string[] s = ParsersExtractors.ExtractAllCookies(col);

                AddS(s);
                Success();
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Extract current engine proxy server as string.
        ///</summary>
        public string ExtractProxyValue()
        {
            if (!Initial("ExtractProxyValue()")) return null;

            WebProxy p = Proxy;
            if (p == null)
            {
                Fault("Proxy server not set!");
                return null;
            }
            try
            {
                string s = ParsersExtractors.ExtractProxy(p);
                AddS(s);
                Success(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Extract UserAgent string.
        ///</summary>
        public string ExtractUserAgent()
        {
            if (!Initial("ExtractUserAgent()")) return null;
            if (UserAgent == null)
            {
                Fault("UserAgent not set!");
                return null;
            }
            try
            {
                string u = UserAgent;
                AddS(u);
                Success(u);
                return u;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get item from persistent storage.
        ///</summary>
        ///<param name="key">
        /// The key that corresponds to the persisted object.
        ///</param>
        public object GetPersisted(string key)
        {
            if (!Initial("ExtractPersistent()")) return null;

            try
            {
                object o = GetP(key);
                AddS(o);
                Success();
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Add item to persistent storage.
        ///</summary>
        ///<param name="key">
        /// The key that will be assigned.
        ///</param>
        ///<param name="o">
        /// The object to persist. If not set last stored will be used.
        ///</param>
        public void Persist(string key, object o = null)
        {
            if (!Initial("ParseAddPersistent()")) return;
            try
            {
                AddP(key, o);
                Success();
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Add object to collection.
        ///</summary>
        ///<param name="add">
        /// The object to add. 
        ///</param>
        public object ParseAddElement(string add)
        {
            if (!Initial("ParseAddElement()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }

            try
            {
                object o = ParsersExtractors.Add(obj, add);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Add object to collection.
        ///</summary>
        ///<param name="o">
        /// The array of objects to add to. 
        ///</param>
        ///<param name="add">
        /// The object to add. 
        ///</param>
        public string[] ParseAddElement(string[] o, string add)
        {
            if (!Initial("ParseAddElement()")) return null;

            try
            {
                string[] obj = ParsersExtractors.Add(o, add) as string[];
                if (obj == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(obj);
                return obj;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Add object to collection.
        ///</summary>
        ///<param name="o">
        /// The collection of objects to add to. 
        ///</param>
        ///<param name="add">
        /// The object to add. 
        ///</param>
        public IList<string> ParseAddElement(IList<string> o, object add)
        {
            if (!Initial("ParseAddElement()")) return null;

            try
            {
                IList<string> obj = ParsersExtractors.Add(o, add) as IList<string>;
                if (obj == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(obj);
                return obj;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Remove item from persistent storage.
        ///</summary>
        ///<param name="key">
        /// The key that corresponds to the object to be removed.
        ///</param>
        public void Unpersist(string key)
        {
            if (!Initial("ParseRemovePersistent()")) return;
            try
            {
                RemP(key);
                Success();
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Add object to collection.
        ///</summary>
        ///<param name="add">
        /// The object to add. 
        ///</param>
        public object ParseAddElement(object add)
        {
            if (!Initial("ParseAddElement()")) return null;
            object obj = GetS();

            if (obj is object[] == false && obj is IList<object> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            if (add is object == false && add is object[] == false && obj is IList<object> == false)
            {
                Fault("Invalid value!");
                return null;
            }

            try
            {
                object o = ParsersExtractors.Add(obj, add);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Add object to collection.
        ///</summary>
        ///<param name="o">
        /// The array of objects to add to. 
        ///</param>
        ///<param name="add">
        /// The object to add. 
        ///</param>
        public object[] ParseAddElement(object[] o, object add)
        {
            if (!Initial("ParseAddElement()")) return null;

            if (add is object == false && add is object[] == false && o is IList<object> == false)
            {
                Fault("Invalid value!");
                return null;
            }

            try
            {
                object[] obj = ParsersExtractors.Add(o, add) as object[];
                if (obj == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(obj);
                return obj;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Add object to collection.
        ///</summary>
        ///<param name="o">
        /// The collection of objects to add to. 
        ///</param>
        ///<param name="add">
        /// The object to add. 
        ///</param>
        public IList<object> ParseAddElement(IList<object> o, object add)
        {
            if (!Initial("ParseAddElement()")) return null;

            if (add is object == false && add is object[] == false && o is IList<object> == false)
            {
                Fault("Invalid value!");
                return null;
            }

            try
            {
                IList<object> obj = ParsersExtractors.Add(o, add) as IList<object>;
                if (obj == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(obj);
                return obj;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Remove element from collection in result.
        ///</summary>
        ///<param name="n">
        /// The element No. 
        ///</param>
        public object ParseRemoveElement(int n)
        {
            if (!Initial("ParseRemoveElement()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }

            try
            {
                object o = ParsersExtractors.RemoveElement(obj, n);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove element from array.
        ///</summary>
        ///<param name="o">
        /// The array to be parsed.
        ///</param>
        ///<param name="n">
        /// The element No. 
        ///</param>
        public object[] ParseRemoveElement(object[] o, int n)
        {
            if (!Initial("ParseRemoveElement()")) return null;

            try
            {
                object[] obj = ParsersExtractors.RemoveElement(o, n) as object[];
                if (obj == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(obj);
                return obj;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove element from collection.
        ///</summary>
        ///<param name="o">
        /// The collection to be parsed.
        ///</param>
        ///<param name="n">
        /// The element No. 
        ///</param>
        public IList<object> ParseRemoveElement(IList<object> o, int n)
        {
            if (!Initial("ParseRemoveElement()")) return null;

            try
            {
                IList<object> obj = ParsersExtractors.RemoveElement(o, n) as IList<object>;
                if (obj == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(obj);
                return obj;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Extract element from result string collection or array.
        ///</summary>
        ///<param name="n">
        /// The zero based element No. 
        ///</param>
        public object ParseExtractElement(int n)
        {
            if (!Initial("ParseExtractElement()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }

            try
            {
                object o = ParsersExtractors.GetElement(obj, n);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Extract element from array.
        ///</summary>
        ///<param name="o">
        /// The array to be parsed.
        ///</param>
        ///<param name="n">
        /// The zero based element No. 
        ///</param>
        public object ParseExtractElement(object[] o, int n)
        {
            if (!Initial("ParseExtractElement()")) return null;

            try
            {
                object obj = ParsersExtractors.GetElement(o, n);
                if (obj == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(obj);
                return obj;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Extract element from collection.
        ///</summary>
        ///<param name="o">
        /// The collection to be parsed.
        ///</param>
        ///<param name="n">
        /// The zero based element No. 
        ///</param>
        public object ParseExtractElement(IList<object> o, int n)
        {
            if (!Initial("ParseExtractElement()")) return null;

            try
            {
                object obj = ParsersExtractors.GetElement(o, n);
                if (obj == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(obj);
                return obj;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Get last result string collection first element containing string.
        ///</summary>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public object ParseExtractFirstContaining(string content)
        {
            if (!Initial("ParseExtractFirstContaining()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.GetElementContaining(obj, content, true);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string array first element containing string.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public string ParseExtractFirstContaining(string[] s, string content)
        {
            if (!Initial("ParseExtractFirstContaining()")) return null;

            try
            {
                string str = ParsersExtractors.GetElementContaining(s, content, true) as string;
                if (str == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(str);
                return str;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string collection first element containing string.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public string ParseExtractFirstContaining(IList<string> s, string content)
        {
            if (!Initial("ParseExtractFirstContaining()")) return null;

            try
            {
                string str = ParsersExtractors.GetElementContaining(s, content, true) as string;
                if (str == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(str);
                return str;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Get last result string collection first element not containing string.
        ///</summary>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public object ParseExtractFirstNotContaining(string content)
        {
            if (!Initial("ParseExtractFirstNotContaining()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.GetElementNotContaining(obj, content, true);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string array first element not containing string.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public string ParseExtractFirstNotContaining(string[] s, string content)
        {
            if (!Initial("ParseExtractFirstNotContaining()")) return null;

            try
            {
                string str = ParsersExtractors.GetElementNotContaining(s, content) as string;
                if (str == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(str);
                return str;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string collection first element not containing string.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public string ParseExtractFirstNotContaining(IList<string> s, string content)
        {
            if (!Initial("ParseExtractFirstNotContaining()")) return null;

            try
            {
                string str = ParsersExtractors.GetElementNotContaining(s, content) as string;
                if (str == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(str);
                return str;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Get last result string collection elements containing string.
        ///</summary>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public object ParseExtractContaining(string content)
        {
            if (!Initial("ParseExtractContaining()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.GetElementContaining(obj, content);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string array elements containing string.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public string[] ParseExtractContaining(string[] s, string content)
        {
            if (!Initial("ParseExtractContaining()")) return null;

            try
            {
                s = ParsersExtractors.GetElementContaining(s, content) as string[];
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string collection elements containing string.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public IList<string> ParseExtractContaining(IList<string> s, string content)
        {
            if (!Initial("ParseExtractContaining()")) return null;

            try
            {
                s = ParsersExtractors.GetElementContaining(s, content) as IList<string>;
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Get last result string collection elements not containing string.
        ///</summary>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public object ParseExtractNotContaining(string content)
        {
            if (!Initial("ParseExtractNotContaining()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.GetElementNotContaining(obj, content);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string array elements not containing string.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public string[] ParseExtractNotContaining(string[] s, string content)
        {
            if (!Initial("ParseExtractNotContaining()")) return null;

            try
            {
                s = ParsersExtractors.GetElementNotContaining(s, content) as string[];
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string collection elements not containing string.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public IList<string> ParseExtractNotContaining(IList<string> s, string content)
        {
            if (!Initial("ParseExtractNotContaining()")) return null;

            try
            {
                s = ParsersExtractors.GetElementNotContaining(s, content) as IList<string>;
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Get last result string collection first element in given length interval.
        ///</summary>
        ///<param name="min">
        /// The inclusive minimum length.
        ///</param>
        ///<param name="max">
        /// The inclusive maximum length.
        ///</param>
        public object ParseExtractFirstByLength(int min = 0, int max = 999999)
        {
            if (!Initial("ParseExtractFirstByLength()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.GetElementByLength(obj, min, max, true);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string array first element in given length interval.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be parsed.
        ///</param>
        ///<param name="min">
        /// The inclusive minimum length.
        ///</param>
        ///<param name="max">
        /// The inclusive maximum length.
        ///</param>
        public string ParseExtractFirstByLength(string[] s, int min = 0, int max = 999999)
        {
            if (!Initial("ParseExtractFirstByLength()")) return null;

            try
            {
                string str = ParsersExtractors.GetElementByLength(s, min, max, true) as string;
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(str);
                return str;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string collection first element in given length interval.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be parsed.
        ///</param>
        ///<param name="min">
        /// The inclusive minimum length.
        ///</param>
        ///<param name="max">
        /// The inclusive maximum length.
        ///</param>
        public string ParseExtractFirstByLength(IList<string> s, int min = 0, int max = 999999)
        {
            if (!Initial("ParseExtractFirstByLength()")) return null;

            try
            {
                string str = ParsersExtractors.GetElementByLength(s, min, max, true) as string;
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(str);
                return str;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Get last result string collection elements in given length interval.
        ///</summary>
        ///<param name="min">
        /// The inclusive minimum length.
        ///</param>
        ///<param name="max">
        /// The inclusive maximum length.
        ///</param>
        public object ParseExtractByLength(int min = 0, int max = 999999)
        {
            if (!Initial("ParseExtractByLength()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.GetElementByLength(obj, min, max);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string array elements in given length interval.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be parsed.
        ///</param>
        ///<param name="min">
        /// The inclusive minimum length.
        ///</param>
        ///<param name="max">
        /// The inclusive maximum length.
        ///</param>
        public string[] ParseExtractByLength(string[] s, int min = 0, int max = 999999)
        {
            if (!Initial("ParseExtractByLength()")) return null;

            try
            {
                s = ParsersExtractors.GetElementByLength(s, min, max) as string[];
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Get from string collection elements in given length interval.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be parsed.
        ///</param>
        ///<param name="min">
        /// The inclusive minimum length.
        ///</param>
        ///<param name="max">
        /// The inclusive maximum length.
        ///</param>
        public IList<string> ParseExtractByLength(IList<string> s, int min = 0, int max = 999999)
        {
            if (!Initial("ParseExtractByLength()")) return null;

            try
            {
                s = ParsersExtractors.GetElementByLength(s, min, max) as IList<string>;
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Remove last result string collection elements in given length interval.
        ///</summary>
        ///<param name="min">
        /// The inclusive minimum length.
        ///</param>
        ///<param name="max">
        /// The inclusive maximum length.
        ///</param>
        public object ParseRemoveByLength(int min = 0, int max = 999999)
        {
            if (!Initial("ParseRemoveByLength()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.RemoveElementByLength(obj, min, max);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove from string array elements in given length interval.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be parsed.
        ///</param>
        ///<param name="min">
        /// The inclusive minimum length.
        ///</param>
        ///<param name="max">
        /// The inclusive maximum length.
        ///</param>
        public string[] ParseRemoveByLength(string[] s, int min = 0, int max = 999999)
        {
            if (!Initial("ParseRemoveByLength()")) return null;

            try
            {
                s = ParsersExtractors.RemoveElementByLength(s, min, max) as string[];
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove from string collection elements in given length interval.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be parsed.
        ///</param>
        ///<param name="min">
        /// The inclusive minimum length.
        ///</param>
        ///<param name="max">
        /// The inclusive maximum length.
        ///</param>
        public IList<string> ParseRemoveByLength(IList<string> s, int min = 0, int max = 999999)
        {
            if (!Initial("ParseRemoveByLength()")) return null;

            try
            {
                s = ParsersExtractors.RemoveElementByLength(s, min, max) as IList<string>;
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Remove last result string collection elements containing string.
        ///</summary>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public object ParseRemoveContaining(string content)
        {
            if (!Initial("ParseRemoveContaining()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.RemoveElementContaining(obj, content);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove from string array elements containing string.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public string[] ParseRemoveContaining(string[] s, string content)
        {
            if (!Initial("ParseRemoveContaining()")) return null;

            try
            {
                s = ParsersExtractors.RemoveElementContaining(s, content) as string[];
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove from string collection elements containing string.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public IList<string> ParseRemoveContaining(IList<string> s, string content)
        {
            if (!Initial("ParseRemoveContaining()")) return null;

            try
            {
                s = ParsersExtractors.RemoveElementContaining(s, content) as IList<string>;
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Remove last result string collection elements not containing string.
        ///</summary>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public object ParseRemoveNotContaining(string content)
        {
            if (!Initial("ParseRemoveNotContaining()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.RemoveElementNotContaining(obj, content);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(o);
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove from string array elements not containing string.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public string[] ParseRemoveNotContaining(string[] s, string content)
        {
            if (!Initial("ParseRemoveNotContaining()")) return null;

            try
            {
                s = ParsersExtractors.RemoveElementNotContaining(s, content) as string[];
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove from string collection elements not containing string.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be parsed.
        ///</param>
        ///<param name="content">
        /// The string to search in elements for.
        ///</param>
        public IList<string> ParseRemoveNotContaining(IList<string> s, string content)
        {
            if (!Initial("ParseRemoveNotContaining()")) return null;

            try
            {
                s = ParsersExtractors.RemoveElementNotContaining(s, content) as IList<string>;
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }

                Success();
                AddS(s);
                return s;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Concentrates last N elements from storage to string. All elements must be strings, arrays of strings or collections of strings.
        ///</summary>
        ///<param name="lastNElements">
        /// The number of elements.
        ///</param>
        ///<param name="delimiter">
        /// The delimiter to be used.
        ///</param>
        public string ParseExtractConcentrate(int lastNElements, string delimiter = "")
        {
            if (!Initial("ParseExtractConcentrate()")) return null;

            try
            {
                string output = "";

                if (Memory._Storage == null || Memory._Storage.Count < lastNElements)
                {
                    Fault("Invalid value!");
                    return null;
                }
                else
                {
                    lock (Memory._Storage)
                    {
                        for (int i = Memory._Storage.Count - (lastNElements); i < Memory._Storage.Count; i++)
                        {
                            object obj = GetS(i);

                            if (obj is string == false && obj is string[] == false && obj is IList<string> == false)
                            {
                                Fault("Invalid value!");
                                return null;
                            }
                            if (obj is string)
                            {
                                output += (output == "") ? obj as string : delimiter + obj as string;
                            }
                            else if (obj is string)
                            {
                                string[] sep = obj as string[];
                                foreach (string stri in sep)
                                {
                                    output += (output == "") ? obj as string : delimiter + obj as string;
                                }
                            }
                            else if (obj is IList<string>)
                            {
                                IList<string> sep = obj as IList<string>;
                                foreach (string stri in sep)
                                {
                                    output += (output == "") ? obj as string : delimiter + obj as string;
                                }
                            }
                        }
                    }
                }

                AddS(output);
                Success(output);
                return output;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Splits last result string into an array of strings.
        ///</summary>
        ///<param name="delimiter">
        /// The separator the collection will be splited on.
        ///</param>
        public object ParseExtractSplit(string delimiter)
        {
            if (!Initial("ParseExtractSplit()")) return null;
            object obj = GetS();

            if (obj is string == false && obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }

            try
            {
                object o = ParsersExtractors.Split(obj, delimiter);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                AddS(o);
                Success();
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Split string into array of strings.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="delimiter">
        /// The separator the array will be splited on.
        ///</param>
        public string[] ParseExtractSplit(string s, string delimiter)
        {
            if (!Initial("ParseExtractSplit()")) return null;

            try
            {
                string[] o = ParsersExtractors.Split(s, delimiter) as string[];
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }

                AddS(o);
                Success();
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Join last result collection into string.
        ///</summary>
        ///<param name="delimiter">
        /// The separator to be used between each value.
        ///</param>
        public object ParseExtractJoin(string delimiter = "")
        {
            if (!Initial("ParseExtractJoin()")) return null;
            object obj = GetS();

            if (obj is string[] == false && obj is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.Join(obj, delimiter);
                if (o == null) 
                {
                    Fault("Null"); 
                    return null; 
                }

                AddS(o);
                Success();
                return o;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Join array of strings into string.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be parsed.
        ///</param>
        ///<param name="delimiter">
        /// The separator to be used between each value.
        ///</param>
        public string ParseExtractJoin(string[] s, string delimiter = "")
        {
            if (!Initial("ParseExtractJoin()")) return null;

            try
            {
                string otp = ParsersExtractors.Join(s, delimiter) as string;
                if (otp == null)
                {
                    Fault("Null");
                    return null;
                }

                AddS(otp);
                Success();
                return otp;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Join collection of strings into string.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be parsed.
        ///</param>
        ///<param name="delimiter">
        /// The separator to be used between each value.
        ///</param>
        public string ParseExtractJoin(IList<string> s, string delimiter = "")
        {
            if (!Initial("ParseExtractJoin()")) return null;

            try
            {
                string otp = ParsersExtractors.Join(s, delimiter) as string;
                if (otp == null)
                {
                    Fault("Null");
                    return null;
                }

                AddS(otp);
                Success();
                return otp;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Parse last result - extract before of after delimiter, or between delimiters, while char does not mach a pattern.
        ///</summary>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="before">
        /// The string before where extraction start.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public object ParseExtractInverseRegex(string regex, string before = "", string after = "")
        {
            if (!Initial("ParseExtractInverseRegex()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ExtractRegexInverse(s, regex, before, after);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - extract before of after delimiter, or between delimiters, while char does not mach a pattern.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="before">
        /// The string before where extraction start.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public string ParseExtractInverseRegex(string s, string regex, string before = "", string after = "")
        {
            if (!Initial("ParseExtractInverseRegex()")) return null;
            try
            {
                s = ParsersExtractors.ExtractRegexInverse(s, regex, before, after) as string;

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
        ///<summary>
        /// Parse an array of strings - extract before of after delimiter, or between delimiters, while char does not mach a pattern.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="before">
        /// The string before where extraction start.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public string[] ParseExtractInverseRegex(string[] s, string regex, string before = "", string after = "")
        {
            if (!Initial("ParseExtractInverseRegex()")) return null;
            try
            {
                s = ParsersExtractors.ExtractRegexInverse(s, regex, before, after) as string[];

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
        ///<summary>
        /// Parse a collection of strings - extract before of after delimiter, or between delimiters, while char does not mach a pattern.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="before">
        /// The string before where extraction start.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public IList<string> ParseExtractInverseRegex(IList<string> s, string regex, string before = "", string after = "")
        {
            if (!Initial("ParseExtractInverseRegex()")) return null;
            try
            {
                s = ParsersExtractors.ExtractRegexInverse(s, regex, before, after) as IList<string>;

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


        ///<summary>
        /// Parse last result - extract pattern with string.
        ///</summary>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        public object ParseExtractRegex(string regex)
        {
            if (!Initial("ParseExtractRegex()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ExtractRegex(s, regex, false);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - extract pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        public string ParseExtractRegex(string s, string regex)
        {
            if (!Initial("ParseExtractRegex()")) return null;
            try
            {
                s = ParsersExtractors.ExtractRegex(s, regex, false) as string;

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
        ///<summary>
        /// Parse an array of strings - extract pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        public string[] ParseExtractRegex(string[] s, string regex)
        {
            if (!Initial("ParseExtractRegex()")) return null;
            try
            {
                s = ParsersExtractors.ExtractRegex(s, regex, false) as string[];

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
        ///<summary>
        /// Parse a collection of strings - extract pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        public IList<string> ParseExtractRegex(IList<string> s, string regex)
        {
            if (!Initial("ParseExtractRegex()")) return null;
            try
            {
                s = ParsersExtractors.ExtractRegex(s, regex, false) as IList<string>;

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


        ///<summary>
        /// Parse last result - extract first occurance of pattern with string.
        ///</summary>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        public object ParseExtractRegexFirst(string regex)
        {
            if (!Initial("ParseExtractRegexFirst()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ExtractRegex(s, regex, true);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - extract first occurance of pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        public string ParseExtractRegexFirst(string s, string regex)
        {
            if (!Initial("ParseExtractRegexFirst()")) return null;
            try
            {
                s = ParsersExtractors.ExtractRegex(s, regex, true) as string;

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
        ///<summary>
        /// Parse an array of strings - extract first occurance of pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        public string[] ParseExtractRegexFirst(string[] s, string regex)
        {
            if (!Initial("ParseExtractRegexFirst()")) return null;
            try
            {
                s = ParsersExtractors.ExtractRegex(s, regex, true) as string[];

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
        ///<summary>
        /// Parse a collection of strings - extract first occurance of pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        public IList<string> ParseExtractRegexFirst(IList<string> s, string regex)
        {
            if (!Initial("ParseExtractRegexFirst()")) return null;
            try
            {
                s = ParsersExtractors.ExtractRegex(s, regex, true) as IList<string>;

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


        ///<summary>
        /// Parse last result - extract first occurance of delimited string.
        ///</summary>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public object ParseExtractDelimitedFirst(string before, string after)
        {
            if (!Initial("ParseExtractDelimitedFirst()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ExtractDelimited(s, before, after, true);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - extract first occurance of delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public string ParseExtractDelimitedFirst(string s, string before, string after)
        {
            if (!Initial("ParseExtractDelimitedFirst()")) return null;
            try
            {
                s = ParsersExtractors.ExtractDelimited(s, before, after, true) as string;

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
        ///<summary>
        /// Parse an array of strings - extract first occurance of delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public string[] ParseExtractDelimitedFirst(string[] s, string before, string after)
        {
            if (!Initial("ParseExtractDelimitedFirst()")) return null;
            try
            {
                s = ParsersExtractors.ExtractDelimited(s, before, after, true) as string[];

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
        ///<summary>
        /// Parse a collection of strings - extract first occurance of delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public IList<string> ParseExtractDelimitedFirst(IList<string> s, string before, string after)
        {
            if (!Initial("ParseExtractDelimitedFirst()")) return null;
            try
            {
                s = ParsersExtractors.ExtractDelimited(s, before, after, true) as IList<string>;

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


        ///<summary>
        /// Parse last result - extract delimited string.
        ///</summary>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public object ParseExtractDelimited(string before, string after)
        {
            if (!Initial("ParseExtractDelimited()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ExtractDelimited(s, before, after, false);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - extract delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public string ParseExtractDelimited(string s, string before, string after)
        {
            if (!Initial("ParseExtractDelimited()")) return null;
            try
            {
                s = ParsersExtractors.ExtractDelimited(s, before, after, false) as string;

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
        ///<summary>
        /// Parse an array of strings - extract delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public string[] ParseExtractDelimited(string[] s, string before, string after)
        {
            if (!Initial("ParseExtractDelimited()")) return null;
            try
            {
                s = ParsersExtractors.ExtractDelimited(s, before, after, false) as string[];

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
        ///<summary>
        /// Parse a collection of strings - extract delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        public IList<string> ParseExtractDelimited(IList<string> s, string before, string after)
        {
            if (!Initial("ParseExtractDelimited()")) return null;
            try
            {
                s = ParsersExtractors.ExtractDelimited(s, before, after, false) as IList<string>;

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


        ///<summary>
        /// Parse a string - extract url by part of the url.
        ///</summary>
        ///<param name="part">
        /// The beggining of the Url.
        ///</param>
        public string ParseExtractUrlByPartFirst(string part)
        {
            if (!Initial("ParseExtractUrlByPartFirst()")) return null;
            object obj = GetS();

            if (obj is string == false)
            {
                Fault("Invalid value!");
                return null;
            }

            try
            {
                string s = obj as string;
                s = ParsersExtractors.ExtractRegexInverse(s, patternInvalidUrl, part) as string;
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    s = part + s;
                    AddS(s);
                    Success(s);
                    return s;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string - extract url by part of the url.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="part">
        /// The beggining of the Url.
        ///</param>
        public string ParseExtractUrlByPartFirst(string s, string part)
        {
            if (!Initial("ParseExtractUrlByPartFirst()")) return null;
            try
            {
                s = ParsersExtractors.ExtractRegexInverse(s, patternInvalidUrl, part) as string;
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    s = part + s;
                    AddS(s);
                    Success(s);
                    return s;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }


        ///<summary>
        /// Parse last result - replace first occurance of string.
        ///</summary>
        ///<param name="search">
        /// The string to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public object ParseReplaceFirst(string search, string replacee = "")
        {
            if (!Initial("ParseReplaceFirst()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.Replace(s, search, replacee, true);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - replace first occurance of string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="search">
        /// The string to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string ParseReplaceFirst(string s, string search, string replacee = "")
        {
            if (!Initial("ParseReplaceFirst()")) return null;
            try
            {
                s = ParsersExtractors.Replace(s, search, replacee, true) as string;

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
        ///<summary>
        /// Parse an array of strings - replace first occurance of string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="search">
        /// The string to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string[] ParseReplaceFirst(string[] s, string search, string replacee = "")
        {
            if (!Initial("ParseReplaceFirst()")) return null;
            try
            {
                s = ParsersExtractors.Replace(s, search, replacee, true) as string[];

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
        ///<summary>
        /// Parse a collection of strings - replace first occurance of string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="search">
        /// The string to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public IList<string> ParseReplaceFirst(IList<string> s, string search, string replacee = "")
        {
            if (!Initial("ParseReplaceFirst()")) return null;
            try
            {
                s = ParsersExtractors.Replace(s, search, replacee, true) as IList<string>;

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


        ///<summary>
        /// Parse last result - replace string.
        ///</summary>
        ///<param name="search">
        /// The string to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public object ParseReplace(string search, string replacee = "")
        {
            if (!Initial("ParseReplace()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.Replace(s, search, replacee, false);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - replace string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="search">
        /// The string to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string ParseReplace(string s, string search, string replacee = "")
        {
            if (!Initial("ParseReplace()")) return null;
            try
            {
                s = ParsersExtractors.Replace(s, search, replacee, false) as string;

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
        ///<summary>
        /// Parse an array of strings - replace string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="search">
        /// The string to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string[] ParseReplace(string[] s, string search, string replacee = "")
        {
            if (!Initial("ParseReplace()")) return null;
            try
            {
                s = ParsersExtractors.Replace(s, search, replacee, false) as string[];

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
        ///<summary>
        /// Parse a collection of strings - replace string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="search">
        /// The string to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public IList<string> ParseReplace(IList<string> s, string search, string replacee = "")
        {
            if (!Initial("ParseReplace()")) return null;
            try
            {
                s = ParsersExtractors.Replace(s, search, replacee, false) as IList<string>;

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


        ///<summary>
        /// Parse last result - replace first occurance of delimited string.
        ///</summary>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public object ParseReplaceDelimitedFirst(string before, string after, string replacee = "")
        {
            if (!Initial("ParseReplaceDelimitedFirst()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ReplaceDelimited(s, before, after, replacee, true);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - replace first occurance of delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string ParseReplaceDelimitedFirst(string s, string before, string after, string replacee = "")
        {
            if (!Initial("ParseReplaceDelimitedFirst()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceDelimited(s, before, after, replacee, true) as string;

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
        ///<summary>
        /// Parse an array of strings - replace first occurance of delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string[] ParseReplaceDelimitedFirst(string[] s, string before, string after, string replacee = "")
        {
            if (!Initial("ParseReplaceDelimitedFirst()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceDelimited(s, before, after, replacee, true) as string[];

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
        ///<summary>
        /// Parse a collection of strings - replace first occurance of delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public IList<string> ParseReplaceDelimitedFirst(IList<string> s, string before, string after, string replacee = "")
        {
            if (!Initial("ParseReplaceDelimitedFirst()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceDelimited(s, before, after, replacee, true) as IList<string>;

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


        ///<summary>
        /// Parse last result - replace delimited string.
        ///</summary>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public object ParseReplaceDelimited(string before, string after, string replacee = "")
        {
            if (!Initial("ParseReplaceDelimited()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ReplaceDelimited(s, before, after, replacee, false);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - replace delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string ParseReplaceDelimited(string s, string before, string after, string replacee = "")
        {
            if (!Initial("ParseReplaceDelimited()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceDelimited(s, before, after, replacee, false) as string;

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
        ///<summary>
        /// Parse an array of strings - replace delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string[] ParseReplaceDelimited(string[] s, string before, string after, string replacee = "")
        {
            if (!Initial("ParseReplaceDelimited()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceDelimited(s, before, after, replacee, false) as string[];

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
        ///<summary>
        /// Parse a collection of strings - replace delimited string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="before">
        /// The string before what should be replaced.
        ///</param>
        ///<param name="after">
        /// The string after.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public IList<string> ParseReplaceDelimited(IList<string> s, string before, string after, string replacee = "")
        {
            if (!Initial("ParseReplaceDelimited()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceDelimited(s, before, after, replacee, false) as IList<string>;

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


        ///<summary>
        /// Parse last result - replace pattern with string.
        ///</summary>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public object ParseReplaceRegex(string regex, string replacee = "")
        {
            if (!Initial("ParseReplaceRegex()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ReplaceRegex(s, regex, replacee, false);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - replace pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string ParseReplaceRegex(string s, string regex, string replacee = "")
        {
            if (!Initial("ParseReplaceRegex()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceRegex(s, regex, replacee, false) as string;

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
        ///<summary>
        /// Parse an array of strings - replace pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string[] ParseReplaceRegex(string[] s, string regex, string replacee = "")
        {
            if (!Initial("ParseReplaceRegex()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceRegex(s, regex, replacee, false) as string[];

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
        ///<summary>
        /// Parse a collection of strings - replace pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public IList<string> ParseReplaceRegex(IList<string> s, string regex, string replacee = "")
        {
            if (!Initial("ParseReplaceRegex()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceRegex(s, regex, replacee, false) as IList<string>;

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


        ///<summary>
        /// Parse last result - replace first occurance of pattern with string.
        ///</summary>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public object ParseReplaceRegexFirst(string regex, string replacee = "")
        {
            if (!Initial("ParseReplaceRegexFirst()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ReplaceRegex(s, regex, replacee, true);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url - replace first occurance of pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string ParseReplaceRegexFirst(string s, string regex, string replacee = "")
        {
            if (!Initial("ParseReplaceRegexFirst()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceRegex(s, regex, replacee, true) as string;

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
        ///<summary>
        /// Parse an array of strings - replace first occurance of pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public string[] ParseReplaceRegexFirst(string[] s, string regex, string replacee = "")
        {
            if (!Initial("ParseReplaceRegexFirst()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceRegex(s, regex, replacee, true) as string[];

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
        ///<summary>
        /// Parse a collection of strings - replace first occurance of pattern with string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="regex">
        /// The regular expresion pattern to search for.
        ///</param>
        ///<param name="replacee">
        /// The string to replace with.
        ///</param>
        public IList<string> ParseReplaceRegexFirst(IList<string> s, string regex, string replacee = "")
        {
            if (!Initial("ParseReplaceRegexFirst()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceRegex(s, regex, replacee, true) as IList<string>;

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


        ///<summary>
        /// Parse last result url replacing the query string.
        ///</summary>
        ///<param name="query">
        /// The query string be appended.
        ///</param>
        public object ParseReplaceUrlQuery(string query = "")
        {
            if (!Initial("ParseReplaceUrlQuery()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ReplaceUrlQuery(s, query);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse a string url replacing the query string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="query">
        /// The query string be appended.
        ///</param>
        public string ParseReplaceUrlQuery(string s, string query = "")
        {
            if (!Initial("ParseReplaceUrlQuery()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceUrlQuery(s, query) as string;

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
        ///<summary>
        /// Parse an array of string urls replacing the query string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="query">
        /// The query string be appended.
        ///</param>
        public string[] ParseReplaceUrlQuery(string[] s, string query = "")
        {
            if (!Initial("ParseReplaceUrlQuery()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceUrlQuery(s, query) as string[];

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
        ///<summary>
        /// Parse a collection of string urls replacing the query string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="query">
        /// The query string be appended.
        ///</param>
        public IList<string> ParseReplaceUrlQuery(IList<string> s, string query = "")
        {
            if (!Initial("ParseReplaceUrlQuery()")) return null;
            try
            {
                s = ParsersExtractors.ReplaceUrlQuery(s, query) as IList<string>;

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


        ///<summary>
        /// Parse last result string as POST query pair/pairs.
        ///</summary>
        ///<param name="fieldName">
        /// The string be appended.
        ///</param>
        public object ParseAsPostField(string fieldName)
        {
            if (!Initial("ParseAsPostField()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.ParseAsPostField(s, fieldName);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Parse string as POST query pair.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="fieldName">
        /// The string be appended.
        ///</param>
        public string ParseAsPostField(string s, string fieldName)
        {
            if (!Initial("ParseAsPostField()")) return null;
            try
            {
                s = ParsersExtractors.ParseAsPostField(s, fieldName) as string;

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
        ///<summary>
        /// Parse an array of strings as POST query pairs.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="fieldName">
        /// The string be appended.
        ///</param>
        public string[] ParseAsPostField(string[] s, string fieldName)
        {
            if (!Initial("ParseAsPostField()")) return null;
            try
            {
                s = ParsersExtractors.ParseAsPostField(s, fieldName) as string[];

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
        ///<summary>
        /// Parse a collection of strings as POST query pairs.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="fieldName">
        /// The string be appended.
        ///</param>
        public IList<string> ParseAsPostField(IList<string> s, string fieldName)
        {
            if (!Initial("ParseAsPostField()")) return null;
            try
            {
                s = ParsersExtractors.ParseAsPostField(s, fieldName) as IList<string>;

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


        ///<summary>
        /// Append string/s before last result string/s.
        ///</summary>
        ///<param name="append">
        /// The string be appended.
        ///</param>
        public object ParseAppendBefore(string append)
        {
            if (!Initial("ParseAppendBefore()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.Append(s, append, true);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Append string before string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="append">
        /// The string be appended.
        ///</param>
        public string ParseAppendBefore(string s, string append)
        {
            if (!Initial("ParseAppendBefore()")) return null;
            try
            {
                s = ParsersExtractors.Append(s, append, true) as string;

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
        ///<summary>
        /// Parse an array of strings appending string before each string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="append">
        /// The string be appended.
        ///</param>
        public string[] ParseAppendBefore(string[] s, string append)
        {
            if (!Initial("ParseAppendBefore()")) return null;
            try
            {
                s = ParsersExtractors.Append(s, append, true) as string[];

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
        ///<summary>
        /// Parse a collection of strings appending string before each string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="append">
        /// The string be appended.
        ///</param>
        public IList<string> ParseAppendBefore(IList<string> s, string append)
        {
            if (!Initial("ParseAppendBefore()")) return null;
            try
            {
                s = ParsersExtractors.Append(s, append, true) as IList<string>;

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


        ///<summary>
        /// Append string/s after last result string/s.
        ///</summary>
        ///<param name="append">
        /// The string be appended.
        ///</param>
        public object ParseAppendAfter(string append)
        {
            if (!Initial("ParseAppendAfter()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                object o = ParsersExtractors.Append(s, append, false);
                if (o == null)
                {
                    Fault("Null");
                    return null;
                }
                else
                {
                    Success();
                    AddS(o);
                    return o;
                }
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Append string after string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="append">
        /// The string be appended.
        ///</param>
        public string ParseAppendAfter(string s, string append)
        {
            if (!Initial("ParseAppendAfter()")) return null;
            try
            {
                s = ParsersExtractors.Append(s, append, false) as string;

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
        ///<summary>
        /// Parse an array of strings appending string after each string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="append">
        /// The string be appended.
        ///</param>
        public string[] ParseAppendAfter(string[] s, string append)
        {
            if (!Initial("ParseAppendAfter()")) return null;
            try
            {
                s = ParsersExtractors.Append(s, append, false) as string[];

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
        ///<summary>
        /// Parse a collection of strings appending string after each string.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        ///<param name="append">
        /// The string be appended.
        ///</param>
        public IList<string> ParseAppendAfter(IList<string> s, string append)
        {
            if (!Initial("ParseAppendAfter()")) return null;
            try
            {
                s = ParsersExtractors.Append(s, append, false) as IList<string>;

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


        ///<summary>
        /// UrlEncode last result.
        ///</summary>
        public object ParseUrlEncode()
        {
            if (!Initial("ParseUrlEncode()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersExtractors.UrlEncode(s);

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
        ///<summary>
        /// UrlEncode a string.
        ///</summary>
        ///<param name="s">
        /// The string to be encoded.
        ///</param>
        public string ParseUrlEncode(string s)
        {
            if (!Initial("ParseUrlEncode()")) return null;

            try
            {
                s = ParsersExtractors.UrlEncode(s) as string;

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
        ///<summary>
        /// UrlEncode an array of strings.
        ///</summary>
        ///<param name="s">
        /// The array string[] to be encoded.
        ///</param>
        public string[] ParseUrlEncode(string[] s)
        {
            if (!Initial("ParseUrlEncode()")) return null;

            try
            {
                s = ParsersExtractors.UrlEncode(s) as string[];

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
        ///<summary>
        /// UrlEncode a collection of strings.
        ///</summary>
        ///<param name="s">
        /// The collection to be encoded.
        ///</param>
        public IList<string> ParseUrlEncode(IList<string> s)
        {
            if (!Initial("ParseUrlEncode()")) return null;

            try
            {
                s = ParsersExtractors.UrlEncode(s) as IList<string>;

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


        ///<summary>
        /// UrlDecode last result.
        ///</summary>
        public object ParseUrlDecode()
        {
            if (!Initial("ParseUrlDecode()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersExtractors.UrlDecode(s);

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
        ///<summary>
        /// UrlDecode a string.
        ///</summary>
        ///<param name="s">
        /// The string to be encoded.
        ///</param>
        public string ParseUrlDecode(string s)
        {
            if (!Initial("ParseUrlDecode()")) return null;

            try
            {
                s = ParsersExtractors.UrlDecode(s) as string;

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
        ///<summary>
        /// UrlDecode an array of strings.
        ///</summary>
        ///<param name="s">
        /// The array string[] to be encoded.
        ///</param>
        public string[] ParseUrlDecode(string[] s)
        {
            if (!Initial("ParseUrlDecode()")) return null;

            try
            {
                s = ParsersExtractors.UrlDecode(s) as string[];

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
        ///<summary>
        /// UrlDecode a collection of strings.
        ///</summary>
        ///<param name="s">
        /// The collection to be encoded.
        ///</param>
        public IList<string> ParseUrlDecode(IList<string> s)
        {
            if (!Initial("ParseUrlDecode()")) return null;

            try
            {
                s = ParsersExtractors.UrlDecode(s) as IList<string>;

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


        ///<summary>
        /// Parse last result from BG to latin.
        ///</summary>
        public object ParseBGtoLatin()
        {
            if (!Initial("ParseBGtoLatin()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersExtractors.BGtoLatin(s);

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
        ///<summary>
        /// Parse a string from BG to latin.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        public string ParseBGtoLatin(string s)
        {
            if (!Initial("ParseBGtoLatin()")) return null;

            try
            {
                s = ParsersExtractors.BGtoLatin(s) as string;

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
        ///<summary>
        /// Parse an array of strings from BG to latin.
        ///</summary>
        ///<param name="s">
        /// The array string[] to be parsed.
        ///</param>
        public string[] ParseBGtoLatin(string[] s)
        {
            if (!Initial("ParseBGtoLatin()")) return null;

            try
            {
                s = ParsersExtractors.BGtoLatin(s) as string[];

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
        ///<summary>
        /// Parse a collection of strings from BG to latin.
        ///</summary>
        ///<param name="s">
        /// The collection to be parsed.
        ///</param>
        public IList<string> ParseBGtoLatin(IList<string> s)
        {
            if (!Initial("ParseBGtoLatin()")) return null;

            try
            {
                s = ParsersExtractors.BGtoLatin(s) as IList<string>;

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


        ///<summary>
        /// Parse last result from RU to latin.
        ///</summary>
        public object ParseRUtoLatin()
        {
            if (!Initial("ParseRUtoLatin()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersExtractors.RUtoLatin(s);

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
        ///<summary>
        /// Parse a string from RU to latin.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        public string ParseRUtoLatin(string s)
        {
            if (!Initial("ParseRUtoLatin()")) return null;

            try
            {
                s = ParsersExtractors.RUtoLatin(s) as string;

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
        ///<summary>
        /// Parse an array of strings from RU to latin.
        ///</summary>
        ///<param name="s">
        /// The array string[] to be parsed.
        ///</param>
        public string[] ParseRUtoLatin(string[] s)
        {
            if (!Initial("ParseRUtoLatin()")) return null;

            try
            {
                s = ParsersExtractors.RUtoLatin(s) as string[];

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
        ///<summary>
        /// Parse a collection of strings from RU to latin.
        ///</summary>
        ///<param name="s">
        /// The collection to be parsed.
        ///</param>
        public IList<string> ParseRUtoLatin(IList<string> s)
        {
            if (!Initial("ParseRUtoLatin()")) return null;

            try
            {
                s = ParsersExtractors.RUtoLatin(s) as IList<string>;

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


        ///<summary>
        /// Parse last result to lower case.
        ///</summary>
        public object ParseToLower()
        {
            if (!Initial("ParseToLower()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersExtractors.ToLower(s);

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
        ///<summary>
        /// Parse a string to lower case.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        public string ParseToLower(string s)
        {
            if (!Initial("ParseToLower()")) return null;

            try
            {
                s = ParsersExtractors.ToLower(s) as string;

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
        ///<summary>
        /// Parse an array of strings to lower case.
        ///</summary>
        ///<param name="s">
        /// The array string[] to be parsed.
        ///</param>
        public string[] ParseToLower(string[] s)
        {
            if (!Initial("ParseToLower()")) return null;

            try
            {
                s = ParsersExtractors.ToLower(s) as string[];

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
        ///<summary>
        /// Parse a collection of strings to lower case.
        ///</summary>
        ///<param name="s">
        /// The collection to be parsed.
        ///</param>
        public IList<string> ParseToLower(IList<string> s)
        {
            if (!Initial("ParseToLower()")) return null;

            try
            {
                s = ParsersExtractors.ToLower(s) as IList<string>;

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


        ///<summary>
        /// Parse last result to upper case.
        ///</summary>
        public object ParseToUpper()
        {
            if (!Initial("ParseToUpper()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersExtractors.ToUpper(s);

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
        ///<summary>
        /// Parse a string to upper case.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        public string ParseToUpper(string s)
        {
            if (!Initial("ParseToUpper()")) return null;

            try
            {
                s = ParsersExtractors.ToUpper(s) as string;

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
        ///<summary>
        /// Parse an array of strings to upper case.
        ///</summary>
        ///<param name="s">
        /// The array string[] to be parsed.
        ///</param>
        public string[] ParseToUpper(string[] s)
        {
            if (!Initial("ParseToUpper()")) return null;

            try
            {
                s = ParsersExtractors.ToUpper(s) as string[];

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
        ///<summary>
        /// Parse a collection of strings to upper case.
        ///</summary>
        ///<param name="s">
        /// The collection to be parsed.
        ///</param>
        public IList<string> ParseToUpper(IList<string> s)
        {
            if (!Initial("ParseToUpper()")) return null;

            try
            {
                s = ParsersExtractors.ToUpper(s) as IList<string>;

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


        ///<summary>
        /// Parse last result - upper case letters to lower and lower case to upper.
        ///</summary>
        public object ParseSwitchCases()
        {
            if (!Initial("ParseSwitchCases()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersExtractors.SwitchCases(s);

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
        ///<summary>
        /// Parse a string - upper case letters to lower and lower case to upper.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        public string ParseSwitchCases(string s)
        {
            if (!Initial("ParseSwitchCases()")) return null;

            try
            {
                s = ParsersExtractors.SwitchCases(s) as string;

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
        ///<summary>
        /// Parse an array of strings - upper case letters to lower and lower case to upper.
        ///</summary>
        ///<param name="s">
        /// The array string[] to be parsed.
        ///</param>
        public string[] ParseSwitchCases(string[] s)
        {
            if (!Initial("ParseSwitchCases()")) return null;

            try
            {
                s = ParsersExtractors.SwitchCases(s) as string[];

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
        ///<summary>
        /// Parse a collection of strings - upper case letters to lower and lower case to upper.
        ///</summary>
        ///<param name="s">
        /// The collection to be parsed.
        ///</param>
        public IList<string> ParseSwitchCases(IList<string> s)
        {
            if (!Initial("ParseSwitchCases()")) return null;

            try
            {
                s = ParsersExtractors.SwitchCases(s) as IList<string>;

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


        ///<summary>
        /// Parse last result replacing letters with special characters to create strong password.
        ///</summary>
        public object ParsePasswordize()
        {
            if (!Initial("ParsePasswordize()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersExtractors.Passwordize(s);

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
        ///<summary>
        /// Parse a string replacing letters with special characters to create strong password.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        public string ParsePasswordize(string s)
        {
            if (!Initial("ParsePasswordize()")) return null;

            try
            {
                s = ParsersExtractors.Passwordize(s) as string;

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
        ///<summary>
        /// Parse an array of strings replacing letters with special characters to create strong password.
        ///</summary>
        ///<param name="s">
        /// The array string[] to be parsed.
        ///</param>
        public string[] ParsePasswordize(string[] s)
        {
            if (!Initial("ParsePasswordize()")) return null;

            try
            {
                s = ParsersExtractors.Passwordize(s) as string[];

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
        ///<summary>
        /// Parse a collection of strings replacing letters with special characters to create strong password.
        ///</summary>
        ///<param name="s">
        /// The collection to be parsed.
        ///</param>
        public IList<string> ParsePasswordize(IList<string> s)
        {
            if (!Initial("ParseSwitchCases()")) return null;

            try
            {
                s = ParsersExtractors.Passwordize(s) as IList<string>;

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


        ///<summary>
        /// Parse last result removing characters forbidden in filenames.
        ///</summary>
        public object ParseCleanFileName()
        {
            if (!Initial("ParseCleanFileName()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersExtractors.CleanFileName(s as string);

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
        ///<summary>
        /// Parse a string removing characters forbidden in filenames.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        public string ParseCleanFileName(string s)
        {
            if (!Initial("ParseCleanFileName()")) return null;

            try
            {
                s = ParsersExtractors.CleanFileName(s) as string;

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
        ///<summary>
        /// Parse an array of strings removing characters forbidden in filenames.
        ///</summary>
        ///<param name="s">
        /// The array string[] to be parsed.
        ///</param>
        public string[] ParseCleanFileName(string[] s)
        {
            if (!Initial("ParseCleanFileName()")) return null;

            try
            {
                s = ParsersExtractors.CleanFileName(s) as string[];

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
        ///<summary>
        /// Parse a collection of strings removing characters forbidden in filenames.
        ///</summary>
        ///<param name="s">
        /// The collection to be parsed.
        ///</param>
        public IList<string> ParseCleanFileName(IList<string> s)
        {
            if (!Initial("ParseCleanFileName()")) return null;

            try
            {
                s = ParsersExtractors.CleanFileName(s) as IList<string>;

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


        ///<summary>
        /// Parse last result url to get the host name.
        ///</summary>
        public object ParseGetHost()
        {
            if (!Initial("ParseGetHost()")) return null;
            object s = GetS();

            if (s is string == false && s is string[] == false && s is IList<string> == false)
            {
                Fault("Invalid value!");
                return null;
            }
            try
            {
                s = ParsersExtractors.GetHost(s as string);

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
        ///<summary>
        /// Parse a string url to get the host name.
        ///</summary>
        ///<param name="s">
        /// The string to be parsed.
        ///</param>
        public string ParseGetHost(string s)
        {
            if (!Initial("ParseGetHost()")) return null;

            try
            {
                s = ParsersExtractors.GetHost(s) as string;

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
        ///<summary>
        /// Parse an array of string urls to get the host names.
        ///</summary>
        ///<param name="s">
        /// The array string[] to be parsed.
        ///</param>
        public string[] ParseGetHost(string[] s)
        {
            if (!Initial("ParseGetHost()")) return null;

            try
            {
                s = ParsersExtractors.GetHost(s) as string[];

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
        ///<summary>
        /// Parse a collection of string urls to get the host names.
        ///</summary>
        ///<param name="s">
        /// The collection to be parsed.
        ///</param>
        public IList<string> ParseGetHost(IList<string> s)
        {
            if (!Initial("ParseGetHost()")) return null;

            try
            {
                s = ParsersExtractors.GetHost(s) as IList<string>;

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