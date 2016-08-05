using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace HttpVehicle
{
    class Informers
    {
        static Random Ran = new Random();

        public static string CreateUserAgent(int n = 1)
        {
                string[] uas = Informers.chopEnbeddedString("HttpVehicle.Data.UserAgents.txt");
                int ran = CreateRandInt(1, uas.Count());
                return uas[ran];
        }

        public static string CreatePassword(int minLenght = 12, int maxLenght = 16, string flag = "3")
        {
            string pool = @"023456789";
            switch (flag)
            {
                case "0":
                    break;
                case "1":
                    pool = @"abcdefghijklmnopqrstuvwxyz";
                    break;
                case "2":
                    pool = @"abcdefghijklmnopqrstuvwxyz";
                    break;
                case "3":
                    pool = @"AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwZxYyZz";
                    break;
                case "4":
                    pool = @"AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwZxYyZz1234567890";
                    break;
                case "5":
                    pool = @"AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwZxYyZz1234567890!@#$%^&*()_+{},./\|][";
                    break;
                default:
                    if (String.IsNullOrEmpty(flag) == false && String.IsNullOrWhiteSpace(flag) == false)
                    {
                        string newPool = "";
                        if (flag.ToLower().Contains("numeric")) { newPool += @"023456789"; }
                        if (flag.ToLower().Contains("alpha")) { newPool += @"abcdefghijklmnopqrstuvwxyz"; }
                        if (flag.ToLower().Contains("upper")) { newPool += @"ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
                        if (flag.ToLower().Contains("symbolic")) { newPool += @"!@#$%^&*()_+ {},./\|]["; }
                        if (newPool != "") { pool = newPool; }
                    }
                    break;
            }
            int length = CreateRandInt(minLenght, maxLenght);

            return CreateRandString(length, pool);
        }
        public static string CreatePasswordBG(int minLenght = 12, int maxLenght = 16, string flag = "3")
        {
            string pool = @"023456789";
            switch (flag)
            {
                case "0":
                    break;
                case "1":
                    pool = @"абвгдежзийклмнопрстуфхцчшщъьюя";
                    break;
                case "2":
                    pool = @"абвгдежзийклмнопрстуфхцчшщъьюя";
                    break;
                case "3":
                    pool = @"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЮЯабвгдежзийклмнопрстуфхцчшщъьюя";
                    break;
                case "4":
                    pool = @"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЮЯабвгдежзийклмнопрстуфхцчшщъьюя1234567890";
                    break;
                case "5":
                    pool = @"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЮЯабвгдежзийклмнопрстуфхцчшщъьюя1234567890!@#$%^&*()_+{},./\|][";
                    break;
                default:
                    if (String.IsNullOrEmpty(flag) == false && String.IsNullOrWhiteSpace(flag) == false)
                    {
                        string newPool = "";
                        if (flag.ToLower().Contains("numeric")) { newPool += @"023456789"; }
                        if (flag.ToLower().Contains("alpha")) { newPool += @"абвгдежзийклмнопрстуфхцчшщъьюя"; }
                        if (flag.ToLower().Contains("upper")) { newPool += @"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЮЯ"; }
                        if (flag.ToLower().Contains("symbolic")) { newPool += @"!@#$%^&*()_+ {},./\|]["; }
                        if (newPool != "") { pool = newPool; }
                    }
                    break;
            }
            int length = CreateRandInt(minLenght, maxLenght);

            return CreateRandString(length, pool);
        }
        public static string CreatePassphraseEN(int minWords = 4, int maxWords = 6, int maxChars = 999)
        {
            int words = new Random().Next(minWords, maxWords);
            string[] ws = Informers.chopEnbeddedString("HttpVehicle.Data.Words_EN.txt");
            Random ran = new Random();
            string phrase = "";
            for (int i = 0; i < words; i++)
            {
                phrase += ws[ran.Next(0, ws.Count())];
            }
            if (phrase.Length > maxChars) { phrase = phrase.Substring(0, maxChars); }

            return ParsersExtractors.Passwordize(phrase) as string;
        }
        public static string CreatePassphraseBG(int minWords = 4, int maxWords = 6, int maxChars = 999)
        {
            int words = new Random().Next(minWords, maxWords);
            string[] ws = Informers.chopEnbeddedString("HttpVehicle.Data.Words_BG.txt");
            Random ran = new Random();
            string phrase = "";
            for (int i = 0; i < words; i++)
            {
                phrase += ws[ran.Next(0, ws.Count())];
            }
            if (phrase.Length > maxChars) { phrase = phrase.Substring(0, maxChars); }

            return ParsersExtractors.Passwordize(phrase) as string;
        }

        public static string CreateNameEN(int sex = 0, int fila = 2)
        {
            if (sex == 0)
            {
                sex = CreateRandInt(1, 3);
            }
            if (fila == 2)
            {
                string[] names;
                if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_UK.txt"); }
                else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_UK.txt"); }
                int ran = CreateRandInt(1, names.Count());
                string name = names[ran];

                string[] sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2_UK.txt");
                int ran2 = CreateRandInt(1, sirs.Count());
                string sir = sirs[ran2];

                return name + " " + sir;
            }
            else if (fila == 1)
            {
                string[] sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2_UK.txt");
                int ran2 = CreateRandInt(1, sirs.Count());
                string sir = sirs[ran2];

                return sir;
            }
            else if (fila == 0)
            {
                string[] names;
                if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_UK.txt"); }
                else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_UK.txt"); }
                int ran = CreateRandInt(1, names.Count());
                string name = names[ran];

                return name;
            }
            else return null;
        }           
        public static string CreateNameBG(int sex = 0, int fila = 2)
        {
            if (sex == 0)
            {
                sex = CreateRandInt(1, 3);
            }
            if (fila == 2)
            {
                string[] names;
                if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_BG.txt"); }
                else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_UK.txt"); }
                int ran = CreateRandInt(1, names.Count());
                string name = names[ran];

                string[] sirs;
                if (sex == 1) { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_BG.txt"); }
                else { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2F_BG.txt"); }
                int ran2 = CreateRandInt(1, sirs.Count());
                string sir = sirs[ran2];

                return name + " " + sir;
            }
            else if (fila == 1)
            {
                string[] names;
                if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_BG.txt"); }
                else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_UK.txt"); }
                int ran = CreateRandInt(1, names.Count());
                string name = names[ran];

                return name;
            }
            else if (fila == 0)
            {
                string[] sirs;
                if (sex == 1) { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_BG.txt"); }
                else { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2F_BG.txt"); }
                int ran2 = CreateRandInt(1, sirs.Count());
                string sir = sirs[ran2];

                return sir;
            }
            else return null;
        }           
        public static string CreateNameRU(int sex = 0, int fila = 2)
        {
            if (sex == 0)
            {
                sex = CreateRandInt(1, 3);
            }
            if (fila == 2)
            {
                string[] names;
                if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_RU.txt"); }
                else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_RU.txt"); }
                int ran = CreateRandInt(1, names.Count());
                string name = names[ran];

                string[] sirs;
                if (sex == 1) { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_RU.txt"); }
                else { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2F_RU.txt"); }
                int ran2 = CreateRandInt(1, sirs.Count());
                string sir = sirs[ran2];

                return name + " " + sir;
            }
            else if (fila == 1)
            {
                string[] names;
                if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_RU.txt"); }
                else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_RU.txt"); }
                int ran = CreateRandInt(1, names.Count());
                string name = names[ran];

                return name;
            }
            else if (fila == 0)
            {
                string[] sirs;
                if (sex == 1) { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_RU.txt"); }
                else { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2F_RU.txt"); }
                int ran2 = CreateRandInt(1, sirs.Count());
                string sir = sirs[ran2];

                return sir;
            }
            else return null;
        }
        public static string CreateNameHobbit(int sex = 0, int fila = 2)
        {
            if (sex == 0)
            {
                sex = CreateRandInt(1, 3);
            }
            if (fila == 2)
            {
                string[] names;
                if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_Hobbit_1M.txt"); }
                else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_Hobbit_2M.txt"); }
                int ran = CreateRandInt(1, names.Count());
                string name = names[ran];

                string[] sirs;
                if (sex == 1) { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_Hobbit_1M.txt"); }
                else { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_Hobbit_2M.txt"); }
                int ran2 = CreateRandInt(1, sirs.Count());
                string sir = sirs[ran2];

                return name + " " + sir;
            }
            else if (fila == 1)
            {
                string[] names;
                if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_Hobbit_1M.txt"); }
                else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_Hobbit_2M.txt"); }
                int ran = CreateRandInt(1, names.Count());
                string name = names[ran];

                return name;
            }
            else if (fila == 0)
            {
                string[] sirs;
                if (sex == 1) { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_Hobbit_1M.txt"); }
                else { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_Hobbit_2M.txt"); }
                int ran2 = CreateRandInt(1, sirs.Count());
                string sir = sirs[ran2];

                return sir;
            }
            else return null;
        }
        public static string CreateNameBGLatin(int sex = 0, int fila = 2)
        {
            if (sex == 0)
            {
                sex = CreateRandInt(1, 3);
            }
            string names = CreateNameBG(sex, fila);
            return ParsersExtractors.BGtoLatin(names) as string;
        }
        public static string CreateNameRULatin(int sex = 0, int fila = 2)
        {
            if (sex == 0)
            {
                sex = CreateRandInt(1, 3);
            }
            string names = CreateNameRU(sex, fila);
            return ParsersExtractors.RUtoLatin(names) as string;
        }

        public static string CreateUserNameEN(int sex = 0)
        {
            string user = CreateNameEN(sex).Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }
        public static string CreateUserNameBG(int sex = 0)
        {
            string user = CreateNameBG(sex).Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }
        public static string CreateUserNameRU(int sex = 0)
        {
            string user = CreateNameRU(sex).Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }
        public static string CreateUserNameHobbit(int sex = 0)
        {
            string user = CreateNameHobbit(sex).Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }
        public static string CreateUserNameBGLatin(int sex = 0)
        {
            string user = CreateNameBGLatin(sex).Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }
        public static string CreateUserNameRULatin(int sex = 0)
        {
            string user = CreateNameRULatin(sex).Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }

        public static string CreateEmailEN(string domain = "@gmail.com", int diverse = 2, int sex = 0)
        {
            if (!domain.Contains('@')) domain = "@" + domain;
            if (diverse == 1)
            {
                int fiorla = CreateRandInt(1, 3);
                string[] names = CreateNameEN(sex).Split(' ');
                return names[fiorla] + domain;
            }
            else if (diverse == 2)
            {
                string user = CreateNameEN(sex).Replace(" ", string.Empty).ToLower();
                return user + domain;
            }
            else if(diverse == 3)
            {
                int ye = CreateRandInt(50, 99);
                string user = CreateNameEN(sex).Replace(" ", string.Empty).ToLower();
                return user + ye + domain;
            }
            else
            {
                int day = CreateRandInt(1, 28);
                int month = CreateRandInt(1, 13);
                string user = CreateNameEN(sex).Replace(" ", string.Empty).ToLower();
                return user + day + month + domain;
            }
        }
        public static string CreateEmailBG(string domain = "@gmail.com", int diverse = 2, int sex = 0)
        {
            if (!domain.Contains('@')) domain = "@" + domain;
            if (diverse == 1)
            {
                int fiorla = CreateRandInt(1, 3);
                string[] names = CreateUserNameBGLatin(sex).Split(' ');
                return names[fiorla] + domain;
            }
            else if (diverse == 2)
            {
                string user = CreateUserNameBGLatin(sex).Replace(" ", string.Empty).ToLower();
                return user + domain;
            }
            else if (diverse == 3)
            {
                int ye = CreateRandInt(50, 99);
                string user = CreateUserNameBGLatin(sex).Replace(" ", string.Empty).ToLower();
                return user + ye + domain;
            }
            else
            {
                int day = CreateRandInt(1, 28);
                int month = CreateRandInt(1, 13);
                string user = CreateUserNameBGLatin(sex).Replace(" ", string.Empty).ToLower();
                return user + day + month + domain;
            }
        }
        public static string CreateEmailRU(string domain = "@gmail.com", int diverse = 2, int sex = 0)
        {
            if (!domain.Contains('@')) domain = "@" + domain;
            if (diverse == 1)
            {
                int fiorla = CreateRandInt(1, 3);
                string[] names = CreateUserNameBGLatin(sex).Split(' ');
                return names[fiorla] + domain;
            }
            else if (diverse == 2)
            {
                string user = CreateUserNameRULatin(sex).Replace(" ", string.Empty).ToLower();
                return user + domain;
            }
            else if (diverse == 3)
            {
                int ye = CreateRandInt(50, 99);
                string user = CreateUserNameRULatin(sex).Replace(" ", string.Empty).ToLower();
                return user + ye + domain;
            }
            else
            {
                int day = CreateRandInt(1, 28);
                int month = CreateRandInt(1, 13);
                string user = CreateUserNameRULatin(sex).Replace(" ", string.Empty).ToLower();
                return user + day + month + domain;
            }
        }

        public static string CreatePhoneBG()
        {
            try
            {
                return "+3598" + new Random().Next(7, 10).ToString() + new Random().Next(1000000, 9999999).ToString();
            }
            catch { return "error"; }
        }

        public static int CreateRandInt(int min, int max)
        {
            try
            {
                return Ran.Next(min, max);
            }
            catch { return max; }
        }
        public static string CreateRandString(int length, string pool = @"abcdefghijklmnopqrstuvwxyz")
        {
            try
            {
                string a = "";
                Random ran = new Random();
                for (int i = 1; i < length + 1; i++) { a = a + pool[ran.Next(0, pool.Length)]; }
                return a;
            }
            catch { return ""; }
        }
        public static string CreateRandHex(int length)
        {
            try
            {
                string pool = @"1234567890abcdef";
                string a = "";
                Random ran = new Random();
                for (int i = 1; i < length + 1; i++) { a = a + pool[ran.Next(0, pool.Length)]; }
                return a;
            }
            catch { return ""; }
        }
        public static string CreateRandUpperHex(int length)
        {
            try
            {
                string pool = @"1234567890ABCDEF";
                string a = "";
                Random ran = new Random();
                for (int i = 1; i < length + 1; i++) { a = a + pool[ran.Next(0, pool.Length)]; }
                return a;
            }
            catch { return ""; }
        }

        public static string CreateRandMD5Hash()
        {
            try
            {
                MD5 md5 = MD5.Create();
                byte[] inputBytes = Encoding.ASCII.GetBytes(CreateRandString(256));
                byte[] hash = md5.ComputeHash(inputBytes);


                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
            catch { return ""; }
        }
        public static string CreateRandSHA1Hash()
        {
            try
            {
                SHA1 md5 = SHA1.Create();
                byte[] inputBytes = Encoding.ASCII.GetBytes(CreateRandString(256));
                byte[] hash = md5.ComputeHash(inputBytes);


                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
            catch { return ""; }
        }
        public static string CreateRandSHA256Hash()
        {
            try
            {
                SHA256 md5 = SHA256.Create();
                byte[] inputBytes = Encoding.ASCII.GetBytes(CreateRandString(256));
                byte[] hash = md5.ComputeHash(inputBytes);


                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
            catch { return ""; }
        }
        public static string CreateRandSHA512Hash()
        {
            try
            {
                SHA512 md5 = SHA512.Create();
                byte[] inputBytes = Encoding.ASCII.GetBytes(CreateRandString(256));
                byte[] hash = md5.ComputeHash(inputBytes);


                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
            catch { return ""; }
        }

        public static string GetLinuxEpoch()
        {
            TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            return Math.Floor(span.TotalMilliseconds).ToString();
        }





        static string extractEnbeddedString(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            StreamReader str = new StreamReader(asm.GetManifestResourceStream(name));
            return str.ReadToEnd();
        }
        static string[] chopEnbeddedString(string name)
        {
            string str = extractEnbeddedString(name);
            string[] result = str.Split(new string[] { "\", \"" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Count(); i++)
            {
                result[i] = result[i].Replace("\"", "");
            }
            return result;
        }
    }
}

//sex  -> 0 - random; 1 - male; 2 - female;                             //fila -> 0 - first; 1 - last; 2 - both;