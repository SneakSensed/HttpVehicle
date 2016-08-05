using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        ///<summary>
        /// Reset the FileName field to a string representing a timestamp.
        ///</summary>
        public void ResetFileName()
        {
            if (!Initial("ResetFileName()")) return;
            try
            {
                Memory._FileName = IO.iDatestamp;
                Success();
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Reset the CookieContainer for this instance of the engine.
        ///</summary>
        public void ResetJar()
        {
            if (!Initial("ResetJar()")) return;
            CookieJar = new CookieContainer();
            Success();
        }
        ///<summary>
        /// Reset the Log for this instance of the engine.
        ///</summary>
        public void ResetLog()
        {
            if (!Initial("ResetLog()")) return;
            try
            {
                Log = "";
                LogLine("ResetLog() : Ok");
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Reset the AdditionalHeaders to 'null' for this instance of the engine.
        ///</summary>
        public void ResetAdditionalHeaders()
        {
            if (!Initial("AdditionalHeaders()")) return;
            try
            {
                if (Memory._AdditionalHeaders != null)
                {
                    lock (Memory._AdditionalHeaders)
                    {
                        Memory._AdditionalHeaders = new Dictionary<string, string>();
                    }
                }
                else Memory._AdditionalHeaders = new Dictionary<string, string>();
                Success();
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Reset the User agent to 'null' for this instance of the engine.
        ///</summary>
        public void ResetUserAgent()
        {
            if (!Initial("ResetUserAgent()")) return;
            try
            {
                UserAgent = null;
                Success();
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Reset the Proxy to 'null' for this instance of the engine.
        ///</summary>
        public void ResetProxy()
        {
            if (!Initial("ResetProxy()")) return;
            try
            {
                Proxy = null;
                Success();
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Reset the Timeout to 7000 for this instance of the engine.
        ///</summary>
        public void ResetTimeout()
        {
            if (!Initial("ResetTimeout()")) return;
            try
            {
                Timeout = 7000;
                Success();
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Remove all items from persistent storage.
        ///</summary>
        public void ResetPersistent()
        {
            if (!Initial("ResetPersistent()")) return;
            try
            {
                ClrP();
                Success();
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Reset the Storage shelf for this instance of the engine.
        ///</summary>
        public void ResetStorage()
        {
            if (!Initial("ResetStorage()")) return;
            try
            {
                if (Memory._Storage != null)
                {
                    lock (Memory._Storage)
                    {
                        Memory._Storage = new List<object>();
                    }
                }
                else Memory._Storage = new List<object>();
                Success();
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }



        ///<summary>
        /// Provide a random UserAgent to be used with this instance of the engine.
        ///</summary>
        public string ProvideSetUserAgent()
        {
            if (!Initial("ProvideSetUserAgent()")) return null;
            try
            {
                string s = Informers.CreateUserAgent();
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }
                UserAgent = s;
                if (s.Length > 50) s = s.Substring(0, 50) + " ...";
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
        /// Provide a random UserAgent.
        ///</summary>
        public string ProvideUserAgent()
        {
            if (!Initial("ProvideUserAgent()")) return null;
            try
            {
                string s = Informers.CreateUserAgent();
                if (s == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(s);
                if (s.Length > 50) s = s.Substring(0, 50) + " ...";
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
        /// Provide a random password string.
        ///</summary>
        ///<param name="minLenght">
        /// Minimum length of the password.
        ///</param>
        ///<param name="maxLenght">
        /// Maximum length of the password.
        ///</param>
        public string ProvidePassword(int minLenght = 12, int maxLenght = 16)
        {
            if (!Initial("ProvidePassword()")) return null;
            try
            {
                string dt = Informers.CreatePassword(minLenght, maxLenght, "3");
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random password string.
        ///</summary>
        ///<param name="minLenght">
        /// Minimum length of the password.
        ///</param>
        ///<param name="maxLenght">
        /// Maximum length of the password.
        ///</param>
        public string ProvidePasswordComplex(int minLenght = 12, int maxLenght = 16)
        {
            if (!Initial("ProvidePasswordComplex()")) return null;
            try
            {
                string dt = Informers.CreatePassword(minLenght, maxLenght, "5");
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random password string.
        ///</summary>
        ///<param name="minLenght">
        /// Minimum length of the password.
        ///</param>
        ///<param name="maxLenght">
        /// Maximum length of the password.
        ///</param>
        public string ProvidePasswordAlpha(int minLenght = 12, int maxLenght = 16)
        {
            if (!Initial("ProvidePasswordAlpha()")) return null;
            try
            {
                string dt = Informers.CreatePassword(minLenght, maxLenght, "Alpha");
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random password string.
        ///</summary>
        ///<param name="minLenght">
        /// Minimum length of the password.
        ///</param>
        ///<param name="maxLenght">
        /// Maximum length of the password.
        ///</param>
        public string ProvidePasswordNumeric(int minLenght = 12, int maxLenght = 16)
        {
            if (!Initial("ProvidePasswordNumeric()")) return null;
            try
            {
                string dt = Informers.CreatePassword(minLenght, maxLenght, "Numeric");
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random password string.
        ///</summary>
        ///<param name="minLenght">
        /// Minimum length of the password.
        ///</param>
        ///<param name="maxLenght">
        /// Maximum length of the password.
        ///</param>
        public string ProvidePasswordAlphaNumeric(int minLenght = 12, int maxLenght = 16)
        {
            if (!Initial("ProvidePasswordAlphaNumeric()")) return null;
            try
            {
                string dt = Informers.CreatePassword(minLenght, maxLenght, "4");
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random password string using cyrillic letters.
        ///</summary>
        ///<param name="minLenght">
        /// Minimum length of the password.
        ///</param>
        ///<param name="maxLenght">
        /// Maximum length of the password.
        ///</param>
        public string ProvidePasswordCyrillic(int minLenght = 12, int maxLenght = 16)
        {
            if (!Initial("ProvidePasswordCyrillic()")) return null;
            try
            {
                string dt = Informers.CreatePasswordBG(minLenght, maxLenght, "3");
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random password string using cyrillic letters.
        ///</summary>
        ///<param name="minLenght">
        /// Minimum length of the password.
        ///</param>
        ///<param name="maxLenght">
        /// Maximum length of the password.
        ///</param>
        public string ProvidePasswordComplexCyrillic(int minLenght = 12, int maxLenght = 16)
        {
            if (!Initial("ProvidePasswordComplexCyrillic()")) return null;
            try
            {
                string dt = Informers.CreatePasswordBG(minLenght, maxLenght, "5");
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random password string using cyrillic letters.
        ///</summary>
        ///<param name="minLenght">
        /// Minimum length of the password.
        ///</param>
        ///<param name="maxLenght">
        /// Maximum length of the password.
        ///</param>
        public string ProvidePasswordNumericCyrillic(int minLenght = 12, int maxLenght = 16)
        {
            if (!Initial("ProvidePasswordNumericCyrillic()")) return null;
            try
            {
                string dt = Informers.CreatePasswordBG(minLenght, maxLenght, "Numeric");
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random password string using cyrillic letters.
        ///</summary>
        ///<param name="minLenght">
        /// Minimum length of the password.
        ///</param>
        ///<param name="maxLenght">
        /// Maximum length of the password.
        ///</param>
        public string ProvidePasswordAlphaNumericCyrillic(int minLenght = 12, int maxLenght = 16)
        {
            if (!Initial("ProvidePasswordAlphaNumericCyrillic()")) return null;
            try
            {
                string dt = Informers.CreatePasswordBG(minLenght, maxLenght, "4");
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random password phrase.
        ///</summary>
        ///<param name="minWords">
        /// Minimum words to be used.
        ///</param>
        ///<param name="maxWords">
        /// Maximum words to be used.
        ///</param>
        ///<param name="maxChars">
        /// How many characters the phrase can not exceed.
        ///</param>
        public string ProvidePassphrase(int minWords = 4, int maxWords = 6, int maxChars = 999)
        {
            if (!Initial("ProvidePassphrase()")) return null;
            try
            {
                string dt = Informers.CreatePassphraseEN(minWords, maxWords, maxChars);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random password phrase in cyrillic, using Bulgarian words.
        ///</summary>
        ///<param name="minWords">
        /// Minimum words to be used.
        ///</param>
        ///<param name="maxWords">
        /// Maximum words to be used.
        ///</param>
        ///<param name="maxChars">
        /// How many characters the phrase can not exceed.
        ///</param>
        public string ProvidePassphraseBG(int minWords = 4, int maxWords = 6, int maxChars = 999)
        {
            if (!Initial("ProvidePassphraseBG()")) return null;
            try
            {
                string dt = Informers.CreatePassphraseBG(minWords, maxWords, maxChars);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }

        ///<summary>
        /// Provide a random UK/US name.
        ///</summary>
        public string ProvideNameEN()
        {
            if (!Initial("ProvideNameEN()")) return null;
            try
            {
                string dt = Informers.CreateNameEN();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male UK/US name.
        ///</summary>
        public string ProvideNameENMale()
        {
            if (!Initial("ProvideNameENMale()")) return null;
            try
            {
                string dt = Informers.CreateNameEN(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female UK/US name.
        ///</summary>
        public string ProvideNameENFemale()
        {
            if (!Initial("ProvideNameENFemale()")) return null;
            try
            {
                string dt = Informers.CreateNameEN(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male UK/US first name.
        ///</summary>
        public string ProvideNameENMaleFirst()
        {
            if (!Initial("ProvideNameENMaleFirst()")) return null;
            try
            {
                string dt = Informers.CreateNameEN(1, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female UK/US first name.
        ///</summary>
        public string ProvideNameENFemaleFirst()
        {
            if (!Initial("ProvideNameENFemaleFirst()")) return null;
            try
            {
                string dt = Informers.CreateNameEN(2, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random UK/US last name.
        ///</summary>
        public string ProvideNameENLast()
        {
            if (!Initial("ProvideNameENLast()")) return null;
            try
            {
                string dt = Informers.CreateNameEN(0, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random Hobbit name.
        ///</summary>
        public string ProvideNameHobbit()
        {
            if (!Initial("ProvideNameHobbit()")) return null;
            try
            {
                string dt = Informers.CreateNameHobbit();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male Hobbit name.
        ///</summary>
        public string ProvideNameHobbitMale()
        {
            if (!Initial("ProvideNameHobbitMale()")) return null;
            try
            {
                string dt = Informers.CreateNameHobbit(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female Hobbit name.
        ///</summary>
        public string ProvideNameHobbitFemale()
        {
            if (!Initial("ProvideNameHobbitFemale()")) return null;
            try
            {
                string dt = Informers.CreateNameHobbit(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male Hobbit first name.
        ///</summary>
        public string ProvideNameHobbitMaleFirst()
        {
            if (!Initial("ProvideNameHobbitMaleFirst()")) return null;
            try
            {
                string dt = Informers.CreateNameHobbit(1, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female Hobbit first name.
        ///</summary>
        public string ProvideNameHobbitFemaleFirst()
        {
            if (!Initial("ProvideNameHobbitFemaleFirst()")) return null;
            try
            {
                string dt = Informers.CreateNameHobbit(2, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random Hobbit last name.
        ///</summary>
        public string ProvideNameHobbitLast()
        {
            if (!Initial("ProvideNameHobbitLast()")) return null;
            try
            {
                string dt = Informers.CreateNameHobbit(0, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG name in cyrillic.
        ///</summary>
        public string ProvideNameBG()
        {
            if (!Initial("ProvideNameBG()")) return null;
            try
            {
                string dt = Informers.CreateNameBG();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male BG name in cyrillic.
        ///</summary>
        public string ProvideNameBGMale()
        {
            if (!Initial("ProvideNameBGMale()")) return null;
            try
            {
                string dt = Informers.CreateNameBG(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female BG name in cyrillic.
        ///</summary>
        public string ProvideNameBGFemale()
        {
            if (!Initial("ProvideNameBGFemale()")) return null;
            try
            {
                string dt = Informers.CreateNameBG(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male BG name in cyrillic.
        ///</summary>
        public string ProvideNameBGMaleFirst()
        {
            if (!Initial("ProvideNameBGMaleFirst()")) return null;
            try
            {
                string dt = Informers.CreateNameBG(1, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female BG name in cyrillic.
        ///</summary>
        public string ProvideNameBGFemaleFirst()
        {
            if (!Initial("ProvideNameBGFemaleFirst()")) return null;
            try
            {
                string dt = Informers.CreateNameBG(2, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male BG name in cyrillic.
        ///</summary>
        public string ProvideNameBGMaleLast()
        {
            if (!Initial("ProvideNameBGMaleLast()")) return null;
            try
            {
                string dt = Informers.CreateNameBG(1, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female BG name in cyrillic.
        ///</summary>
        public string ProvideNameBGFemaleLast()
        {
            if (!Initial("ProvideNameBGFemaleLast()")) return null;
            try
            {
                string dt = Informers.CreateNameBG(2, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU name in cyrillic.
        ///</summary>
        public string ProvideNameRU()
        {
            if (!Initial("ProvideNameRU()")) return null;
            try
            {
                string dt = Informers.CreateNameRU();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male RU name in cyrillic.
        ///</summary>
        public string ProvideNameRUMale()
        {
            if (!Initial("ProvideNameRUMale()")) return null;
            try
            {
                string dt = Informers.CreateNameRU(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female RU name in cyrillic.
        ///</summary>
        public string ProvideNameRUFemale()
        {
            if (!Initial("ProvideNameRUFemale()")) return null;
            try
            {
                string dt = Informers.CreateNameRU(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male RU name in cyrillic.
        ///</summary>
        public string ProvideNameRUMaleFirst()
        {
            if (!Initial("ProvideNameRUMaleFirst()")) return null;
            try
            {
                string dt = Informers.CreateNameRU(1, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female RU name in cyrillic.
        ///</summary>
        public string ProvideNameRUFemaleFirst()
        {
            if (!Initial("ProvideNameRUFemaleFirst()")) return null;
            try
            {
                string dt = Informers.CreateNameRU(2, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male RU name in cyrillic.
        ///</summary>
        public string ProvideNameRUMaleLast()
        {
            if (!Initial("ProvideNameRUMaleLast()")) return null;
            try
            {
                string dt = Informers.CreateNameRU(1, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female RU name in cyrillic.
        ///</summary>
        public string ProvideNameRUFemaleLast()
        {
            if (!Initial("ProvideNameRUFemaleLast()")) return null;
            try
            {
                string dt = Informers.CreateNameRU(2, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG name in latin.
        ///</summary>
        public string ProvideNameBGLatin()
        {
            if (!Initial("ProvideNameBGLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameBGLatin();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male BG name in latin.
        ///</summary>
        public string ProvideNameBGMaleLatin()
        {
            if (!Initial("ProvideNameBGMaleLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameBGLatin(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female BG name in latin.
        ///</summary>
        public string ProvideNameBGFemaleLatin()
        {
            if (!Initial("ProvideNameBGFemaleLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameBGLatin(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male BG name in latin.
        ///</summary>
        public string ProvideNameBGMaleFirstLatin()
        {
            if (!Initial("ProvideNameBGMaleFirstLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameBGLatin(1, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female BG name in latin.
        ///</summary>
        public string ProvideNameBGFemaleFirstLatin()
        {
            if (!Initial("ProvideNameBGFemaleFirstLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameBGLatin(2, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male BG name in latin.
        ///</summary>
        public string ProvideNameBGMaleLastLatin()
        {
            if (!Initial("ProvideNameBGMaleLastLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameBGLatin(1, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female BG name in latin.
        ///</summary>
        public string ProvideNameBGFemaleLastLatin()
        {
            if (!Initial("ProvideNameBGFemaleLastLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameBGLatin(2, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU name in latin.
        ///</summary>
        public string ProvideNameRULatin()
        {
            if (!Initial("ProvideNameRULatin()")) return null;
            try
            {
                string dt = Informers.CreateNameRULatin();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male RU name in latin.
        ///</summary>
        public string ProvideNameRUMaleLatin()
        {
            if (!Initial("ProvideNameRUMaleLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameRULatin(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female RU name in latin.
        ///</summary>
        public string ProvideNameRUFemaleLatin()
        {
            if (!Initial("ProvideNameRUFemaleLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameRULatin(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male RU name in latin.
        ///</summary>
        public string ProvideNameRUMaleFirstLatin()
        {
            if (!Initial("ProvideNameRUMaleFirstLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameRULatin(1, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female RU name in latin.
        ///</summary>
        public string ProvideNameRUFemaleFirstLatin()
        {
            if (!Initial("ProvideNameRUFemaleFirstLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameRULatin(2, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random male RU name in latin.
        ///</summary>
        public string ProvideNameRUMaleLastLatin()
        {
            if (!Initial("ProvideNameRUMaleLastLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameRULatin(1, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random female RU name in latin.
        ///</summary>
        public string ProvideNameRUFemaleLastLatin()
        {
            if (!Initial("ProvideNameRUFemaleLastLatin()")) return null;
            try
            {
                string dt = Informers.CreateNameRULatin(2, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }

        ///<summary>
        /// Provide a random UK/US username.
        ///</summary>
        public string ProvideUsernameEN()
        {
            if (!Initial("ProvideUsernameEN()")) return null;
            try
            {
                string dt = Informers.CreateUserNameEN();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random UK/US male username.
        ///</summary>
        public string ProvideUsernameENMale()
        {
            if (!Initial("ProvideUsernameENMale()")) return null;
            try
            {
                string dt = Informers.CreateUserNameEN(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random UK/US female username.
        ///</summary>
        public string ProvideUsernameENFemale()
        {
            if (!Initial("ProvideUsernameENFemale()")) return null;
            try
            {
                string dt = Informers.CreateUserNameEN(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG username in cyrillic.
        ///</summary>
        public string ProvideUsernameBG()
        {
            if (!Initial("ProvideUsernameBG()")) return null;
            try
            {
                string dt = Informers.CreateUserNameBG();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG male username in cyrillic.
        ///</summary>
        public string ProvideUsernameBGMale()
        {
            if (!Initial("ProvideUsernameBGMale()")) return null;
            try
            {
                string dt = Informers.CreateUserNameBG(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG female username in cyrillic.
        ///</summary>
        public string ProvideUsernameBGFemale()
        {
            if (!Initial("ProvideUsernameBGFemale()")) return null;
            try
            {
                string dt = Informers.CreateUserNameBG(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU username in cyrillic.
        ///</summary>
        public string ProvideUsernameRU()
        {
            if (!Initial("ProvideUsernameRU()")) return null;
            try
            {
                string dt = Informers.CreateUserNameRU();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU male username in cyrillic.
        ///</summary>
        public string ProvideUsernameRUMale()
        {
            if (!Initial("ProvideUsernameRUMale()")) return null;
            try
            {
                string dt = Informers.CreateUserNameRU(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU female username in cyrillic.
        ///</summary>
        public string ProvideUsernameRUFemale()
        {
            if (!Initial("ProvideUsernameRUFemale()")) return null;
            try
            {
                string dt = Informers.CreateUserNameRU(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG username in latin.
        ///</summary>
        public string ProvideUsernameBGLatin()
        {
            if (!Initial("ProvideUsernameBGLatin()")) return null;
            try
            {
                string dt = Informers.CreateUserNameBGLatin();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG male username in latin.
        ///</summary>
        public string ProvideUsernameBGMaleLatin()
        {
            if (!Initial("ProvideUsernameBGMaleLatin()")) return null;
            try
            {
                string dt = Informers.CreateUserNameBGLatin(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG female username in latin.
        ///</summary>
        public string ProvideUsernameBGFemaleLatin()
        {
            if (!Initial("ProvideUsernameBGFemaleLatin()")) return null;
            try
            {
                string dt = Informers.CreateUserNameBGLatin(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU username in latin.
        ///</summary>
        public string ProvideUsernameRULatin()
        {
            if (!Initial("ProvideUsernameRULatin()")) return null;
            try
            {
                string dt = Informers.CreateUserNameRULatin();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU male username in latin.
        ///</summary>
        public string ProvideUsernameRUMaleLatin()
        {
            if (!Initial("ProvideUsernameRUMaleLatin()")) return null;
            try
            {
                string dt = Informers.CreateUserNameRULatin(1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU female username in latin.
        ///</summary>
        public string ProvideUsernameRUFemaleLatin()
        {
            if (!Initial("ProvideUsernameRUFemaleLatin()")) return null;
            try
            {
                string dt = Informers.CreateUserNameRULatin(2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }

        ///<summary>
        /// Provide a random UK/US email.
        ///</summary>
        ///<param name="domain">
        /// The domain name to be used, '@gmail.com' e.g.
        ///</param>
        ///<param name="diverse">
        /// The complexity of the emailname.
        /// 1 - one name or word;
        /// 2 - two names or words;
        /// 3 - two names or words + two digits year;
        /// > - two names or words + two digits month + 2 digits day;
        ///</param>
        public string ProvideEmailEN(string domain, int diverse = 2)
        {
            if (!Initial("ProvideEmailEN()")) return null;
            try
            {
                string dt = Informers.CreateEmailEN(domain, diverse, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random UK/US male email.
        ///</summary>
        ///<param name="domain">
        /// The domain name to be used, '@gmail.com' e.g.
        ///</param>
        ///<param name="diverse">
        /// The complexity of the emailname.
        /// 1 - one name or word;
        /// 2 - two names or words;
        /// 3 - two names or words + two digits year;
        /// > - two names or words + two digits month + 2 digits day;
        ///</param>
        public string ProvideEmailENMale(string domain, int diverse = 2)
        {
            if (!Initial("ProvideEmailENMale()")) return null;
            try
            {
                string dt = Informers.CreateEmailEN(domain, diverse, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random UK/US female email.
        ///</summary>
        ///<param name="domain">
        /// The domain name to be used, '@gmail.com' e.g.
        ///</param>
        ///<param name="diverse">
        /// The complexity of the emailname.
        /// 1 - one name or word;
        /// 2 - two names or words;
        /// 3 - two names or words + two digits year;
        /// > - two names or words + two digits month + 2 digits day;
        ///</param>
        public string ProvideEmailENFemale(string domain, int diverse = 2)
        {
            if (!Initial("ProvideEmailENFemale()")) return null;
            try
            {
                string dt = Informers.CreateEmailEN(domain, diverse, 2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG email.
        ///</summary>
        ///<param name="domain">
        /// The domain name to be used, '@gmail.com' e.g.
        ///</param>
        ///<param name="diverse">
        /// The complexity of the emailname.
        /// 1 - one name or word;
        /// 2 - two names or words;
        /// 3 - two names or words + two digits year;
        /// > - two names or words + two digits month + 2 digits day;
        ///</param>
        public string ProvideEmailBG(string domain, int diverse = 2)
        {
            if (!Initial("ProvideEmailBG()")) return null;
            try
            {
                string dt = Informers.CreateEmailBG(domain, diverse, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG male email.
        ///</summary>
        ///<param name="domain">
        /// The domain name to be used, '@gmail.com' e.g.
        ///</param>
        ///<param name="diverse">
        /// The complexity of the emailname.
        /// 1 - one name or word;
        /// 2 - two names or words;
        /// 3 - two names or words + two digits year;
        /// > - two names or words + two digits month + 2 digits day;
        ///</param>
        public string ProvideEmailBGMale(string domain, int diverse = 2)
        {
            if (!Initial("ProvideEmailBGMale()")) return null;
            try
            {
                string dt = Informers.CreateEmailBG(domain, diverse, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random BG female email.
        ///</summary>
        ///<param name="domain">
        /// The domain name to be used, '@gmail.com' e.g.
        ///</param>
        ///<param name="diverse">
        /// The complexity of the emailname.
        /// 1 - one name or word;
        /// 2 - two names or words;
        /// 3 - two names or words + two digits year;
        /// > - two names or words + two digits month + 2 digits day;
        ///</param>
        public string ProvideEmailBGFemale(string domain, int diverse = 2)
        {
            if (!Initial("ProvideEmailBGFemale()")) return null;
            try
            {
                string dt = Informers.CreateEmailBG(domain, diverse, 2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU email.
        ///</summary>
        ///<param name="domain">
        /// The domain name to be used, '@gmail.com' e.g.
        ///</param>
        ///<param name="diverse">
        /// The complexity of the emailname.
        /// 1 - one name or word;
        /// 2 - two names or words;
        /// 3 - two names or words + two digits year;
        /// > - two names or words + two digits month + 2 digits day;
        ///</param>
        public string ProvideEmailRU(string domain, int diverse = 2)
        {
            if (!Initial("ProvideEmailRU()")) return null;
            try
            {
                string dt = Informers.CreateEmailRU(domain, diverse, 0);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU male email.
        ///</summary>
        ///<param name="domain">
        /// The domain name to be used, '@gmail.com' e.g.
        ///</param>
        ///<param name="diverse">
        /// The complexity of the emailname.
        /// 1 - one name or word;
        /// 2 - two names or words;
        /// 3 - two names or words + two digits year;
        /// > - two names or words + two digits month + 2 digits day;
        ///</param>
        public string ProvideEmailRUMale(string domain, int diverse = 2)
        {
            if (!Initial("ProvideEmailRUMale()")) return null;
            try
            {
                string dt = Informers.CreateEmailRU(domain, diverse, 1);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random RU female email.
        ///</summary>
        ///<param name="domain">
        /// The domain name to be used, '@gmail.com' e.g.
        ///</param>
        ///<param name="diverse">
        /// The complexity of the emailname.
        /// 1 - one name or word;
        /// 2 - two names or words;
        /// 3 - two names or words + two digits year;
        /// > - two names or words + two digits month + 2 digits day;
        ///</param>
        public string ProvideEmailRUFemale(string domain, int diverse = 2)
        {
            if (!Initial("ProvideEmailRUFemale()")) return null;
            try
            {
                string dt = Informers.CreateEmailRU(domain, diverse, 2);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }

        ///<summary>
        /// Provide a random BG mobile phone number.
        ///</summary>
        public string ProvidePhoneBG()
        {
            if (!Initial("ProvidePhoneBG()")) return null;
            try
            {
                string dt = Informers.CreatePhoneBG();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }

        ///<summary>
        /// Provide a random integer.
        ///</summary>
        ///<param name="min">
        /// The inclusive minimum value.
        ///</param>
        ///<param name="max">
        /// The exclusive maximum value.
        /// Don't hate me, this is the way 'Random' class works in .net, 
        /// and it might be confusing if this behaviour is different here :)
        ///</param>
        public string ProvideRandomInt(int min, int max)
        {
            if (!Initial("ProvideRandomInt()")) return null;
            try
            {
                string dt = Informers.CreateRandInt(min, max).ToString();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random string.
        ///</summary>
        ///<param name="length">
        /// The length of the string.
        ///</param>
        ///<param name="pool">
        /// All the characters to pick randomly from.
        ///</param>
        public string ProvideRandomString(int length, string pool = @"abcdefghijklmnopqrstuvwxyz")
        {
            if (!Initial("ProvideRandomString()")) return null;
            try
            {
                string dt = Informers.CreateRandString(length, pool);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random string.
        ///</summary>
        ///<param name="length">
        /// The length of the string.
        ///</param>
        ///<param name="pool">
        /// All the characters to pick randomly from.
        ///</param>
        public string ProvideRandomStringAN(int length, string pool = @"AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwZxYyZz1234567890")
        {
            if (!Initial("ProvideRandomStringAN()")) return null;
            try
            {
                string dt = Informers.CreateRandString(length, pool);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random HEX string.
        ///</summary>
        ///<param name="length">
        /// The length of the string.
        ///</param>
        public string ProvideRandomHex(int length)
        {
            if (!Initial("ProvideRandomHex()")) return null;
            try
            {
                string dt = Informers.CreateRandHex(length);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random HEX string (Upper case).
        ///</summary>
        ///<param name="length">
        /// The length of the string.
        ///</param>
        public string ProvideRandomHexUpper(int length)
        {
            if (!Initial("ProvideRandomHexUpper()")) return null;
            try
            {
                string dt = Informers.CreateRandUpperHex(length);
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random MD5 hash.
        ///</summary>
        public string ProvideRandomMD5Hash()
        {
            if (!Initial("ProvideRandomMD5Hash()")) return null;
            try
            {
                string dt = Informers.CreateRandMD5Hash();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random SHA1 hash.
        ///</summary>
        public string ProvideRandomSHA1Hash()
        {
            if (!Initial("ProvideRandomSHA1Hash()")) return null;
            try
            {
                string dt = Informers.CreateRandSHA1Hash();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random SHA256 hash.
        ///</summary>
        public string ProvideRandomSHA256Hash()
        {
            if (!Initial("ProvideRandomSHA256Hash()")) return null;
            try
            {
                string dt = Informers.CreateRandSHA256Hash();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide a random SHA512 hash.
        ///</summary>
        public string ProvideRandomSHA512Hash()
        {
            if (!Initial("ProvideRandomSHA512Hash()")) return null;
            try
            {
                string dt = Informers.CreateRandSHA512Hash();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Provide Linux epoch string.
        /// This is the number of miliseconds passed since 1.1.1970.
        ///</summary>
        public string ProvideLinuxEpoch()
        {
            if (!Initial("ProvideLinuxEpoch()")) return null;
            try
            {
                string dt = Informers.GetLinuxEpoch();
                if (dt == null)
                {
                    Fault("Null");
                    return null;
                }
                AddS(dt);
                Success(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }



        ///<summary>
        /// Load previously saved engine state from file.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        public HttpEngine Load(string fileName)
        {
            if (!Initial("Load()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault(String.Format(" Invalid file name -> {0}", fileName));
                    return null;
                }

                object o = IO.Deserialize(fileName);
                if (o is MemorySet == false)
                {
                    Fault(String.Format(" Invalid or corrupt file -> {0}", fileName));
                    return null;
                }
                MemorySet mem = o as MemorySet;
                Memory = mem;
                Success(fileName);
                return this;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Load text file.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        public string LoadText(string fileName)
        {
            if (!Initial("LoadText()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault("Invalid file name!");
                    return null;
                }
                if (Validators.ValidateReadAccess(fileName) == false)
                {
                    Fault("You don't have read access!");
                    return null;
                }

                string dt = File.ReadAllText(fileName);

                AddS(dt);
                Success();
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Load text file lines as array of strings.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        public string[] LoadLines(string fileName)
        {
            if (!Initial("LoadLines()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault("Invalid file name!");
                    return null;
                }
                if (Validators.ValidateReadAccess(fileName) == false)
                {
                    Fault("You don't have read access!");
                    return null;
                }

                string[] dt = File.ReadAllLines(fileName);

                AddS(dt);
                Success();
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Load file bytes as array of bytes.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        public byte[] LoadFile(string fileName)
        {
            if (!Initial("LoadFile()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault("Invalid file name!");
                    return null;
                }
                if (Validators.ValidateReadAccess(fileName) == false)
                {
                    Fault("You don't have read access!");
                    return null;
                }

                byte[] dt = File.ReadAllBytes(fileName);

                AddS(dt);
                Success();
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Load first line from text file.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        public string PopData(string fileName)
        {
            if (!Initial("PopData()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault("Invalid file name!");
                    return null;
                }
                if (Validators.ValidateReadAccess(fileName) == false)
                {
                    Fault("You don't have read access!");
                    return null;
                }

                string dt  = IO.GetLineFromFile(fileName);

                AddS(dt);
                Success();
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Load random line from text file.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        public string PopDataRandom(string fileName)
        {
            if (!Initial("PopDataRandom()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault("Invalid file name!");
                    return null;
                }
                if (Validators.ValidateReadAccess(fileName) == false)
                {
                    Fault("You don't have read access!");
                    return null;
                }

                string dt = IO.GetRandomLineFromFile(fileName);

                AddS(dt);
                Success();
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Load first line from text file and set it as UserAgent.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        public string PopSetUserAgent(string fileName)
        {
            if (!Initial("PopSetUserAgent()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault("Invalid file name!");
                    return null;
                }
                if (Validators.ValidateReadAccess(fileName) == false)
                {
                    Fault("You don't have read access!");
                    return null;
                }

                string dt = IO.GetLineFromFile(fileName);

                UserAgent = dt;
                Success();
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Load random line from text file and set it as UserAgent.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        public string PopSetUserAgentRandom(string fileName)
        {
            if (!Initial("PopSetUserAgentRandom()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault("Invalid file name!");
                    return null;
                }
                if (Validators.ValidateReadAccess(fileName) == false)
                {
                    Fault("You don't have read access!");
                    return null;
                }

                string dt = IO.GetRandomLineFromFile(fileName);

                UserAgent = dt;
                Success();
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Load first line from text file and try to set it as Proxy.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        public string PopSetProxy(string fileName)
        {
            if (!Initial("PopSetUserAgent()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault("Invalid file name!");
                    return null;
                }
                if (Validators.ValidateReadAccess(fileName) == false)
                {
                    Fault("You don't have read access!");
                    return null;
                }

                string dt = IO.GetLineFromFile(fileName);

                ProxyString = dt;
                Success();
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Load random line from text file and try to set it as Proxy.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        public string PopSetProxyRandom(string fileName)
        {
            if (!Initial("PopSetUserAgentRandom()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault("Invalid file name!");
                    return null;
                }
                if (Validators.ValidateReadAccess(fileName) == false)
                {
                    Fault("You don't have read access!");
                    return null;
                }

                string dt = IO.GetRandomLineFromFile(fileName);

                ProxyString = dt;
                Success();
                return dt;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Load previously saved engine state from encrypted file.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file, or the path to that file.
        /// If a name is given, the file will be expected to be in "pSavepath".
        ///</param>
        ///<param name="password">
        /// The password to decrypt with.
        ///</param>
        public HttpEngine Load(string fileName, string password)
        {
            if (!Initial("Load()")) return null;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = pSavepath + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    Fault(String.Format(" Invalid file name -> {0}", fileName));
                    return null;
                }

                byte[] dt = File.ReadAllBytes(fileName);
                byte[] clear = IO.DecryptBytes(dt, password);

                object o = IO.Deserialize(clear);
                if (o is MemorySet == false)
                {
                    Fault(String.Format(" Invalid or corrupt file -> {0}", fileName));
                    return null;
                }
                MemorySet mem = o as MemorySet;
                Memory = mem;
                Success(fileName);
                return this;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }



        ///<summary>
        /// Set a cookie.
        ///</summary>
        ///<param name="name">
        /// The name of the cookie.
        ///</param>
        ///<param name="value">
        /// The value of the cookie.
        ///</param>
        ///<param name="expire">
        /// The expiry date of the cookie in 100 ms ticks since 01.01.0001 00:00:00.
        ///</param>
        ///<param name="domain">
        /// The domain of the cookie.
        ///</param>
        ///<param name="path">
        /// The path of the cookie.
        ///</param>
        public void SetCookie(string name, string value, string expire = "", string domain = "", string path = "")
        {
            if (!Initial("Cookie()")) return;
            try
            {
                if (Memory._AdditionalHeaders == null)
                {
                    Dictionary<string, string> ah = new Dictionary<string, string>();
                    ah.Add(name, value);
                    Memory._AdditionalHeaders = ah;
                }
                else
                {
                    lock (Memory._Jar)
                    {
                        Cookie c = new Cookie();
                        c.Name = name;
                        c.Value = value;
                        if (domain != "") c.Domain = domain;
                        if (path != "") c.Path = path;
                        if (expire != "")
                        {
                            try
                            {
                                DateTime d = new DateTime(Convert.ToInt64(expire));
                            }
                            catch { }
                        }
                        Memory._Jar.Add(c);
                    }
                }

                Success(String.Format("{0}:{1}", name, value));
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Set a header, that will be used once, in the next request.
        ///</summary>
        ///<param name="name">
        /// The name of the header.
        ///</param>
        ///<param name="value">
        /// The value of the header.
        ///</param>
        public void SetHeader(string name, string value)
        {
            if (!Initial("SetHeader()")) return;
            try
            {
                if (Memory._AdditionalHeaders == null)
                {
                    Dictionary<string, string> ah = new Dictionary<string, string>();
                    ah.Add(name, value);
                    Memory._AdditionalHeaders = ah;
                }
                else
                {
                    lock (Memory._AdditionalHeaders)
                    {
                        Memory._AdditionalHeaders.Add(name, value);
                    }
                }

                Success(String.Format("{0}:{1}", name, value));
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Set proxy server.
        ///</summary>
        ///<param name="namePort">
        /// The hostName/ip - port pair to be used in format [Ip]:[Port]
        ///</param>
        public string SetProxy(string namePort = "")
        {
            if (!Initial("SetProxy()")) return null;
            if (namePort == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return null;
                }
                else namePort = obj as string;
            }
            try
            {
                string[] sep = namePort.Split(':');
                Proxy = (sep.Count() > 3) ? new WebProxy(sep[0], Convert.ToInt32(sep[1])) {Credentials = new NetworkCredential(sep[2], sep[3])}
                                          : new WebProxy(sep[0], Convert.ToInt32(sep[1]));

                Success(String.Format("{0}:{1}", sep[0], sep[1]));
                return (sep.Count() > 3) ? String.Format("{0}:{1}:{2}:{3}", sep[0], sep[1], sep[2], sep[3]) : String.Format("{0}:{1}", sep[0], sep[1]);
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Set proxy server.
        ///</summary>
        ///<param name="namePort">
        /// The hostName/ip - port pair to be used in format [Ip]:[Port].
        ///</param>
        ///<param name="user">
        /// The userName to be used.
        ///</param>
        ///<param name="pass">
        /// The password to be used.
        ///</param>
        public void SetProxy(string namePort, string user, string pass)
        {
            if (!Initial("SetProxy()")) return;
            try
            {
                string[] sep = namePort.Split(':');
                Proxy = new WebProxy(sep[0], Convert.ToInt32(sep[1])) { Credentials = new NetworkCredential(user, pass) };

                Success(String.Format("{0}:{1}:{2}:{3}", sep[0], sep[1], user, pass));
            }
            catch (Exception ex)
            {
                Exceptional(ex);
            }
        }
        ///<summary>
        /// Set filename to be used with this instance of the engine.
        /// If not set, the filename will be a string representing a timestamp. T:HttpVehicleItems can be used.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention]
        ///</param>
        public string SetFileName(string fileName = "")
        {
            if (!Initial("SetFileName()")) return null;
            if (fileName == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return null;
                }
                else fileName = obj as string;
            }
            try
            {
                FileName = fileName;
                Success(fileName);
                return fileName;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Set the timeout for this instance of the engine.
        ///</summary>
        ///<param name="timeout">
        /// The timeout in milliseconds.
        ///</param>
        public void SetTimeout(int timeout = 7000)
        {
            if (!Initial("SetTimeout()")) return;
            Timeout = timeout;
            Success(timeout.ToString());
        }
        ///<summary>
        /// Set the UserAgent for this instance of the engine.
        ///</summary>
        ///<param name="userAgent">
        /// The UserAgent to be used.
        ///</param>
        public string SetUserAgent(string userAgent = "")
        {
            if (!Initial("SetUserAgent()")) return null;
            if (userAgent == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return null;
                }
                else userAgent = obj as string;
                Success(obj as string);
            }

            UserAgent = userAgent;
            Success(userAgent);
            return userAgent;
        }
        ///<summary>
        /// Add object to storage.
        ///</summary>
        ///<param name="data">
        /// The object to be set.
        ///</param>
        public void SetData(object data)
        {
            if (!Initial("SetData()")) return;

            try
            {
                AddS(data);
                Success();
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return;
            }
        }
    }
}
