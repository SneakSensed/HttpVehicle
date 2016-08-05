using System;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        ///<summary>
        /// Validate a given string equals string.
        ///</summary>
        ///<param name="toCompare">
        /// The string to comparre to.
        ///</param>
        ///<param name="s">
        /// The string to be compared.
        ///</param>
        public bool ValidateStringEquals(string toCompare, string s = "")
        {
            if (!Initial("ValidateStringEquals()")) return false;

            if (s == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return false;
                }
                s = obj as string;
            }

            try
            {
                if (Validators.ValidateStringEquals(s, toCompare))
                {
                     Success();
                     return true;
                }
                Fault();
                return false;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return false;
            }
        }
        ///<summary>
        /// Validate a given string contains string.
        ///</summary>
        ///<param name="toCompare">
        /// The string to comparre to.
        ///</param>
        ///<param name="s">
        /// The string to be compared.
        ///</param>
        public bool ValidateStringContains(string toCompare, string s = "")
        {
            if (!Initial("ValidateStringContains()")) return false;

            if (s == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return false;
                }
                s = obj as string;
            }

            try
            {
                if (Validators.ValidateStringContains(s, toCompare))
                {
                    Success();
                    return true;
                }
                Fault();
                return false;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return false;
            }
        }
        ///<summary>
        /// Validate a given string length is in given boundaries.
        ///</summary>
        ///<param name="min">
        /// Minimum acceptable length.
        ///</param>
        ///<param name="max">
        /// Maximum acceptable length.
        ///</param>
        ///<param name="s">
        /// The string to be compared.
        ///</param>
        public bool ValidateStringLength(int min = 0, int max = 9999, string s = "")
        {
            if (!Initial("ValidateStringLength()")) return false;

            if (s == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return false;
                }
                s = obj as string;
            }

            try
            {
                if (Validators.ValidateStringLength(s, min, max))
                {
                    Success();
                    return true;
                }
                Fault();
                return false;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return false;
            }
        }
        ///<summary>
        /// Validate a given string is a valid URL.
        ///</summary>
        ///<param name="s">
        /// The string to be tested.
        ///</param>
        public bool ValidateUrl(string s = "")
        {
            if (!Initial("ValidateUrl()")) return false;

            if (s == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return false;
                }
                s = obj as string;
            }

            try
            {
                if (Validators.ValidateUrl(s))
                {
                    Success();
                    return true;
                }
                Fault();
                return false;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return false;
            }
        }
        ///<summary>
        /// Validate a given string is a valid Ip address.
        ///</summary>
        ///<param name="s">
        /// The string to be tested.
        ///</param>
        public bool ValidateIp(string s = "")
        {
            if (!Initial("ValidateIp()")) return false;

            if (s == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return false;
                }
                s = obj as string;
            }

            try
            {
                if (Validators.ValidateIp(s))
                {
                    Success();
                    return true;
                }
                Fault();
                return false;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return false;
            }
        }
        ///<summary>
        /// Validate a given string is a valid [Ip]:[port] pair.
        ///</summary>
        ///<param name="s">
        /// The string to be tested.
        ///</param>
        public bool ValidateIpPort(string s = "")
        {
            if (!Initial("ValidateIpPort()")) return false;

            if (s == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return false;
                }
                s = obj as string;
            }

            try
            {
                if (Validators.ValidateIpPort(s))
                {
                    Success();
                    return true;
                }
                Fault();
                return false;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return false;
            }
        }
        ///<summary>
        /// Validate a given host is up by pinging it.
        ///</summary>
        ///<param name="s">
        /// The name/ip of the host host to be tested.
        ///</param>
        public bool ValidateHostUp(string s = "")
        {
            if (!Initial("ValidateHostUp()")) return false;

            if (s == "")
            {
                object obj = GetS();
                if (obj is string == false)
                {
                    Fault("Invalid value!");
                    return false;
                }
                s = obj as string;
            }

            try
            {
                if (Validators.ValidateHostUp(s))
                {
                    Success();
                    return true;
                }
                Fault();
                return false;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return false;
            }
        }
    }
}
