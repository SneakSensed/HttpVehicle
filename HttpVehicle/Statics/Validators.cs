using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Net;

namespace HttpVehicle
{
    class Validators
    {
        public const string AnyStringPattern = @"(.*?)";
        public const string InvalidUrlPattern = @"[^-\]_.~!*'();:@&=+$,/?%#[A-z0-9]";
        public const string IpPattern = @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}";
        public const string IpPortPattern = @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}:[0-9]{1,5}";
        public const string IpPortUserPattern = @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}:[0-9]{1,5}:\S+";
        public const string IpPortUserPassPattern = @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}:[0-9]{1,5}:\S+:\S+";
        public const string EmailPattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public const string EmailPassPattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$:\S+";

        public static bool ValidateUrl(string url)
        {
            string invalidChars = InvalidUrlPattern;
            Regex Rx = new Regex(invalidChars, RegexOptions.IgnoreCase);
            return !Rx.IsMatch(url);
        }
        public static bool ValidateIp(string ip)
        {
            string pattern = IpPattern;
            Regex Rx = new Regex(pattern, RegexOptions.IgnoreCase);
            return Rx.IsMatch(ip);
        }
        public static bool ValidateIpPort(string ip)
        {
            string pattern = IpPortPattern;
            Regex Rx = new Regex(pattern, RegexOptions.IgnoreCase);
            return Rx.IsMatch(ip);
        }
        public static bool ValidateIpPortUser(string ip)
        {
            string pattern = IpPortUserPattern;
            Regex Rx = new Regex(pattern, RegexOptions.IgnoreCase);
            return Rx.IsMatch(ip);
        }
        public static bool ValidateIpPortUserPass(string ip)
        {
            string pattern = IpPortUserPassPattern;
            Regex Rx = new Regex(pattern, RegexOptions.IgnoreCase);
            return Rx.IsMatch(ip);
        }
        public static bool ValidateEmail(string ip)
        {
            string pattern = EmailPattern;
            Regex Rx = new Regex(pattern, RegexOptions.IgnoreCase);
            return Rx.IsMatch(ip);
        }
        public static bool ValidateEmailPass(string ip)
        {
            string pattern = EmailPassPattern;
            Regex Rx = new Regex(pattern, RegexOptions.IgnoreCase);
            return Rx.IsMatch(ip);
        }

        public static bool ValidateStringEquals(string compare, string toCompare)
        {
            if (compare == toCompare) return true;
            return false;
        }
        public static bool ValidateStringContains(string compare, string toCompare)
        {
            if (compare.Contains(toCompare)) return true;
            return false;
        }
        public static bool ValidateStringDontContains(string compare, string toCompare)
        {
            if (compare.Contains(toCompare)) return false;
            return true;
        }
        public static bool ValidateStringLength(string s, int min, int max)
        {
            if(string.IsNullOrEmpty(s) || 
                string.IsNullOrWhiteSpace(s) || 
                s.Length < min || 
                s.Length > max) return false;
            return true;
        }
        //ValidateSimilarity/Diversity
        //ValidateContainsPattern
        //ValidateIsPattern
        //Validate proxy/userAgent
 
        //ValidateCredentials
        //ValidateLanguage
        //ValidateEncoding
        //ValidateHasHtmlElement
        //for multyvalues and multyvalues count and divisibility

        public static bool ValidateFolderExists(string path)
        {
            try
            {
                if (Directory.Exists(path) == true)
                {
                    return true;
                }
                else if (Directory.Exists(Path.GetDirectoryName(path)))
                {
                    return true;
                }
                else { return false; }
            }
            catch
            {
                return false;
            }
        }
        public static bool ValidateFileExists(string path)
        {
            try
            {
                if (File.Exists(path) == true)
                {
                    return true;
                }
                else { return false; }
            }
            catch
            {
                return false;
            }
        }
        public static bool ValidateLocalFileExists(string fileName)
        {
            string path = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath + @"\" + fileName;
            return ValidateFileExists(fileName);
        }
        public static bool ValidateWriteAccess(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    DirectorySecurity ds = Directory.GetAccessControl(path);
                    return true;
                }
                else
                {
                    DirectorySecurity ds = Directory.GetAccessControl(Path.GetDirectoryName(path));
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool ValidateLocalWriteAccess(string fileName)
        {
            string path = new Uri(IO.iSavedir).LocalPath + @"\" + fileName;
            return ValidateWriteAccess(path);
        }
        public static bool ValidateReadAccess(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.GetFiles(path);
                    return true;
                }
                else
                {
                    Directory.GetFiles(Path.GetDirectoryName(path));
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        public static bool ValidateHostUp(string address)
        {
            string[] sep = address.Split(':');
            PingReply reply = new Ping().Send(sep[0], 7000);
            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            return false;
        }
        public static bool ValidateResourceExists(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                if (response.StatusCode == HttpStatusCode.OK ||
                    response.StatusCode == HttpStatusCode.Accepted ||
                    response.StatusCode == HttpStatusCode.Ambiguous ||
                    response.StatusCode == HttpStatusCode.Continue ||
                    response.StatusCode == HttpStatusCode.Created ||
                    response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Found ||
                    response.StatusCode == HttpStatusCode.Gone ||
                    response.StatusCode == HttpStatusCode.HttpVersionNotSupported ||
                    response.StatusCode == HttpStatusCode.LengthRequired ||
                    response.StatusCode == HttpStatusCode.MultipleChoices ||
                    response.StatusCode == HttpStatusCode.NoContent ||
                    response.StatusCode == HttpStatusCode.NonAuthoritativeInformation ||
                    response.StatusCode == HttpStatusCode.PartialContent ||
                    response.StatusCode == HttpStatusCode.Redirect ||
                    response.StatusCode == HttpStatusCode.RedirectKeepVerb ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                    
                return true;
                
                return false;
            }
            catch
            {
                return false;
            }
        }
        //ValidateProxyType
        //ValidateLagLess





        public static bool ValidateLackOfError(List<object> results)
        {
            bool output = true;

            if (results[results.Count - 1] is string)
            {
                if (results[results.Count - 1] as string == "error")
                {
                    output = false;
                }
            }

            return output;
        }
    }
}