using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        ///<summary>
        /// The current exe path.
        ///</summary>
        public string pExepath
        {
            get { return IO.iExepath; }
        }
        ///<summary>
        /// The default save directory path.
        ///</summary>
        public string pSavepath
        {
            get { return IO.iSavedir; }
        }
        ///<summary>
        /// Current user desktop folder.
        ///</summary>
        public string pDesktoppath
        {
            get
            {
                return IO.iDesktoppath;
            }
        }
        ///<summary>
        /// String representing current datestamp.
        ///</summary>
        public string pDatestamp
        {
            get { return IO.iDatestamp; }
        }
        ///<summary>
        /// String representing the proxy address.
        ///</summary>
        public string pProxyAddress
        {
            get
            {
                return Memory._Proxy == null ? "127-0-0-1" : Memory._Proxy.Address.Host.Replace('.', '-');
            }
        }
        ///<summary>
        /// Current exe directory.
        ///</summary>
        public string pExedir
        {
            get
            {
                return IO.iExedir;
            }
        }
        ///<summary>
        /// System directory path.
        ///</summary>
        public string pSystemdir
        {
            get
            {
                return IO.iSystemdir;
            }
        }
        ///<summary>
        /// Printer directory path.
        ///</summary>
        public string pPrinterShortcutsdir
        {
            get
            {
                return IO.iPrinterShortcutsdir;
            }
        }
        ///<summary>
        /// Recent places directory path.
        ///</summary>
        public string pRecentdir
        {
            get
            {
                return IO.iRecentdir;
            }
        }
        ///<summary>
        /// MyComputer directory path.
        ///</summary>
        public string pMyComputerdir
        {
            get
            {
                return IO.iMyComputerdir;
            }
        }
        ///<summary>
        /// MyDocuments directory path.
        ///</summary>
        public string pMyDocumentsdir
        {
            get
            {
                return IO.iMyDocumentsdir;
            }
        }
        ///<summary>
        /// MyMusic directory path.
        ///</summary>
        public string pMyMusicdir
        {
            get
            {
                return IO.iMyMusicdir;
            }
        }
        ///<summary>
        /// MyPictures directory path.
        ///</summary>
        public string pMyPicturesdir
        {
            get
            {
                return IO.iMyPicturesdir;
            }
        }
        ///<summary>
        /// MyVideos directory path.
        ///</summary>
        public string pMyVideosdir
        {
            get
            {
                return IO.iMyVideosdir;
            }
        }
        ///<summary>
        /// Local AppData directory path.
        ///</summary>
        public string pLocalApplicationDatadir
        {
            get
            {
                return IO.iLocalApplicationDatadir;
            }
        }
        ///<summary>
        /// All users videos directory path.
        ///</summary>
        public string pCommonVideosdir
        {
            get
            {
                return IO.iCommonVideosdir;
            }
        }
        ///<summary>
        /// CD burning directory path.
        ///</summary>
        public string pCDBurningdir
        {
            get
            {
                return IO.iCDBurningdir;
            }
        }






        ///<summary>
        /// Save the current engine state to file, so it can be loaded later.
        /// If FileName property is not set, the FileName is a string, representing a timestamp.
        ///</summary>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileSave(string savePath = "")
        {
            if (!Initial("FileSave()")) return null;
            try
            {
                if (savePath == "")
                {
                    savePath = pSavepath + FileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += FileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Serialize(savePath, Memory);
                Success(FileName + " Saved!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Save the current engine state to file, so it can be loaded later.
        /// If file with the same name exists it won't be replaced, 
        /// but to the current filename will be added a number, 'filename(1).acc' e.g. 
        ///</summary>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileSaveSafe(string savePath = "")
        {
            if (!Initial("FileSaveSafe()")) return null;
            try
            {
                if (savePath == "")
                {
                    savePath = pSavepath + FileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += FileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                byte[] buff = IO.Serialize(Memory);
                IO.SaveSafe(savePath, buff);
                Success(FileName + " Saved!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Encrypt and save the current engine state to file, so it can be loaded later.
        /// If FileName property is not set, the FileName is a string, representing a timestamp.
        ///</summary>
        ///<param name="password">
        /// The password to encrypt the file.
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileSave(string password, string savePath = "")
        {
            if (!Initial("FileSave()")) return null;
            try
            {
                if (savePath == "")
                {
                    savePath = pSavepath + FileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += FileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                byte[] buff = IO.Serialize(Memory);
                byte[] crypted = IO.EncryptBytes(buff, password);
                IO.Save(savePath, crypted);
                Success(FileName + " Saved!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Encrypt and save the current engine state to file, so it can be loaded later.
        /// If file with the same name exists it won't be replaced, 
        /// but to the current filename will be added a number, 'filename(1).acc' e.g. 
        ///</summary>
        ///<param name="password">
        /// The password to encrypt the file.
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileSaveSafe(string password, string savePath = "")
        {
            if (!Initial("FileSaveSafe()")) return null;
            try
            {
                if (savePath == "")
                {
                    savePath = pSavepath + FileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += FileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                byte[] buff = IO.Serialize(Memory);
                byte[] crypted = IO.EncryptBytes(buff, password);
                IO.SaveSafe(savePath, crypted);
                Success(FileName + " Saved!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }

        ///<summary>
        /// Save to a file.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileExport(string fileName = "", string savePath = "")
        {
            if (!Initial("FileExport()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                object obj = GetS();
                if (obj is byte[] == false && obj is string == false && obj is string[] == false && obj is IEnumerable<object> == false)
                {
                    Fault("Invalid value!");
                    return savePath;
                }

                if (obj is string)
                {
                    string str = obj as string;
                    IO.Save(savePath, str);
                    Success(fileName + " Saved! " + str.Length.ToString() + " chars of data written!");
                    return savePath;
                }
                else if (obj is string[])
                {
                    string str = String.Join(Environment.NewLine, obj as string[]);
                    IO.Save(savePath, str);
                    Success(fileName + " Saved! " + (obj as string[]).Count().ToString() + " lines written!");
                    return savePath;
                }
                else if (obj is byte[])
                {
                    byte[] buff = obj as byte[];
                    IO.Save(savePath, buff);
                    Success(fileName + " Saved! " + buff.Count() + " bytes of data written!");
                    return savePath;
                }

                Fault("Invalid value!");
                return null;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Save to a file.
        ///</summary>
        ///<param name="s">
        /// The string to be saved.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileExport(string s, string fileName, string savePath = "")
        {
            if (!Initial("FileExport()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Save(savePath, s);
                Success(fileName + " Saved! " + s.Length.ToString() + " chars of data written!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Save object to a file.
        ///</summary>
        ///<param name="buff">
        /// The array of bytes to be saved.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileExport(byte[] buff, string fileName = "", string savePath = "")
        {
            if (!Initial("FileExport()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Save(savePath, buff);
                Success(fileName + " Saved! " + buff.Count() + " bytes of data written!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Save to a file.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be saved.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileExport(string[] s, string fileName = "", string savePath = "")
        {
            if (!Initial("FileExport()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Save(savePath, s);
                Success(fileName + " Saved! " + s.Count().ToString() + " lines written!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Save to a file.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be saved.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileExport(IList<string> s, string fileName = "", string savePath = "")
        {
            if (!Initial("FileExport()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Save(savePath, s);
                Success(fileName + " Saved! " + s.Count().ToString() + " lines written!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }

        ///<summary>
        /// Append to a file.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileAppend(string fileName = "", string savePath = "")
        {
            if (!Initial("FileAppend()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                object obj = GetS();
                if (obj is string == false && obj is string[] == false && obj is IEnumerable<object> == false)
                {
                    Fault("Invalid value!");
                    return null;
                }

                if (obj is string)
                {
                    string str = obj as string;
                    IO.Append(savePath, str);
                    Success(fileName + " Saved! " + str.Length.ToString() + " chars of data written!");
                    return savePath;
                }
                else if (obj is string[])
                {
                    string str = String.Join(Environment.NewLine, obj as string[]);
                    IO.Append(savePath, str);
                    Success(fileName + " Saved! " + (obj as string[]).Count().ToString() + " lines written!");
                    return savePath;
                }
                Fault("Invalid value!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Append to a file.
        ///</summary>
        ///<param name="s">
        /// The string to be appended.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileAppend(string s, string fileName, string savePath = "")
        {
            if (!Initial("FileAppend()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Append(savePath, s);
                Success(fileName + " Saved! " + s.Length.ToString() + " chars of data written!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Append to a file.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be appended.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileAppend(string[] s, string fileName = "", string savePath = "")
        {
            if (!Initial("FileAppend()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Append(savePath, s);
                Success(fileName + " Saved! " + s.Count().ToString() + " lines written!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Append to a file.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be appended.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileAppend(IList<string> s, string fileName = "", string savePath = "")
        {
            if (!Initial("FileAppend()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Append(savePath, s);
                Success(fileName + " Saved! " + s.Count().ToString() + " lines written!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }

        ///<summary>
        /// Append to a file if entity not allready present.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileAccumulate(string fileName = "", string savePath = "")
        {
            if (!Initial("FileAccumulate()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                object obj = GetS();
                if (obj is string == false && obj is string[] == false && obj is IEnumerable<object> == false)
                {
                    Fault("Invalid value!");
                    return savePath;
                }

                if (obj is string)
                {
                    string str = obj as string;
                    IO.Accumulate(savePath, str);
                    Success(fileName + " -> " + str.Length.ToString() + " chars of data processed!");
                    return savePath;
                }
                else if (obj is string[])
                {
                    string str = String.Join(Environment.NewLine, obj as string[]);
                    IO.Accumulate(savePath, str);
                    Success(fileName + " -> " + (obj as string[]).Count().ToString() + " lines processed!");
                    return savePath;
                }
                Fault("Invalid value!");
                return null;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Append to a file if entity not allready present.
        ///</summary>
        ///<param name="s">
        /// The string to be appended.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileAccumulate(string s, string fileName, string savePath = "")
        {
            if (!Initial("FileAccumulate()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Accumulate(savePath, s);
                Success(fileName + " -> " + s.Length.ToString() + " chars of data processed!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Append to a file if entity not allready present.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be appended.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileAccumulate(string[] s, string fileName = "", string savePath = "")
        {
            if (!Initial("FileAccumulate()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Accumulate(savePath, s);
                Success(fileName + " -> " + s.Count().ToString() + " lines processed!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Append to a file if entity not allready present.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be appended.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileAccumulate(IList<string> s, string fileName = "", string savePath = "")
        {
            if (!Initial("FileAccumulate()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Accumulate(savePath, s);
                Success(fileName + " -> " + s.Count().ToString() + " lines processed!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }

        ///<summary>
        /// Remove from a text file.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileRemove(string fileName = "", string savePath = "")
        {
            if (!Initial("FileRemove()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                object obj = GetS();
                if (obj is string == false && obj is string[] == false && obj is IEnumerable<object> == false)
                {
                    Fault("Invalid value!");
                    return savePath;
                }

                if (obj is string)
                {
                    string str = obj as string;
                    IO.Remove(savePath, str);
                    Success(fileName + " -> " + str.Length.ToString() + " chars of data processed!");
                    return savePath;
                }
                else if (obj is string[])
                {
                    string str = String.Join(Environment.NewLine, obj as string[]);
                    IO.Remove(savePath, str);
                    Success(fileName + " -> " + (obj as string[]).Count().ToString() + " lines processed!");
                    return savePath;
                }
                Fault("Invalid value!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove from a text file.
        ///</summary>
        ///<param name="s">
        /// The string to be removed.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileRemove(string s, string fileName, string savePath = "")
        {
            if (!Initial("FileRemove()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Remove(savePath, s);
                Success(fileName + " -> " + s.Length.ToString() + " chars of data processed!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove from a text file.
        ///</summary>
        ///<param name="s">
        /// The array of strings to be removed.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileRemove(string[] s, string fileName = "", string savePath = "")
        {
            if (!Initial("FileRemove()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Remove(savePath, s);
                Success(fileName + " -> " + s.Count().ToString() + " lines processed!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Remove from a text file.
        ///</summary>
        ///<param name="s">
        /// The collection of strings to be removed.
        ///</param>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention].
        ///</param>
        ///<param name="savePath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "pSavepath".
        ///</param>
        public string FileRemove(IList<string> s, string fileName = "", string savePath = "")
        {
            if (!Initial("FileRemove()")) return null;
            try
            {
                if (fileName == "") fileName = FileName;
                if (savePath == "")
                {
                    savePath = pSavepath + fileName;
                }
                else
                {
                    if (savePath[savePath.Length - 1] != '\\') savePath += '\\';
                    savePath += fileName;
                }
                if (Validators.ValidateWriteAccess(savePath) == false)
                {
                    Fault("You don't have write access!");
                    return savePath;
                }

                IO.Remove(savePath, s);
                Success(fileName + " -> " + s.Count().ToString() + " lines processed!");
                return savePath;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }

        ///<summary>
        /// Delete a file.
        ///</summary>
        ///<param name="path">
        /// The full path of the file or the relative one from pSavepath.
        ///</param>
        public string FileDelete(string path = "")
        {
            if (!Initial("FileDelete()")) return null;
            try
            {
                if (path == "")
                {
                    path = pSavepath + FileName;
                }
                else
                {
                    if (path[path.Length - 1] != '\\') path += '\\';
                    path += FileName;
                }
                if (!Directory.Exists(path) && Validators.ValidateWriteAccess(path) == false)
                {
                    Fault("You don't have write access!");
                    return path;
                }

                IO.Delete(path);
                Success(FileName + " Deleted!");
                return path;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return null;
            }
        }
        ///<summary>
        /// Scramble lines in text file.
        ///</summary>
        ///<param name="path">
        /// The full path of the file or the relative one from pSavepath.
        ///</param>
        public string FileScramble(string path = "")
        {
            if (!Initial("FileScramble()")) return null;
            try
            {
                if (path == "")
                {
                    path = pSavepath + FileName;
                }
                else
                {
                    if (path[path.Length - 1] != '\\') path += '\\';
                    path += FileName;
                }
                if (!Directory.Exists(path) && Validators.ValidateWriteAccess(path) == false)
                {
                    Fault("You don't have write access!");
                    return path; ;
                }

                IO.Scramble(path);
                Success(FileName + " Scrambled!");
                return path;
            }
            catch (Exception ex)
            {
                Exceptional(ex);
                return path;
            }
        }
    }
}
//Remove and accumulate in IO must return int in order to log how many lines/chars are changed
//IEnumerable/IList processing has bugs, probably Stack overflow!!