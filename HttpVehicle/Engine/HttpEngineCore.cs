using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace HttpVehicle
{
    ///<summary>
    /// The Http Engine
    /// </summary>
    public partial class HttpEngine
    {
        //HEAD
        [Serializable]
        private class MemorySet
        {
            //Agent
            public string _FileName = IO.iDatestamp;

            public CookieContainer _Jar;
            public string _Log;
            public Dictionary<string, string> _AdditionalHeaders;   //autoresetable
            public string _UserAgent;
            public WebProxy _Proxy;
            public int _Timeout;

            //ICBM
            public Dictionary<string, object> _Persist;
            public List<object> _Storage;
        }
        MemorySet Memory = new MemorySet();
        private int _storageSize = 20;


        ///<summary>
        /// Ctor.
        /// Construct the engine.
        ///</summary>
        public HttpEngine()
        {
            ResetFileName();
            ResetJar();

            ResetAdditionalHeaders();
                                   
            ProvideSetUserAgent();
            ResetProxy();
            ResetTimeout();
              
            ResetPersistent();
            ResetStorage();
        }


        ///<summary>
        /// The number of results the engine storage holds.
        /// These are generally used for debugging purposes only, 
        /// though there are few functions that might take advantage of number of stored elements.
        /// </summary>
        public int StorageSize
        {
            get { return _storageSize; }
            set { _storageSize = value; }
        }
        ///<summary>
        /// The log for this run of the engine.
        ///</summary>
        public string Log
        {
            get
            {
                if (Memory._Log != null)
                {
                    string log = "";
                    lock (Memory._Log)
                    {
                        log = Memory._Log;
                    }
                    return log;
                }
                else return null;
            }
            private set
            {
                if (Memory._Log != null)
                {
                    lock (Memory._Log)
                    {
                        Memory._Log = value;
                    }
                }
                else { Memory._Log = value; }
            }
        }
        ///<summary>
        /// The cookies for this instance of the engine currently set.
        ///</summary>
        public CookieCollection Cookies
        {
            get
            {
                CookieCollection cc = null;
                if (Memory._Jar != null)
                {
                    lock (Memory._Jar)
                    {
                        cc = ParsersExtractors.ExtractAllCookies(Memory._Jar);
                    }
                }
                return cc;
            }
        }
        ///<summary>
        /// The cookie container for this instance of the engine.
        ///</summary>
        public CookieContainer CookieJar
        {
            get
            {
                CookieContainer cc = null;
                if (Memory._Jar != null)
                {
                    lock (Memory._Jar)
                    {
                        cc = Memory._Jar;
                    }
                }
                return cc;
            }
            private set
            {
                if (Memory._Jar != null)
                {
                    lock (Memory._Jar)
                    {
                        Memory._Jar = value;
                    }
                }
                else Memory._Jar = value;
            }
        }
        ///<summary>
        /// Outputs the cookies for this instance of the engine currently set.
        ///</summary>
        public string[] CookieStrings
        {
            get
            {
                CookieCollection cc = Cookies;
                return ParsersExtractors.ExtractAllCookies(cc);
            }
        }
        ///<summary>
        /// The UserAgent for this instance of the engine
        ///</summary>
        public string UserAgent
        {
            get
            {
                string s = null;
                if (Memory._UserAgent != null)
                {
                    lock (Memory._UserAgent)
                    {
                        s = Memory._UserAgent;
                    }
                }
                return s;
            }
            private set
            {
                if (Memory._UserAgent != null)
                {
                    lock (Memory._UserAgent)
                    {
                        Memory._UserAgent = value;
                    }
                }
                else Memory._UserAgent = value;
            }
        }
        ///<summary>
        /// The Proxy for this instance of the engine
        ///</summary>
        public string ProxyString
        {
            get
            {
                string s = null;
                if (Memory._Proxy != null)
                {
                    lock (Memory._Proxy)
                    {
                        s = ParsersExtractors.ExtractProxy(Memory._Proxy);
                    }
                }
                return s;
            }
            private set
            {
                string[] sep = value.Split(':');
                WebProxy prox = new WebProxy();
                if (sep.Length == 2)
                {
                    prox = new WebProxy(sep[0], Convert.ToInt32(sep[1]));
                }
                else if (sep.Length == 4)
                {
                    prox = new WebProxy(sep[0], Convert.ToInt32(sep[1]));
                    prox.Credentials = new NetworkCredential(sep[2], sep[3]);
                }
            }
        }
        ///<summary>
        /// The Proxy for this instance of the engine
        ///</summary>
        public WebProxy Proxy
        {
            get
            {
                WebProxy s = null;
                if (Memory._Proxy != null)
                {
                    lock (Memory._Proxy)
                    {
                        s = Memory._Proxy;
                    }
                }
                return s;
            }
            private set
            {
                if (Memory._Proxy != null)
                {
                    lock (Memory._Proxy)
                    {
                        Memory._Proxy = value;
                    }
                }
                else Memory._Proxy = value;
            }
        }
        ///<summary>
        /// The Timeout for this instance of the engine
        ///</summary>
        public int Timeout
        {
            get
            {
                return Memory._Timeout;
            }
            private set
            {
                Memory._Timeout = value;
            }
        }
        ///<summary>
        /// The FileName for this instance of the engine
        ///</summary>
        public string FileName
        {
            get
            {
                return Memory._FileName;
            }
            private set
            {
                Memory._FileName = value;
            }
        }
        ///<summary>
        /// Get last stored object.
        ///</summary>
        public object LastStored
        {
            get
            {
                return GetS();
            }
        }
        Dictionary<string, string> AdditionalHeaders
        {
            get
            {
                if (Memory._AdditionalHeaders != null)
                {
                    return Memory._AdditionalHeaders;
                }
                return null;
            }
        }


        private object GetS(int i = -1)
        {
            if (Memory._Storage != null && Memory._Storage.Count > 0)
            {
                lock (Memory._Storage)
                {
                    if (i > -1)
                    {
                        return Memory._Storage[i];
                    }
                    else return Memory._Storage[Memory._Storage.Count - 1];
                }
            }
            return null;
        }
        private void AddS(object o)
        {
            if (Memory._Storage != null)
            {
                lock (Memory._Storage)
                {
                    Memory._Storage.Add(o);
                    if (Memory._Storage.Count > _storageSize)
                    {
                        List<object> newstorege = new List<object>();
                        for (int i = Memory._Storage.Count - _storageSize; i < Memory._Storage.Count; i++)
                        {
                            newstorege.Add(Memory._Storage[i]);
                        }
                        Memory._Storage = newstorege;
                    }
                }
            }
        }
        private object GetP(string key)
        {
            if (Memory._Persist != null && Memory._Persist.Count > 0 && Memory._Persist.ContainsKey(key))
            {
                lock (Memory._Persist)
                {
                    return Memory._Persist[key];
                }
            }
            return null;
        }
        private void AddP(string key, object o = null)
        {
            if (o == null) o = GetS();
            if (Memory._Persist != null && key != null && key != "")
            {
                lock (Memory._Persist)
                {
                    if (Memory._Persist.ContainsKey(key))
                    {
                        object obj = Memory._Persist[key];
                        //wander why don't work eh? 
                        //You haven't implemented it yet you lazy fuck!
                    }
                    else
                    {
                        Memory._Persist.Add(key, o);
                    }
                }
            }
        }
        private void RemP(string key)
        {
            if (Memory._Persist != null && Memory._Persist.Count > 0 && Memory._Persist.ContainsKey(key))
            {
                lock (Memory._Persist)
                {
                    Memory._Persist.Remove(key);
                }
            }
        }
        private void ClrP()
        {
            if (Memory._Persist != null)
            {
                lock (Memory._Persist)
                {
                    Memory._Persist = new Dictionary<string, object>();
                }
            }
            else Memory._Persist = new Dictionary<string, object>();
        }


        private void LogLine(string line)
        {
            if (Memory._Log != null)
            {
                lock (Memory._Log)
                {
                    Memory._Log += line;
                }
            }
            if (_ConsoleLog) Console.WriteLine(line);
        }
        private bool Initial(string fName)
        {
            if (Memory._Log != null)
            {
                lock (Memory._Log)
                {
                    if (Memory._Log != null && Memory._Log != "") Memory._Log += Environment.NewLine;
                    Memory._Log += fName + " :";
                }
            }
            if (_ConsoleLog) Console.Write(fName + " :");

            if (Memory._Storage != null && Memory._Storage.Count > 0)
            {
                bool lackErrBefore = Validators.ValidateLackOfError(Memory._Storage);
                if (_ErrorOnly) lackErrBefore = !lackErrBefore;
                if (!lackErrBefore)
                {
                    LogLine(" Encountered error before!");
                    return false;
                }
            }
            return true;
        }
        private void Exceptional(Exception ex)
        {
            if (_IgnoreError == false) AddS("error");
            LogLine(" " + ex.Message.Replace(Environment.NewLine, "! "));
        }
        private void Fault(string errMessage = "")
        {
            if (_IgnoreError == false) AddS("error");
            if (errMessage != "" && !string.IsNullOrEmpty(errMessage) && !string.IsNullOrWhiteSpace(errMessage))
            {
                LogLine(" Error -> " + errMessage);
            }
            else
            {
                LogLine(" Error");
            }
        }
        private void Success(string okMessage = "")
        {
            if (okMessage != "" && !string.IsNullOrEmpty(okMessage) && !string.IsNullOrWhiteSpace(okMessage))
            {
                LogLine(" Ok -> " + okMessage);
            }
            else
            {
                LogLine(" Ok");
            }
        }
    }
}
