using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace HttpVehicle
{
    class IO
    {
        private static string _SavePath = iDesktoppath + "Save" + "\\";

        internal static string iDesktoppath
        {
            get { return Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + @"\"; }
        }
        internal static string iExepath
        {
            get { return new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath; }
        }
        internal static string iExedir
        {
            get { return Path.GetDirectoryName(iDesktoppath); }
        }
        internal static string iSavedir
        {
            get { return _SavePath; }
            set { _SavePath = value; }
        }
        internal static string iDatestamp
        {
            get
            {
                DateTime dt = DateTime.Now;
                return "(" + dt.Day.ToString().PadLeft(2, '0') + "." + dt.Month.ToString().PadLeft(2, '0') + "." + dt.Year.ToString() + "--" +
                    dt.Hour.ToString().PadLeft(2, '0') + "." + dt.Minute.ToString().PadLeft(2, '0') + "." + dt.Second.ToString().PadLeft(2, '0') + "--" +
                    dt.Millisecond.ToString().PadLeft(3, '0');
            }
        }
        internal static string iSystemdir
        {
            get 
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.System); 
            }
        }
        internal static string iPrinterShortcutsdir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.PrinterShortcuts);
            }
        }
        internal static string iRecentdir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            }
        }
        internal static string iMyComputerdir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }
        }
        internal static string iMyDocumentsdir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
        }
        internal static string iMyMusicdir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            }
        }
        internal static string iMyPicturesdir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }
        }
        internal static string iMyVideosdir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            }
        }
        internal static string iLocalApplicationDatadir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            }
        }
        internal static string iCommonVideosdir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos);
            }
        }
        internal static string iCDBurningdir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.CDBurning);
            }
        }




        public static string GetLineFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            string line = lines[0];

            return line.Replace("\"", "").Replace(",", "").Replace(";", "").Replace("\'", "");
        }
        public static string GetRandomLineFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            int lineNo = Informers.CreateRandInt(0, lines.Length);
            string line = lines[lineNo];

            return line.Replace("\"", "").Replace(",", "").Replace(";", "").Replace("\'", "");
        }




        public static bool Save(string fullPath, object toSave)
        {
            if (toSave is byte[])
            {
                byte[] blob = toSave as byte[];
                File.WriteAllBytes(fullPath, blob);
                return true;
            }
            else if (toSave is string)
            {
                string str = toSave as string;
                File.WriteAllText(fullPath, str);
                return true;
            }
            else if (toSave is string[])
            {
                string str = string.Join(Environment.NewLine, toSave as string[]);
                File.WriteAllText(fullPath, str);
                return true;
            }
            else if(toSave is IEnumerable)
            {
                IEnumerable<object> o = toSave as IEnumerable<object>;

                int i = 1;
                foreach(object obj in o)
                {
                    Save('(' + i + ')' + fullPath, obj);
                    i++;
                }
            }
            return false;
        }
        public static bool SaveSafe(string fullPath, object toSave)
        {

            if (File.Exists(fullPath))
            {
                string newPath = fullPath;
                int i = 1;
                while (File.Exists(newPath))
                {
                    string path = Path.GetDirectoryName(fullPath);
                    string file = Path.GetFileName(fullPath);

                    string name = file.Substring(0, file.Length - (file.Length - file.LastIndexOf('.')));
                    string ext = file.Substring(file.LastIndexOf('.'), file.Length - file.LastIndexOf('.'));
                    newPath = path + '\\' + name + '(' + i + ')' + ext;
                    i++;
                }
                fullPath = newPath;
            }


            return Save(fullPath, toSave);
        }
        public static bool Append(string fullPath, object toSave)
        {
            if (toSave is byte[])
            {
                byte[] blob = toSave as byte[];
                if (File.Exists(fullPath))
                {
                    using (var stream = new FileStream(fullPath, FileMode.Append)) { stream.Write(blob, 0, blob.Length); }
                }
                else { File.WriteAllBytes(fullPath, blob); }
                return true;
            }
            else if (toSave is string)
            {
                string str = toSave as string;
                if (File.Exists(fullPath)) { File.AppendAllText(fullPath, str); }
                else { File.WriteAllText(fullPath, str); }
                return true;
            }
            else if (toSave is string[])
            {
                string str = String.Join(Environment.NewLine, toSave as string[]);
                if (File.Exists(fullPath)) { File.AppendAllText(fullPath, str); }
                else { File.WriteAllText(fullPath, str); }
                return true;
            }
            else if (toSave is IEnumerable<string>)
            {
                Append(fullPath, (toSave as IEnumerable<string>).ToArray());
            }
            else if (toSave is IEnumerable)
            {
                IEnumerable<object> o = toSave as IEnumerable<object>;

                int i = 1;
                foreach (object obj in o)
                {
                    Append('(' + i + ')' + fullPath, obj);
                    i++;
                }
            }
            return false;
        }
        public static bool Accumulate(string fullPath, object toSave)
        {
            if (toSave is string)
            {
                string str = toSave as string;
                if (File.Exists(fullPath))
                {
                    string[] all = File.ReadAllLines(fullPath);
                    if (all.Contains(str) == false)
                    {
                        Append(fullPath, str);
                    }
                    return true;
                }
                else
                {
                    File.WriteAllText(fullPath, str);
                }
            }
            else if (toSave is string[])
            {
                string[] sep = toSave as string[];

                if (File.Exists(fullPath))
                {
                    string[] all = File.ReadAllLines(fullPath);

                    List<string> toAdd = new List<string>();
                    foreach (string str in sep)
                    {
                        if (all.Contains(str) == false)
                        {
                            toAdd.Add(str);
                        }
                    }
                    foreach (string s in all)
                    {
                        toAdd.Add(s);
                    }
                    File.WriteAllLines(fullPath, toAdd);
                    return true;
                }
                else
                {
                    File.WriteAllLines(fullPath, sep);
                    return true;
                }
            }
            else if (toSave is IEnumerable<string>)
            {
                Accumulate(fullPath, (toSave as IEnumerable<string>).ToArray());
            }
            else if (toSave is IEnumerable)
            {
                IEnumerable<object> o = toSave as IEnumerable<object>;

                foreach (object obj in o)
                {
                    Accumulate(fullPath, obj);
                }
            }
            return false;
        }
        public static bool Remove(string fullPath, object toRemove)
        {
            if (toRemove is string)
            {
                string str = toRemove as string;
                if (File.Exists(fullPath))
                {
                    List<string> all = File.ReadAllLines(fullPath).ToList();
                    if (all.Contains(str))
                    {
                        all.Remove(str);
                        File.WriteAllLines(fullPath, all.ToArray());
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (toRemove is string[])
            {
                string[] sep = toRemove as string[];

                if (File.Exists(fullPath))
                {
                    List<string> all = File.ReadAllLines(fullPath).ToList();

                    foreach (string str in sep)
                    {
                        if (all.Contains(str))
                        {
                            all.Remove(str);
                        }
                    }

                    File.WriteAllLines(fullPath, all.ToArray());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (toRemove is IEnumerable<string>)
            {
                Append(fullPath, (toRemove as IEnumerable<string>).ToArray());
            }
            else if (toRemove is IEnumerable)
            {
                IEnumerable<object> o = toRemove as IEnumerable<object>;

                foreach (object obj in o)
                {
                    Remove(fullPath, obj);
                }
            }
            return false;
        }
        public static bool Scramble(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                List<string> all = File.ReadAllLines(fullPath).ToList();
                List<string> otp = new List<string>();
                List<int> indexes = new List<int>();

                for (int i = 0; i < all.Count; i++) { indexes.Add(i); }
                while (indexes.Count > 0)
                {
                    int i = Informers.CreateRandInt(0, indexes.Count);
                    int ind = indexes[i];
                    otp.Add(all[ind]);
                    indexes.Remove(i);
                }

                File.WriteAllLines(fullPath, otp);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Delete(string fullPath)
        {
            File.Delete(fullPath);
            return true;
        }

        public static bool Serialize(string fullPath, object toSave)
        {
            using (Stream stream = File.Open(fullPath, FileMode.Create))
            {
                new BinaryFormatter().Serialize(stream, toSave);
            }
            return true;
        }
        public static byte[] Serialize(object toSave)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, toSave);
                return ms.ToArray();
            }
        }
        public static object Deserialize(string fullPath)
        {
            object o;
            using (Stream stream = File.Open(fullPath, FileMode.Open))
            {
                o = new BinaryFormatter().Deserialize(stream);
            }
            return o;
        }
        public static object Deserialize(byte[] buff)
        {
            object o;
            using (var ms = new MemoryStream(buff))
            {
                o = new BinaryFormatter().Deserialize(ms);
            }
            return o;
        }

        public static string EncryptString(string plain, string password)
        {
            byte[] bytes = new byte[plain.Length * sizeof(char)];
            Buffer.BlockCopy(plain.ToCharArray(), 0, bytes, 0, bytes.Length);

            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, plain.Length);
            cryptoStream.Close();
            byte[] crypted = memoryStream.ToArray();

            char[] chars = new char[crypted.Length / sizeof(char)];
            Buffer.BlockCopy(crypted, 0, chars, 0, crypted.Length);
            return new string(chars);
        }
        public static byte[] EncryptBytes(string plain, string password)
        {
            byte[] bytes = new byte[plain.Length * sizeof(char)];
            Buffer.BlockCopy(plain.ToCharArray(), 0, bytes, 0, bytes.Length);

            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, plain.Length);
            cryptoStream.Close();
            byte[] crypted = memoryStream.ToArray();

            return crypted;
        }
        public static byte[] EncryptBytes(byte[] plain, string password)
        {
            byte[] bytes = plain;

            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, plain.Length);
            cryptoStream.Close();
            byte[] crypted = memoryStream.ToArray();

            return crypted;
        }
        public static string DecryptString(byte[] cipher, string password)
        {
            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipher, 0, cipher.Length);
            cryptoStream.Close();
            byte[] plain = memoryStream.ToArray();

            char[] chars = new char[plain.Length / sizeof(char)];
            Buffer.BlockCopy(plain, 0, chars, 0, plain.Length);
            return new string(chars);
        }
        public static byte[] DecryptBytes(byte[] cipher, string password)
        {
            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipher, 0, cipher.Length);
            cryptoStream.Close();
            byte[] plain = memoryStream.ToArray();

            return plain;
        }

        private static byte[] salt = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
    }
}