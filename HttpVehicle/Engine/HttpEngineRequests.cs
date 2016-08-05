using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        ///<summary>
        /// Perform HTTP/HTTPS GET request.
        ///</summary>
        ///<param name="url">
        /// The Url of the request.
        ///</param>
        public object GetRequest(string url = "")
        {
            if (!Initial("GetRequest()")) return null;

            if (url == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return null;
                }
                else { url = obj as string; }
            }


            try
            {
            ln1: if (!url.Contains("http://") && !url.Contains("https://")) url = "http://" + url;

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url); 
                req.CookieContainer = CookieJar;

                addHeadersPOST(ref req);

                req.Method = "GET";
                req.Host = url.Split(new string[] { "//" }, StringSplitOptions.None)[1].Split('/')[0];
                req.UserAgent = UserAgent;
                req.Proxy = Proxy;
                req.Timeout = Timeout;


                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                int status = (int)resp.StatusCode;
                if (status == 200)
                {
                    string resph = ""; for (int i = 0; i < resp.Headers.Count; i++) resph += resp.Headers.Keys[i] + ":" + resp.Headers[i] + ";" + Environment.NewLine;
                    string contentType = resp.GetResponseHeader("content-type");
                    if (resph.ToLower().Contains("content-encoding:gzip"))
                    {

                        if (isText(contentType))
                        {
                            string result = new StreamReader(new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress)).ReadToEnd();
                            AddS(result);
                            Success(url + " Error 200");
                            resp.Close();
                            return result;
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress).CopyTo(ms);
                            byte[] result = ms.ToArray();
                            AddS(result);
                            Success(url + " Error 200");
                            resp.Close();
                            return result;
                        }
                    }
                    else
                    {
                        if (isText(contentType))
                        {
                            string result = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                            AddS(result);
                            Success(url + " Error 200");
                            resp.Close();
                            return result;
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream(); resp.GetResponseStream().CopyTo(ms);
                            byte[] result = ms.ToArray();
                            AddS(result);
                            Success(url + " Error 200");
                            resp.Close();
                            return result;
                        }
                    }
                }
                if (status == 302 || status == 301)
                {
                    for (int i = 0; i < resp.Headers.Count; i++) { if (resp.Headers.Keys[i].Contains("Location")) url = resp.Headers[i]; }
                    LogLine(" Error " + status.ToString() + " redirecting...");
                    resp.Close();
                    goto ln1;
                }
                Fault(url);
                resp.Close();
                return null;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Perform HTTP/HTTPS POST request.
        ///</summary>
        ///<param name="url">
        /// The Url of the request.
        ///</param>
        ///<param name="data">
        /// The data url encoded string to be posted.
        ///</param>
        public object PostRequest(string url, string data = "")
        {
            if (!Initial("PostRequest()")) return null;

            if (data == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return null;
                }
                else { data = obj as string; }
            }

            try
            {
                if (!url.Contains("http://") && !url.Contains("https://")) url = "http://" + url;

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url); 
                WebHeaderCollection c = req.Headers; req.CookieContainer = CookieJar;

                req.Method = "POST";
                req.Host = url.Split(new string[] { "//" }, StringSplitOptions.None)[1].Split('/')[0];
                byte[] buff = new UTF8Encoding().GetBytes(data);

                addHeadersPOST(ref req);

                req.ContentLength = buff.Length;
                req.UserAgent = UserAgent;
                req.Proxy = Proxy;
                req.Timeout = Timeout;

                using (Stream stream = req.GetRequestStream())
                {
                    stream.Write(buff, 0, buff.Length);
                }

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                int status = (int)resp.StatusCode;
                if (status == 200)
                {
                    string resph = ""; for (int i = 0; i < resp.Headers.Count; i++) resph += resp.Headers.Keys[i] + ":" + resp.Headers[i] + ";" + Environment.NewLine;
                    string contentType = resp.GetResponseHeader("content-type");

                    if (resph.ToLower().Contains("content-encoding:gzip"))
                    {
                        if (isText(contentType))
                        {
                            string result = new StreamReader(new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress)).ReadToEnd();
                            AddS(result);
                            Success(url + " Error 200");
                            resp.Close();
                            return result;
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress).CopyTo(ms);
                            byte[] result = ms.ToArray();
                            AddS(result);
                            Success(url + " Error 200");
                            resp.Close();
                            return result;
                        }
                    }
                    else
                    {
                        if (isText(contentType))
                        {
                            string result = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                            AddS(result);
                            Success(url + " Error 200");
                            resp.Close();
                            return result;
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            resp.GetResponseStream().CopyTo(ms);
                            byte[] result = ms.ToArray();
                            AddS(result);
                            Success(url + " Error 200");
                            resp.Close();
                            return result;
                        }
                    }
                }
                if (status == 302 || status == 301)
                {
                    string resph = ""; for (int i = 0; i < resp.Headers.Count; i++) resph += resp.Headers.Keys[i] + ":" + resp.Headers[i] + ";" + Environment.NewLine;
                    string contentType = resp.GetResponseHeader("content-type");

                    if (resph.ToLower().Contains("content-encoding:gzip"))
                    {
                        if (isText(contentType))
                        {
                            string result = new StreamReader(new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress)).ReadToEnd();
                            AddS(result);
                            Success(url + " Error " + status);
                            resp.Close();
                            return result;
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress).CopyTo(ms);
                            byte[] result = ms.ToArray();
                            AddS(result);
                            Success(url + " Error " + status);
                            resp.Close();
                            return result;
                        }
                    }
                    else
                    {
                        if (isText(contentType))
                        {                     
                            string result = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                            AddS(result);
                            Success(url + " Error " + status);
                            resp.Close();
                            return result;
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream(); resp.GetResponseStream().CopyTo(ms);
                            byte[] result = ms.ToArray();
                            AddS(result);
                            Success(url + " Error " + status);
                            resp.Close();
                            return result;
                        }
                    }
                }
                Fault(url + ':' + status);
                resp.Close();
                return null;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }




        ///<summary>
        /// Pause the execution for a random period of time, in a given interval.
        ///</summary>
        ///<param name="min">
        /// The minimum period to pause for, in milliseconds.
        ///</param>
        ///<param name="max">
        /// The maximum period to pause for, in milliseconds.
        ///</param>
        public void Sleep(int min = 0, int max = 5000)
        {
            if (!Initial("Sleep()")) return;
            int delay = new Random().Next(min, max);
            System.Threading.Thread.Sleep(delay);
            Success(delay.ToString());
        }
        ///<summary>
        /// Pause the execution for a given period of time
        ///</summary>
        ///<param name="millisecs">
        /// The period to pause for, in milliseconds
        ///</param>
        public void Sleep(int millisecs)
        {
            if (!Initial("Sleep()")) return;
            System.Threading.Thread.Sleep(millisecs);
            Success(millisecs.ToString());
        }




        private bool isText(string contentType)
        {
            contentType = contentType.ToLower();
            if (contentType.Contains("text/plain") ||
                contentType.Contains("text/html") ||
                contentType.Contains("text/css") ||
                contentType.Contains("text/javascript") ||
                contentType.Contains("application/javascript") ||
                contentType.Contains("application/xml") ||
                contentType.Contains("application/xhtml+xml"))
                return true;
            return false;
        }                               //http://www.freeformatter.com/mime-types-list.html
        private void addHeadersPOST(ref HttpWebRequest req)
        {
            req.ServicePoint.Expect100Continue = false;
            req.AllowAutoRedirect = false;

            if (Memory._AdditionalHeaders != null)
            {
                lock (Memory._AdditionalHeaders)
                {
                    if (!Memory._AdditionalHeaders.ContainsKey("Accept")) 
                        req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                    if (!Memory._AdditionalHeaders.ContainsKey("Content-Type")) 
                        req.ContentType = "application/x-www-form-urlencoded";
                    if (!Memory._AdditionalHeaders.ContainsKey("Accept-Encoding")) 
                        req.Headers.Add("Accept-Encoding", "gzip, deflate");
                    if (!Memory._AdditionalHeaders.ContainsKey("Accept-Language")) 
                        req.Headers.Add("Accept-Language", "en-US,en;q=0.8");

                    foreach (KeyValuePair<string, string> h in Memory._AdditionalHeaders)
                    {
                        if (h.Key == "Content-Length") { continue; }
                        else if (h.Key == "Host") { continue; }
                        else if (h.Key == "Proxy") { continue; }
                        else if (h.Key == "Timeout") { continue; }
                        else if (h.Key == "User-Agent") { continue; }
                        else if (h.Key == "Content-Type") { req.ContentType = h.Value; }
                        else if (h.Key == "Accept") { req.Accept = h.Value; }
                        else if (h.Key == "Connection") 
                        {
                            if (h.Value.ToLower() == "keep-alive")
                            {
                                req.KeepAlive = true;
                                var sp = req.ServicePoint;
                                var prop = sp.GetType().GetProperty("HttpBehaviour", BindingFlags.Instance | BindingFlags.NonPublic);
                                prop.SetValue(sp, (byte)0, null);
                            }
                        }
                        else if (h.Key == "Content-Type") { req.ContentType = h.Value; }
                        else if (h.Key == "Date") { req.Date = Convert.ToDateTime(h.Value); }
                        else if (h.Key == "Expect") { req.Expect = h.Value; }
                        else if (h.Key == "Protocol-Version  ") { req.ProtocolVersion = new Version(h.Value); }
                        else if (h.Key == "Referer") { req.Referer = h.Value; }
                        else if (h.Key == "Transfer-Encoding") { req.TransferEncoding = h.Value; }
                        else if (h.Key == "User-Agent") { req.UserAgent = h.Value; }
                        else { req.Headers.Add(h.Key, h.Value); }
                    }
                    Memory._AdditionalHeaders = null;
                }
            }
            else
            {
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                req.ContentType = "application/x-www-form-urlencoded";
                req.Headers.Add("Accept-Encoding", "gzip, deflate");
                req.Headers.Add("Accept-Language", "en-US,en;q=0.8");
            }
        }
        private void addHeadersGET(ref HttpWebRequest req)
        {
            req.ServicePoint.Expect100Continue = false;
            req.AllowAutoRedirect = false;

            if (Memory._AdditionalHeaders != null)
            {
                lock (Memory._AdditionalHeaders)
                {
                    if (!Memory._AdditionalHeaders.ContainsKey("Accept")) 
                        req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                    if (!Memory._AdditionalHeaders.ContainsKey("Accept-Encoding")) 
                        req.Headers.Add("Accept-Encoding", "gzip, deflate");
                    if (!Memory._AdditionalHeaders.ContainsKey("Accept-Language")) 
                        req.Headers.Add("Accept-Language", "en-US,en;q=0.8");

                    foreach (KeyValuePair<string, string> h in Memory._AdditionalHeaders)
                    {
                        if (h.Key == "Content-Length") { continue; }
                        else if (h.Key == "Host") { continue; }
                        else if (h.Key == "Proxy") { continue; }
                        else if (h.Key == "Timeout") { continue; }
                        else if (h.Key == "User-Agent") { continue; }
                        else if (h.Key == "Content-Type") { continue; }
                        else if (h.Key == "Accept") { req.Accept = h.Value; }
                        else if (h.Key == "Connection") { if (h.Value.ToLower() == "keep-alive") req.KeepAlive = true; }
                        else if (h.Key == "Content-Type") { req.ContentType = h.Value; }
                        else if (h.Key == "Date") { req.Date = Convert.ToDateTime(h.Value); }
                        else if (h.Key == "Expect") { req.Expect = h.Value; }
                        else if (h.Key == "Protocol-Version  ") { req.ProtocolVersion = new Version(h.Value); }
                        else if (h.Key == "Referer") { req.Referer = h.Value; }
                        else if (h.Key == "Transfer-Encoding") { req.TransferEncoding = h.Value; }
                        else if (h.Key == "User-Agent") { req.UserAgent = h.Value; }
                        else { req.Headers.Add(h.Key, h.Value); }
                    }
                    Memory._AdditionalHeaders = null;
                }
            }
            else
            {
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                req.Headers.Add("Accept-Encoding", "gzip, deflate");
                req.Headers.Add("Accept-Language", "en-US,en;q=0.8");
            }
        }
    }
}