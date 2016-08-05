using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        private bool _ConsoleLog = false;
        private bool _IgnoreError = false;
        private bool _ErrorOnly = false;


        ///<summary>
        /// Output log to the console.
        ///</summary>
        public void CONSOLE_LOG(ConsoleColor fore = ConsoleColor.DarkCyan)
        {
            Console.ForegroundColor = fore;
            if (_ConsoleLog == false) { if (Log != null && Log != "") Console.Write(Log + Environment.NewLine); }
            _ConsoleLog = true;
            return;
        }
        ///<summary>
        /// If Console logging is enabled, wait for user to press a key.
        ///</summary>
        public void PRESS_ANY_KEY()
        {
            if (_ConsoleLog)
            {
                LogLine(Environment.NewLine + "Press any key to continue!");
                Console.ReadLine();
            }
            return;
        }
        ///<summary>
        /// Remove result/s before last.
        ///</summary>
        ///<param name="n">
        /// The number of results to remove
        ///</param>
        public void COMPACT(int n = 1)
        {
            if (Memory._Storage != null && Memory._Storage.Count > n)
            {
                lock (Memory._Storage)
                {
                    for (int i = 0; i < n; i++)
                    {
                        Memory._Storage.RemoveAt(Memory._Storage.Count - 2);
                    }
                }
            }
        }
        ///<summary>
        /// Open a file.
        ///</summary>
        ///<param name="path">
        /// Full path to the file, or relative to pSavepath.
        ///</param>
        public void VIEW_FILE(string path = "")
        {
            if (!Initial("VIEW_FILE()")) return;

            if (path == "")
            {
                object obj = GetS();
                if (obj is string)
                {
                    Fault("Invalid value!");
                    return;
                }
                path = obj as string;
            }
            try
            {
                if (!Validators.ValidateFileExists(path))
                {
                    path = pSavepath + path;
                }
                System.Diagnostics.Process.Start(path);
                Success();
                return;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Require user input
        ///</summary>
        public string INPUT()
        {
            if (!Initial("INPUT() > ")) return null;
            string s = Console.ReadLine();

            AddS(s);
            Success();
            return s;
        }

        ///<summary>
        /// Use around functions that are not critical.
        ///</summary>
        public void IGNORE_ERROR()
        {
            if (_IgnoreError == true) _IgnoreError = false;
            else _IgnoreError = true;
        }
        ///<summary>
        /// Clear after error.
        ///</summary>
        public void UNDO_ERROR()
        {
            object lastRes = GetS();
            if (lastRes is string && lastRes as string == "error")
            {
                Memory._Storage.Remove(lastRes);
            }
        }
        ///<summary>
        /// Use to fix error.
        ///</summary>
        public void ERROR_ONLY()
        {
            if (_ErrorOnly == true) _ErrorOnly = false;
            else _ErrorOnly = true;
        }
    }
}
