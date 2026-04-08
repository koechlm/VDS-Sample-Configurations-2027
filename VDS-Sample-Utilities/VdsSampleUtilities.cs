using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.Connectivity.WebServices;
using Autodesk.Connectivity.WebServicesTools;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Entities;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.PersistentId;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Properties;
using Inventor;
using VDF = Autodesk.DataManagement.Client.Framework;
using ACET = Autodesk.Connectivity.Explorer.ExtensibilityTools;
using ACW = Autodesk.Connectivity.WebServices;

namespace VdsSampleUtilities
{
    #region Text Encoding and Conversion Classes

    /// <summary>
    /// Provides System.Text.Encoding functionality not available in VDS PowerShell runtime.
    /// </summary>
    public class TextEncoding
    {
        #region Instance Methods

        /// <summary>
        /// Converts a string to a UTF8-encoded byte array.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>A UTF8-encoded byte array.</returns>
        /// <exception cref="ArgumentException">Thrown when input string is null or empty.</exception>
        public byte[] UTF8GetBytes(string value)
        {
            ValidateString(value, nameof(value));
            return System.Text.Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// Converts a string to an ASCII-encoded byte array.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>An ASCII-encoded byte array.</returns>
        /// <exception cref="ArgumentException">Thrown when input string is null or empty.</exception>
        public byte[] ASCIIGetBytes(string value)
        {
            ValidateString(value, nameof(value));
            return System.Text.Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// Converts a UTF8-encoded byte array to a string.
        /// </summary>
        /// <param name="bytes">The byte array to decode.</param>
        /// <returns>The decoded string.</returns>
        /// <exception cref="ArgumentException">Thrown when byte array is null or empty.</exception>
        public string UTF8GetString(byte[] bytes)
        {
            ValidateByteArray(bytes, nameof(bytes));
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Converts an ASCII-encoded byte array to a string.
        /// </summary>
        /// <param name="bytes">The byte array to decode.</param>
        /// <returns>The decoded string.</returns>
        /// <exception cref="ArgumentException">Thrown when byte array is null or empty.</exception>
        public string ASCIIGetString(byte[] bytes)
        {
            ValidateByteArray(bytes, nameof(bytes));
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Converts a string to a UTF8-encoded byte array (static method).
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>A UTF8-encoded byte array.</returns>
        /// <exception cref="ArgumentException">Thrown when input string is null or empty.</exception>
        public static byte[] GetUTF8Bytes(string value)
        {
            ValidateString(value, nameof(value));
            return System.Text.Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// Converts a string to an ASCII-encoded byte array (static method).
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>An ASCII-encoded byte array.</returns>
        /// <exception cref="ArgumentException">Thrown when input string is null or empty.</exception>
        public static byte[] GetASCIIBytes(string value)
        {
            ValidateString(value, nameof(value));
            return System.Text.Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// Converts a UTF8-encoded byte array to a string (static method).
        /// </summary>
        /// <param name="bytes">The byte array to decode.</param>
        /// <returns>The decoded string.</returns>
        /// <exception cref="ArgumentException">Thrown when byte array is null or empty.</exception>
        public static string GetUTF8String(byte[] bytes)
        {
            ValidateByteArray(bytes, nameof(bytes));
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Converts an ASCII-encoded byte array to a string (static method).
        /// </summary>
        /// <param name="bytes">The byte array to decode.</param>
        /// <returns>The decoded string.</returns>
        /// <exception cref="ArgumentException">Thrown when byte array is null or empty.</exception>
        public static string GetASCIIString(byte[] bytes)
        {
            ValidateByteArray(bytes, nameof(bytes));
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        #endregion

        #region Private Helper Methods

        private static void ValidateString(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Input string cannot be null or empty.", paramName);
            }
        }

        private static void ValidateByteArray(byte[] bytes, string paramName)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentException("Input byte array cannot be null or empty.", paramName);
            }
        }

        #endregion
    }

    /// <summary>
    /// Provides methods for converting between different data types.
    /// </summary>
    public class Convert
    {
        #region Static Methods

        /// <summary>
        /// Converts a string to a 32-bit signed integer.
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <returns>A 32-bit signed integer equivalent to the value in <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
        /// <exception cref="OverflowException">Thrown when <paramref name="value"/> represents a number less than <see cref="int.MinValue"/> or greater than <see cref="int.MaxValue"/>.</exception>
        public static int ToInt32(string value) => System.Convert.ToInt32(value);

        /// <summary>
        /// Converts a 32-bit signed integer to its equivalent string representation.
        /// </summary>
        /// <param name="value">The 32-bit signed integer to convert.</param>
        /// <returns>The string representation of the value of this instance.</returns>
        public static string Int32ToString(int value) => System.Convert.ToString(value);

        /// <summary>
        /// Converts a string to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <returns>A 64-bit signed integer equivalent to the value in <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
        /// <exception cref="OverflowException">Thrown when <paramref name="value"/> represents a number less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>.</exception>
        public static long ToInt64(string value) => System.Convert.ToInt64(value);

        /// <summary>
        /// Converts a 64-bit signed integer to its equivalent string representation.
        /// </summary>
        /// <param name="value">The 64-bit signed integer to convert.</param>
        /// <returns>The string representation of the value of this instance.</returns>
        public static string Int64ToString(long value) => System.Convert.ToString(value);

        /// <summary>
        /// Converts a string to a double-precision floating-point number.
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <returns>A double-precision floating-point number equivalent to the value in <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not a number in a valid format.</exception>
        /// <exception cref="OverflowException">Thrown when <paramref name="value"/> represents a number less than <see cref="double.MinValue"/> or greater than <see cref="double.MaxValue"/>.</exception>
        public static double ToDouble(string value) => System.Convert.ToDouble(value);

        /// <summary>
        /// Converts a double-precision floating-point number to its equivalent string representation.
        /// </summary>
        /// <param name="value">The double-precision floating-point number to convert.</param>
        /// <returns>The string representation of the value of this instance.</returns>
        public static string DoubleToString(double value) => System.Convert.ToString(value);

        /// <summary>
        /// Converts a byte array to its equivalent string representation encoded with base-64 digits.
        /// </summary>
        /// <param name="byteArray">An array of 8-bit unsigned integers.</param>
        /// <returns>The string representation in base 64 of the elements in <paramref name="byteArray"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="byteArray"/> is null.</exception>
        public static string ToBase64String(byte[] byteArray) => System.Convert.ToBase64String(byteArray);

        /// <summary>
        /// Converts a base-64 encoded string to an equivalent byte array.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>An array of 8-bit unsigned integers equivalent to <paramref name="s"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> is null.</exception>
        /// <exception cref="FormatException">Thrown when the length of <paramref name="s"/>, ignoring white-space characters, is not zero or a multiple of 4, or the format of <paramref name="s"/> is invalid.</exception>
        public static byte[] FromBase64String(string s) => System.Convert.FromBase64String(s);

        /// <summary>
        /// Converts a string to a 32-bit signed integer (alternative naming for PowerShell compatibility).
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <returns>A 32-bit signed integer equivalent to the value in <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
        /// <exception cref="OverflowException">Thrown when <paramref name="value"/> represents a number less than <see cref="int.MinValue"/> or greater than <see cref="int.MaxValue"/>.</exception>
        public static int StringToInt32(string value) => System.Convert.ToInt32(value);

        /// <summary>
        /// Converts a 32-bit signed integer to its equivalent string representation (static version).
        /// </summary>
        /// <param name="value">The 32-bit signed integer to convert.</param>
        /// <returns>The string representation of the value of this instance.</returns>
        public static string Int32ToStringStatic(int value) => System.Convert.ToString(value);

        /// <summary>
        /// Converts a string to a 64-bit signed integer (alternative naming for PowerShell compatibility).
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <returns>A 64-bit signed integer equivalent to the value in <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
        /// <exception cref="OverflowException">Thrown when <paramref name="value"/> represents a number less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>.</exception>
        public static long StringToInt64(string value) => System.Convert.ToInt64(value);

        /// <summary>
        /// Converts a 64-bit signed integer to its equivalent string representation (static version).
        /// </summary>
        /// <param name="value">The 64-bit signed integer to convert.</param>
        /// <returns>The string representation of the value of this instance.</returns>
        public static string Int64ToStringStatic(long value) => System.Convert.ToString(value);

        /// <summary>
        /// Converts a string to a double-precision floating-point number (alternative naming for PowerShell compatibility).
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <returns>A double-precision floating-point number equivalent to the value in <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not a number in a valid format.</exception>
        /// <exception cref="OverflowException">Thrown when <paramref name="value"/> represents a number less than <see cref="double.MinValue"/> or greater than <see cref="double.MaxValue"/>.</exception>
        public static double StringToDouble(string value) => System.Convert.ToDouble(value);

        /// <summary>
        /// Converts a double-precision floating-point number to its equivalent string representation (static version).
        /// </summary>
        /// <param name="value">The double-precision floating-point number to convert.</param>
        /// <returns>The string representation of the value of this instance.</returns>
        public static string DoubleToStringStatic(double value) => System.Convert.ToString(value);

        /// <summary>
        /// Converts a byte array to its equivalent string representation encoded with base-64 digits (alternative naming for PowerShell compatibility).
        /// </summary>
        /// <param name="byteArray">An array of 8-bit unsigned integers.</param>
        /// <returns>The string representation in base 64 of the elements in <paramref name="byteArray"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="byteArray"/> is null.</exception>
        public static string BytesToBase64(byte[] byteArray) => System.Convert.ToBase64String(byteArray);

        /// <summary>
        /// Converts a base-64 encoded string to an equivalent byte array (alternative naming for PowerShell compatibility).
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>An array of 8-bit unsigned integers equivalent to <paramref name="s"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> is null.</exception>
        /// <exception cref="FormatException">Thrown when the length of <paramref name="s"/>, ignoring white-space characters, is not zero or a multiple of 4, or the format of <paramref name="s"/> is invalid.</exception>
        public static byte[] Base64ToBytes(string s) => System.Convert.FromBase64String(s);

        #endregion
    }

    #endregion

    #region TreeNode Class

    /// <summary>
    /// Represents a node in the tree structure of file dependencies.
    /// </summary>
    public class TreeNode
    {
        private readonly Connection? _connection;
        private readonly Autodesk.Connectivity.WebServices.File? _file;
        private WebServiceManager? ServiceManager => _connection?.WebServiceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode"/> class.
        /// </summary>
        /// <param name="file">The file to be represented by this node.</param>
        /// <param name="connection">The connection to the Vault.</param>
        /// <exception cref="ArgumentNullException">Thrown when file or connection is null.</exception>
        public TreeNode(Autodesk.Connectivity.WebServices.File file, Connection connection)
        {
            _file = file ?? throw new ArgumentNullException(nameof(file));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>
        /// Gets the name of the file represented by this node.
        /// </summary>
        public string? Name => _file?.Name;

        /// <summary>
        /// Retrieves the children of the current file (files dependent on this file).
        /// </summary>
        public List<TreeNode> Children
        {
            get
            {
                ValidateServiceManagerAndFile();

                var fileAssociations = ServiceManager.DocumentService.GetLatestFileAssociationsByMasterIds(
                    new long[] { _file.MasterId },
                    FileAssociationTypeEnum.None,
                    false,
                    FileAssociationTypeEnum.Dependency,
                    false, false, false, false);

                return BuildTreeNodes(fileAssociations, assoc => assoc.CldFile);
            }
        }

        /// <summary>
        /// Retrieves the parents of the current file (files that depend on this file).
        /// </summary>
        public List<TreeNode> Parents
        {
            get
            {
                ValidateServiceManagerAndFile();

                var fileAssociations = ServiceManager.DocumentService.GetLatestFileAssociationsByMasterIds(
                    new long[] { _file.MasterId },
                    FileAssociationTypeEnum.Dependency,
                    false,
                    FileAssociationTypeEnum.None,
                    false, false, false, false);

                return BuildTreeNodes(fileAssociations, assoc => assoc.ParFile);
            }
        }

        /// <summary>
        /// Gets the file type's icon as a bitmap image.
        /// </summary>
        public BitmapImage? Icon
        {
            get
            {
                try
                {
                    var props = _connection.PropertyManager.GetPropertyDefinitions("FILE", null, PropertyDefinitionFilter.IncludeAll);
                    var def = props["EntityIcon"];
                    var fileIter = new FileIteration(_connection, _file);
                    
                    if (_connection.PropertyManager.GetPropertyValue((IEntity)fileIter, def, null) is not ImageInfo prop)
                    {
                        return null;
                    }

                    using (prop)
                    using (var ms = new MemoryStream())
                    {
                        prop.GetImage().Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        ms.Position = 0;

                        var bImg = new BitmapImage();
                        bImg.BeginInit();
                        bImg.StreamSource = ms;
                        bImg.CacheOption = BitmapCacheOption.OnLoad;
                        bImg.EndInit();
                        bImg.Freeze();
                        
                        return bImg;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        private void ValidateServiceManagerAndFile()
        {
            if (ServiceManager == null || _file == null)
            {
                throw new InvalidOperationException("WebServiceManager or File is null.");
            }
        }

        private List<TreeNode> BuildTreeNodes(FileAssocArray[] fileAssociations, Func<FileAssoc, Autodesk.Connectivity.WebServices.File> fileSelector)
        {
            var nodes = new List<TreeNode>();
            
            if (fileAssociations.FirstOrDefault()?.FileAssocs != null)
            {
                nodes.AddRange(fileAssociations.First().FileAssocs.Select(assoc => new TreeNode(fileSelector(assoc), _connection)));
            }

            return nodes;
        }
    }

    #endregion

    #region VltHelpers Class

    /// <summary>
    /// Provides helper methods for extending VDS Vault scripts.
    /// </summary>
    public class VltHelpers
    {
        private byte[]? _virtualCompThumbnail;
        private IEnumerable<object>? occurrences;

        /// <summary>
        /// Gets an image resource as a byte array in PNG format
        /// </summary>
        /// <param name="resourceName">The name of the image resource (e.g., "VirtualComp_32")</param>
        /// <returns>Byte array containing the image in PNG format, or an empty array if the resource cannot be loaded</returns>
        private static byte[] GetImageResourceAsByteArray(string resourceName)
        {
            try
            {
                var resourceManager = new System.Resources.ResourceManager(
                    "VDSSampleUtilities.Properties.Resources",
                    typeof(VltHelpers).Assembly);

                using (var bitmap = resourceManager.GetObject(resourceName) as System.Drawing.Bitmap)
                {
                    if (bitmap != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            return ms.ToArray();
                        }
                    }
                }
            }
            catch
            {
                // If resource loading fails, return empty array
            }

            return Array.Empty<byte>();
        }

        /// <summary>
        /// Gets an image from a local file as a byte array in PNG format
        /// </summary>
        /// <param name="filePath">The full path and filename of the image file</param>
        /// <param name="isFilePath">Must be set to true to indicate this is a file path (used to differentiate overloads)</param>
        /// <returns>Byte array containing the image in PNG format, or an empty array if the file cannot be loaded</returns>
        private static byte[] GetImageResourceAsByteArray(string filePath, bool isFilePath)
        {
            if (!isFilePath)
            {
                return GetImageResourceAsByteArray(filePath);
            }

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return Array.Empty<byte>();
                }

                using (var bitmap = new System.Drawing.Bitmap(filePath))
                {
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }
            catch
            {
                // If file loading fails, return empty array
            }

            return Array.Empty<byte>();
        }

        /// <summary>
        /// Creates user credentials for connecting to a Vault server.
        /// </summary>
        /// <param name="server">The IP address or DNS name of the ADMS server.</param>
        /// <param name="vault">The name of the Vault to connect to.</param>
        /// <param name="user">The username for authentication.</param>
        /// <param name="pw">The password for authentication.</param>
        /// <returns>A <see cref="Autodesk.Connectivity.WebServicesTools.UserPasswordCredentials"/> object for the specified server and Vault.</returns>
        public static Autodesk.Connectivity.WebServicesTools.UserPasswordCredentials UserCredentials1(string server, string vault, string user, string pw)
        {
            // Simplify object initialization and ensure platform compatibility
            var mServer = new ServerIdentities
            {
                DataServer = server,
                FileServer = server
            };

            return new Autodesk.Connectivity.WebServicesTools.UserPasswordCredentials(mServer, vault, user, pw);
        }

        /// <summary>
        /// UserCredentials1 and UserCredentials2 differentiate overloads as powershell can't handle
        /// UserCredentials2 returns readonly loginuser object
        /// </summary>
        /// <param name="server">IP Address or DNS Name of ADMS Server</param>
        /// <param name="vault">Name of vault to connect to</param>
        /// <param name="user">User name</param>
        /// <param name="pw">Password</param>
        /// <param name="rw">Set to "True" to allow Read/Write access</param>
        /// <returns></returns>
        public Autodesk.Connectivity.WebServicesTools.UserPasswordCredentials UserCredentials2(string server, string vault, string user, string pw, bool rw = true)
        {
            // Simplify object initialization and ensure platform compatibility
            var mServer = new ServerIdentities
            {
                DataServer = server,
                FileServer = server
            };

            return new Autodesk.Connectivity.WebServicesTools.UserPasswordCredentials(mServer, vault, user, pw, rw);
        }

        /// <summary>
        /// Deprecated - no longer required, as the overload is removed in 2017 API
        /// </summary>
        /// <param name="svc"></param>
        /// <param name="FldIds"></param>
        /// <param name="propArray"></param>
        /// <returns></returns>
        public Boolean UpdateFolderProp2(WebServiceManager svc, long[] FldIds, PropInstParamArray[] propArray)
        {
            try
            {
                svc.DocumentServiceExtensions.UpdateFolderProperties(FldIds, propArray);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// LinkManager.GetLinkedChildren has an override list; the input is of type IEntity. 
        /// This wrapper allows to input commonly known object types, like Ids and entity names instead.
        /// </summary>
        /// <param name="con">The utility dll is not connected to Vault; 
        /// we need to leverage the established connection to call LinkManager methods</param>
        /// <param name="mId">The parent entity's id to get linked children of</param>
        /// <param name="mClsId">The parent entity's class name; allowed values are FILE FLDR and CUSTENT. 
        /// CO and ITEM cannot have linked children, as they use specific links to related child objects.</param>
        /// <param name="mFilter">Limit the search on links to a particular class; providing an empty value "" will result in a search on all types</param>
        /// <returns>List of entity Ids</returns>
        public List<long>? mGetLinkedChildren1(Connection con, long mId, string mClsId, string mFilter)
        {
            IEnumerable<PersistableIdEntInfo> mEntInfo = new PersistableIdEntInfo[] { new PersistableIdEntInfo(mClsId, mId, true, false) };
            IDictionary<PersistableIdEntInfo, IEntity> mIEnts = con.EntityOperations.ConvertEntInfosToIEntities(mEntInfo);
            IEntity? mIEnt = null;
            try
            {
                foreach (var item in mIEnts)
                {
                    mIEnt = item.Value;
                }
                IEnumerable<IEntity> mLinkedChldrn = con.LinkManager.GetLinkedChildren(mIEnt, mFilter);
                //return mLinkedChldrn;
                List<long> mLinkedIds = new List<long>();
                foreach (var item in mLinkedChldrn)
                {
                    mLinkedIds.Add(item.EntityIterationId);
                }
                return mLinkedIds;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Evaluation of overload 2; see mGetLinkedchildren1 for detailed description
        /// </summary>
        /// <param name="con"></param>
        /// <param name="mParEntIds"></param>
        /// <param name="mClsIds"></param>
        /// <returns></returns>
        private static IEnumerable<IEntity>? GetLinkedChildren2(Connection con, long[] mParEntIds, string[] mClsIds)
        {
            List<PersistableIdEntInfo> mEntInfo = new List<PersistableIdEntInfo>();
            for (int i = 0; i < mParEntIds.Length; i++)
            {
                mEntInfo.Add(new PersistableIdEntInfo("CUSTENT", mParEntIds[i], true, false));
            }

            IDictionary<PersistableIdEntInfo, IEntity> mIEnts = con.EntityOperations.ConvertEntInfosToIEntities(mEntInfo.AsEnumerable());
            List<IEntity> mIEnt = new List<IEntity>();
            try
            {
                foreach (var item in mIEnts)
                {
                    mIEnt.Add(item.Value);
                }
                IEnumerable<IEntity> mLinkedChldrn = con.LinkManager.GetLinkedChildren(mIEnt.AsEnumerable(), mClsIds.AsEnumerable());
                return mLinkedChldrn;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Update file properties
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="mFile"></param>
        /// <param name="mPropDictonary"></param>
        /// <returns>True if updated successfully</returns>
        public bool mUpdateFileProperties(VDF.Vault.Currency.Connections.Connection conn,
            Autodesk.Connectivity.WebServices.File mFile, Dictionary<Autodesk.Connectivity.WebServices.PropDef, object> mPropDictonary)
        {
            try
            {
                ACET.IExplorerUtil mExplUtil = Autodesk.Connectivity.Explorer.ExtensibilityTools.ExplorerLoader.LoadExplorerUtil(
                                            conn.Server, conn.Vault, conn.UserID, conn.Ticket);

                mExplUtil.UpdateFileProperties(mFile, mPropDictonary);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Downloads Vault file using full file path, e.g. "$/Designs/Base.ipt". Returns full file name in local working folder (download enforces override, if local file exists),
        /// returns "FileNotFound if file does not exist at indicated location.
        /// Preset Options: Download Children (recursively) = Enabled, Enforce Overwrite = True
        /// </summary>
        /// <param name="conn">Current Vault Connection</param>
        /// <param name="VaultFullFileName">FullFilePath</param>
        /// <param name="CheckOut">Optional. File downloaded does NOT check-out as default.</param>
        /// <returns>Local path/filename or error statement "FileNotFound"</returns>
        public string mGetFileByFullFileName(VDF.Vault.Currency.Connections.Connection conn, string VaultFullFileName, bool CheckOut = false)
        {
            List<string> mFiles = new List<string>();
            mFiles.Add(VaultFullFileName);
            Autodesk.Connectivity.WebServices.File[] wsFiles = conn.WebServiceManager.DocumentService.FindLatestFilesByPaths(mFiles.ToArray());
            VDF.Vault.Currency.Entities.FileIteration mFileIt = new VDF.Vault.Currency.Entities.FileIteration(conn, (wsFiles[0]));

            VDF.Vault.Settings.AcquireFilesSettings settings = new VDF.Vault.Settings.AcquireFilesSettings(conn);
            if (CheckOut)
            {
                settings.DefaultAcquisitionOption = VDF.Vault.Settings.AcquireFilesSettings.AcquisitionOption.Checkout;
            }
            else
            {
                settings.DefaultAcquisitionOption = VDF.Vault.Settings.AcquireFilesSettings.AcquisitionOption.Download;
            }
            settings.OptionsRelationshipGathering.FileRelationshipSettings.IncludeChildren = true;
            settings.OptionsRelationshipGathering.FileRelationshipSettings.RecurseChildren = true;
            settings.OptionsRelationshipGathering.FileRelationshipSettings.VersionGatheringOption = VDF.Vault.Currency.VersionGatheringOption.Latest;
            settings.OptionsRelationshipGathering.IncludeLinksSettings.IncludeLinks = false;
            VDF.Vault.Settings.AcquireFilesSettings.AcquireFileResolutionOptions mResOpt = new VDF.Vault.Settings.AcquireFilesSettings.AcquireFileResolutionOptions();
            mResOpt.OverwriteOption = VDF.Vault.Settings.AcquireFilesSettings.AcquireFileResolutionOptions.OverwriteOptions.ForceOverwriteAll;
            mResOpt.SyncWithRemoteSiteSetting = VDF.Vault.Settings.AcquireFilesSettings.SyncWithRemoteSite.Always;
            settings.AddFileToAcquire(mFileIt, settings.DefaultAcquisitionOption);
            VDF.Vault.Results.AcquireFilesResults results = conn.FileManager.AcquireFiles(settings);
            if (results != null)
            {
                try
                {
                    VDF.Vault.Results.FileAcquisitionResult mFilesDownloaded = results.FileResults.Last();
                    return mFilesDownloaded.LocalPath.FullPath.ToString();
                }
                catch (Exception)
                {
                    return "FileFoundButDownloadFailed";
                }
            }
            return "FileNotFound";
        }


        /// <summary>
        /// Get the file iteration's properties with Display Names and Values
        /// </summary>
        /// <param name="conn">Current Vault connection ($VaultConnection)</param>
        /// <param name="FileId">File iteration Id</param>
        /// <param name="FileProperties">Name-Value map of Display Name and Values. All Values return as text.</param>
        public void GetFileProps(Connection conn, long FileId, ref Dictionary<string, string> FileProperties)
        {
            PropDef[]? mPropDefs = conn.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FILE");
            PropInst[]? mSourcePropInsts = conn.WebServiceManager.PropertyService.GetPropertiesByEntityIds("FILE", new long[] { FileId });
            string mPropDispName;
            string mPropVal;
            string mThumbnailDispName = mPropDefs.FirstOrDefault(n => n.SysName == "Thumbnail").DispName;
            foreach (PropInst mFilePropInst in mSourcePropInsts)
            {
                mPropDispName = mPropDefs.FirstOrDefault(n => n.Id == mFilePropInst.PropDefId).DispName;
                //filter thumbnail property
                if (mPropDispName != mThumbnailDispName)
                {
                    if (mFilePropInst.Val == null)
                    {
                        mPropVal = "";
                    }
                    else
                    {
                        mPropVal = mFilePropInst.Val.ToString();
                    }
                    FileProperties.Add(mPropDispName, mPropVal);
                }
            }
        }

        /// <summary>
        /// Get Folder properties with Display Names and Values
        /// </summary>
        /// <param name="conn">Current Vault connection ($VaultConnection)</param>
        /// <param name="FolderId">Folder Id</param>
        /// <param name="FolderProperties">Name-Value map of Display Name and Values. All Values return as text.</param>        
        public void GetFolderProps(Connection conn, long FolderId, ref Dictionary<string, string> FolderProperties)
        {
            PropDef[] mPropDefs = conn.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FLDR");
            PropInst[] mSourcePropInsts = conn.WebServiceManager.PropertyService.GetPropertiesByEntityIds("FLDR", new long[] { FolderId });
            string mPropDispName;
            string mPropVal;

            foreach (PropInst mFilePropInst in mSourcePropInsts)
            {
                mPropDispName = mPropDefs.Where(n => n.Id == mFilePropInst.PropDefId).FirstOrDefault().DispName;

                if (mFilePropInst.Val == null)
                {
                    mPropVal = "";
                }
                else
                {
                    mPropVal = mFilePropInst.Val.ToString();
                }
                FolderProperties.Add(mPropDispName, mPropVal);
            }
        }


        /// <summary>
        /// Get Item properties with Display Names and Values
        /// </summary>
        /// <param name="conn">Current Vault connection ($VaultConnection)</param>
        /// <param name="ItemId">Item Id</param>
        /// <param name="ItemProperties">Name-Value map of Display Name and Values. All Values return as text.</param>
        public void GetItemProps(Connection conn, long ItemId, ref Dictionary<string, string> ItemProperties)
        {
            PropDef[] mPropDefs = conn.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("ITEM");
            PropInst[] mSourcePropInsts = conn.WebServiceManager.PropertyService.GetPropertiesByEntityIds("ITEM", new long[] { ItemId });
            string mPropDispName;
            string mPropVal;
            string mThumbnailDispName = mPropDefs.Where(n => n.SysName == "Thumbnail").FirstOrDefault().DispName;
            foreach (PropInst mFilePropInst in mSourcePropInsts)
            {
                mPropDispName = mPropDefs.Where(n => n.Id == mFilePropInst.PropDefId).FirstOrDefault().DispName;
                //filter thumbnail property
                if (mPropDispName != mThumbnailDispName)
                {
                    if (mFilePropInst.Val == null)
                    {
                        mPropVal = "";
                    }
                    else
                    {
                        mPropVal = mFilePropInst.Val.ToString();
                    }
                    ItemProperties.Add(mPropDispName, mPropVal);
                }
            }
        }

        /// <summary>
        /// Get Custom Object properties with Display Names and Values
        /// </summary>
        /// <param name="conn">Current Vault connection ($VaultConnection)</param>
        /// <param name="CustentId">Custom Object Id</param>
        /// <param name="CustentProperties">Name-Value map of Display Name and Values. All Values return as text.</param>

        public void GetCustentProps(Connection conn, long CustentId, ref Dictionary<string, string> CustentProperties)
        {
            PropDef[] mPropDefs = conn.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("CUSTENT");
            PropInst[] mSourcePropInsts = conn.WebServiceManager.PropertyService.GetPropertiesByEntityIds("CUSTENT", new long[] { CustentId });
            string mPropDispName;
            string mPropVal;
            string mThumbnailDispName = mPropDefs.Where(n => n.SysName == "Thumbnail").FirstOrDefault().DispName;
            foreach (PropInst mFilePropInst in mSourcePropInsts)
            {
                mPropDispName = mPropDefs.Where(n => n.Id == mFilePropInst.PropDefId).FirstOrDefault().DispName;
                //filter thumbnail property, as iLogic RuleArguments will fail reading it.
                if (mPropDispName != mThumbnailDispName)
                {
                    if (mFilePropInst.Val == null)
                    {
                        mPropVal = "";
                    }
                    else
                    {
                        mPropVal = mFilePropInst.Val.ToString();
                    }
                    CustentProperties.Add(mPropDispName, mPropVal);
                }
            }
        }

        #region CAD-BOM methods
        /// <summary>
        /// Represents a single row in a Bill of Materials (BOM)
        /// </summary>
        public class BomRow
        {
            public int Position { get; set; }
            public string? PartNumber { get; set; }
            public string? ComponentType { get; set; }
            public float Quantity { get; set; }
            public string? Name { get; set; }
            public byte[]? Thumbnail { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? Material { get; set; }
            public string? FunctionalDesignation { get; set; }
        }

        /// <summary>
        /// Represents a Bill of Materials containing multiple BOM items
        /// </summary>
        public class Bom
        {
            public List<BomRow> BOMItems { get; set; } = new List<BomRow>();
        }

        /// <summary>
        /// Get model states or configurations from a file's BOM structure
        /// </summary>
        /// <param name="conn">Vault connection</param>
        /// <param name="fileId">File ID to get model states from</param>
        /// <returns>Dictionary of model state names and their IDs</returns>
        public Dictionary<string, long> GetModelStates(Connection conn, long fileId)
        {
            var mFileBOM = conn.WebServiceManager.DocumentService.GetBOMByFileId(fileId);
            var mFile = conn.WebServiceManager.DocumentService.GetFileById(fileId);

            var propDefs = conn.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FILE");
            var providerPropDef = propDefs.FirstOrDefault(n => n.SysName == "Provider");

            string mCadProvider = "Unknown";
            if (providerPropDef != null)
            {
                var providerProp = conn.WebServiceManager.PropertyService.GetProperties("FILE", new long[] { fileId }, new long[] { providerPropDef.Id })[0];
                var providerValue = providerProp.Val?.ToString();

                if (providerValue?.Contains("Inventor") == true)
                {
                    mCadProvider = "Inventor";
                }
                else if (providerValue?.Contains("SolidWorks") == true)
                {
                    mCadProvider = "SolidWorks";
                }
            }

            var msArray = new List<BOMComp>();

            if (mCadProvider == "SolidWorks")
            {
                msArray = mFileBOM.CompArray.Where(c =>
                    c.XRefId == -1 &&
                    c.UniqueId != null &&
                    c.UniqueId.Contains("@")
                ).ToList();
            }
            else if (mCadProvider == "Inventor")
            {
                msArray = mFileBOM.CompArray.Where(c =>
                    c.XRefId == -1 && (
                        (c.UniqueId != null && c.UniqueId.StartsWith("MS:")) ||
                        (c.Name != null && System.Text.RegularExpressions.Regex.IsMatch(c.Name, @"\[.*\]"))
                    )
                ).ToList();

                // Add the first component as [Primary] if it's not already in the list
                if (mFileBOM.CompArray.Length > 0)
                {
                    var firstComp = mFileBOM.CompArray[0];
                    if (firstComp.XRefId == -1 && !msArray.Contains(firstComp))
                    {
                        msArray.Insert(0, firstComp);
                    }
                }
            }

            var mMdlStates = new Dictionary<string, long>();

            if (msArray.Count > 1)
            {
                foreach (var comp in msArray)
                {
                    string mName = "";

                    if (mCadProvider == "SolidWorks")
                    {
                        if (comp.Name != null)
                        {
                            var nameParts = comp.Name.Split('@');
                            if (nameParts.Length == 2 && nameParts[1] == mFile.Name)
                            {
                                mName = nameParts[0];
                            }
                            else
                            {
                                mName = comp.Name;
                            }
                        }
                    }
                    else if (mCadProvider == "Inventor")
                    {
                        if (comp.Name != null && comp.Name.Contains(" (") && comp.Name.Contains(")"))
                        {
                            int startIndex = comp.Name.IndexOf(" (");
                            int endIndex = comp.Name.IndexOf(")");
                            if (startIndex >= 0 && endIndex > startIndex)
                            {
                                mName = comp.Name.Substring(startIndex + 2, endIndex - startIndex - 2);
                            }
                        }
                        else
                        {
                            mName = "[Primary]";
                        }
                    }

                    if (!string.IsNullOrEmpty(mName) && !mMdlStates.ContainsKey(mName))
                    {
                        mMdlStates.Add(mName, comp.Id);
                    }
                }
            }

            return mMdlStates;
        }

        /// <summary>
        /// Read the structured BOM (Inventor BOM: Structured = Enabled)
        /// </summary>
        /// <param name="conn">Vault connection</param>
        /// <param name="fileId">File ID</param>
        /// <param name="bomCompId">BOM Component ID (use root component or model state ID)</param>
        /// <param name="returnMessage"></param>
        /// <returns>List of BOM items</returns>
        public List<BomRow> GetFileBOM(Connection conn, long fileId, long bomCompId, ref bool structured, ref string returnMessage)
        {
            var bomItems = new List<BomRow>();
            ACW.BOM? mFileBom = null;
            try
            {
                mFileBom = conn.WebServiceManager.DocumentService.GetBOMByFileId(fileId);
            }
            catch (Exception)
            {
                // unhandled are changes in the BOM scheme, a new check-in of the file will resolve it in most cases
                returnMessage = "Could not read item data of the file. For legacy files, a new check-in of the file might resolve the issue.";
                return bomItems;
            }

            // return a message if the BOM is empty
            if (mFileBom == null)
            {
                returnMessage = "The file does not contain item data; use 'Extract Item Data' to update." +
                    " Note - iAssembly Factories don't display BOM data; select a member file instead.";
                return bomItems;
            }

            // return a message if the BOM exists without any active BOM rows
            if (mFileBom.InstArray.Length == 0)
            {
                returnMessage = "The file does not have active BOM rows.";
                return bomItems;
            }

            // check for structured BOM scheme and process it; if not found try to process the Model BOM scheme
            BOMSchm? schm = null;
            if (mFileBom.SchmArray != null)
            {
                try
                {
                    schm = mFileBom.SchmArray.FirstOrDefault(s => s.SchmTyp == SchemeTypeEnum.Structured && s.RootCompId == bomCompId);
                    // Only call ReadStructuredBom if schm is not null
                    if (schm != null)
                    {
                        ReadStructuredBom(conn, mFileBom, schm, bomItems);
                        structured = true;
                    }
                    else
                    {
                        // if no structured scheme is found, attempt to read the Model BOM structure (Inventor BOM: Model)
                        ReadModelBom(conn, mFileBom, bomItems);
                        structured = false;
                    }
                }
                catch (Exception) { }
            }
            else
            {
                // if no structured scheme is found, attempt to read the Model BOM structure (Inventor BOM: Model)
                ReadModelBom(conn, mFileBom, bomItems);
                structured = false;
            }

            // reset previously used variable to prevent unintended reuse
            occurrences = null;

            return bomItems.OrderBy(b => b.Position).ToList();
        }


        /// <summary>
        /// Read the model BOM
        /// </summary>
        /// <param name="conn">Vault connection</param>
        /// <param name="parentBom">BOM object retrieved from DocumentService.GetBOMByFileId</param>
        /// <param name="bomItems">List to populate with BOM items</param>
        private void ReadModelBom(Connection conn, ACW.BOM parentBom, List<BomRow> bomItems)
        {
            var propDefs = conn.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FILE");
            var thumbnailPropDef = propDefs.FirstOrDefault(n => n.SysName == "Thumbnail");

            var cldIds = new List<long>();

            // Get child IDs from instances where ParId equals 0
            var topLevelInsts = parentBom.InstArray?.Where(i => i.ParId == 0).ToList();
            if (topLevelInsts == null || !topLevelInsts.Any())
            {
                return;
            }

            foreach (var inst in topLevelInsts)
            {
                var comp = parentBom.CompArray?.FirstOrDefault(c => c.Id == inst.CldId);
                if (comp != null && comp.XRefId != -1)
                {
                    cldIds.Add(comp.XRefId);
                }
            }

            if (cldIds.Count == 0)
            {
                return;
            }

            ACW.BOM[]? cldBoms = conn.WebServiceManager.DocumentService.GetBOMByFileIds(cldIds.ToArray());
            var schm = parentBom.SchmArray?.FirstOrDefault(s => s.SchmTyp == SchemeTypeEnum.Structured && s.RootCompId == 0);

            int cldBomCounter = 0;

            foreach (var inst in topLevelInsts)
            {
                var bomItem = new BomRow();
                long cldId = inst.CldId;

                bomItem.Quantity = (float)(inst.QuantOverde == -1 ? inst.Quant : inst.QuantOverde);

                var comp = parentBom.CompArray?.FirstOrDefault(c => c.Id == cldId);
                if (comp == null) continue;

                if (schm != null)
                {
                    var occur = parentBom.SchmOccArray?.FirstOrDefault(o => o.SchmId == schm.Id && o.CompId == cldId);
                    if (occur != null)
                    {
                        bomItem.Position = int.TryParse(occur.DtlId, out int pos) ? pos : (int)occur.Id;
                    }
                }
                else
                {
                    bomItem.Position = cldBomCounter + 1;
                }

                ACW.BOM cldBom;
                if (comp.XRefId == -1)
                {
                    cldBom = parentBom;
                }
                else
                {
                    if (cldBoms != null && cldBomCounter < cldBoms.Length)
                    {
                        cldBom = cldBoms[cldBomCounter++];
                    }
                    else
                    {
                        continue;
                    }
                }

                string uniqueId = comp.UniqueId;
                var cldComp = cldBom.CompArray?.FirstOrDefault(c => c.UniqueId == uniqueId && c.XRefId == -1);
                if (cldComp == null && cldBom.CompArray != null && cldBom.CompArray.Length > 0)
                {
                    cldComp = cldBom.CompArray[0];
                }

                if (cldComp != null)
                {
                    bomItem.Name = cldComp.Name;
                    bomItem.ComponentType = cldComp.CompTyp.ToString();

                    var cldCompAttrArray = cldBom.CompAttrArray.Where(ca => ca.CompId == cldComp.Id).ToArray();
                    if (cldCompAttrArray.Length == 0)
                    {
                        cldCompAttrArray = cldBom.CompAttrArray;
                    }

                    if (cldCompAttrArray != null)
                    {
                        var propPartNumber = cldBom.PropArray?.FirstOrDefault(p => p.DispName == "Part Number");
                        if (propPartNumber != null)
                        {
                            var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == propPartNumber.Id);
                            if (prop != null)
                            {
                                bomItem.PartNumber = prop.Val;
                            }
                        }

                        if (cldComp.CompTyp != ComponentTypeEnum.Virtual)
                        {
                            propDefs = conn.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FILE");
                            thumbnailPropDef = propDefs.FirstOrDefault(n => n.SysName == "Thumbnail");

                            if (thumbnailPropDef != null && comp.XRefId != -1 && cldBomCounter > 0 && cldBomCounter <= cldIds.Count)
                            {
                                var thumbnailProp = conn.WebServiceManager.PropertyService.GetProperties("FILE",
                                    new long[] { cldIds[cldBomCounter - 1] },
                                    new long[] { thumbnailPropDef.Id })[0];
                                bomItem.Thumbnail = thumbnailProp.Val as byte[];
                            }
                        }
                        else
                        {
                            // Load virtual component thumbnail from embedded resource
                            if (_virtualCompThumbnail == null)
                            {
                                _virtualCompThumbnail = GetImageResourceAsByteArray("VirtualComp_32");
                            }

                            bomItem.Thumbnail = _virtualCompThumbnail;
                        }

                        var titleProp = cldBom.PropArray?.FirstOrDefault(p => p.DispName == "Title");
                        if (titleProp != null)
                        {
                            var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == titleProp.Id);
                            if (prop != null)
                            {
                                bomItem.Title = prop.Val;
                            }
                        }

                        var descProp = cldBom.PropArray?.FirstOrDefault(p => p.DispName == "Description");
                        if (descProp != null)
                        {
                            var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == descProp.Id);
                            if (prop != null)
                            {
                                bomItem.Description = prop.Val;
                            }
                        }

                        var matProp = cldBom.PropArray?.FirstOrDefault(p => p.DispName == "Material");
                        if (matProp != null)
                        {
                            var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == matProp.Id);
                            if (prop != null)
                            {
                                bomItem.Material = prop.Val;
                            }
                        }

                        // Function Designation is a bom row property in Vault, and optionally an instance property in Inventor; we need to to handle both cases to get the value if it exists
                        var funcProp = parentBom.PropArray.FirstOrDefault(p => p.DispName == "Functional Designation");
                        if (funcProp != null)
                        {
                            // we need to lookup the instance attribute matching the current instance id
                            if (parentBom?.InstArray?.Length >= 1)
                            {

                                var instArrayMatch = parentBom.InstArray.FirstOrDefault(i => i.Id == inst.Id);
                                if (instArrayMatch != null)
                                {
                                    var instProp = parentBom.InstPropArray.FirstOrDefault(p => p.InstId == instArrayMatch.Id);
                                    if (instProp != null && instProp.PropId == funcProp.Id)
                                    {
                                        bomItem.FunctionalDesignation = instProp.Val;
                                    }
                                    else // no instance property, check for a component property
                                    {
                                        var compProp = cldBom?.PropArray?.FirstOrDefault(p => p.DispName == "Functional Designation");
                                        if (compProp != null)
                                        {
                                            var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == compProp.Id);
                                            if (prop != null)
                                            {
                                                bomItem.FunctionalDesignation = prop.Val;
                                            }
                                        }
                                    }
                                }
                                else // no matching instance array, check for a component property
                                {
                                    var compProp = cldBom?.PropArray?.FirstOrDefault(p => p.DispName == "Functional Designation");
                                    if (compProp != null)
                                    {
                                        var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == compProp.Id);
                                        if (prop != null)
                                        {
                                            bomItem.FunctionalDesignation = prop.Val;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                bomItems.Add(bomItem);
            }
        }


        /// <summary>
        /// Process a BOM level (recursively if needed in future)
        /// </summary>
        private void ReadStructuredBom(Connection conn, ACW.BOM parentBom, ACW.BOMSchm schm, List<BomRow> bomItems)
        {
            // read the occurrences for the current level; filter on ParOccurId = -1 to get only the top-level occurrences for the given component or model state
            try
            {
                occurrences = parentBom.SchmOccArray.Where(o => o.SchmId == schm.Id && o.ParOccurId == -1).ToList();
                // return if no occurrences are found for the given BOM scheme
                if (occurrences == null || !occurrences.Any())
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }

            var cldIds = new List<long>();
            foreach (BOMSchmOccur occur in occurrences)
            {
                var comp = parentBom.CompArray.FirstOrDefault(c => c.Id == occur.CompId);
                if (comp != null && comp.XRefId != -1)
                {
                    cldIds.Add(comp.XRefId);
                }
            }

            ACW.BOM[]? cldBoms = null;
            if (cldIds.Count > 0)
            {
                cldBoms = conn.WebServiceManager.DocumentService.GetBOMByFileIds(cldIds.ToArray());
            }

            int cldBomCounter = 0;

            foreach (BOMSchmOccur occur in occurrences)
            {
                var comp = parentBom.CompArray.FirstOrDefault(c => c.Id == occur.CompId);
                if (comp == null) continue;

                var inst = parentBom.InstArray.FirstOrDefault(i => i.CldId == occur.CompId);
                if (inst == null) continue;

                ACW.BOM cldBom;
                if (comp.XRefId == -1)
                {
                    cldBom = parentBom;
                }
                else
                {
                    if (cldBoms != null && cldBomCounter < cldBoms.Length)
                    {
                        cldBom = cldBoms[cldBomCounter++];
                    }
                    else
                    {
                        continue;
                    }
                }

                var bomItem = new BomRow();

                bomItem.Quantity = (float)(inst.QuantOverde == -1 ? inst.Quant : inst.QuantOverde);

                if (int.TryParse(occur.DtlId, out int position))
                {
                    bomItem.Position = position;
                }
                else
                {
                    bomItem.Position = (int)occur.Id;
                }

                string uniqueId = comp.UniqueId;
                var cldComp = cldBom.CompArray.FirstOrDefault(c => c.UniqueId == uniqueId && c.XRefId == -1);
                if (cldComp == null && cldBom.CompArray.Length > 0)
                {
                    cldComp = cldBom.CompArray[0];
                }

                if (cldComp != null)
                {
                    bomItem.Name = cldComp.Name;
                    bomItem.ComponentType = cldComp.CompTyp.ToString();

                    var cldCompAttrArray = cldBom.CompAttrArray.Where(ca => ca.CompId == cldComp.Id).ToArray();
                    if (cldCompAttrArray.Length == 0)
                    {
                        cldCompAttrArray = cldBom.CompAttrArray;
                    }

                    var propPartNumber = cldBom.PropArray.FirstOrDefault(p => p.DispName == "Part Number");
                    if (propPartNumber != null)
                    {
                        var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == propPartNumber.Id);
                        if (prop != null)
                        {
                            bomItem.PartNumber = prop.Val;
                        }
                    }

                    if (cldComp.CompTyp != ComponentTypeEnum.Virtual)
                    {
                        var propDefs = conn.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FILE");
                        var thumbnailPropDef = propDefs.FirstOrDefault(n => n.SysName == "Thumbnail");

                        if (thumbnailPropDef != null && comp.XRefId != -1 && cldBomCounter > 0 && cldBomCounter <= cldIds.Count)
                        {
                            var thumbnailProp = conn.WebServiceManager.PropertyService.GetProperties("FILE",
                                new long[] { cldIds[cldBomCounter - 1] },
                                new long[] { thumbnailPropDef.Id })[0];
                            bomItem.Thumbnail = thumbnailProp.Val as byte[];
                        }
                    }
                    else
                    {
                        // Load virtual component thumbnail from embedded resource
                        if (_virtualCompThumbnail == null)
                        {
                            _virtualCompThumbnail = GetImageResourceAsByteArray("VirtualComp_32");
                        }

                        bomItem.Thumbnail = _virtualCompThumbnail;
                    }

                    var titleProp = cldBom.PropArray.FirstOrDefault(p => p.DispName == "Title");
                    if (titleProp != null)
                    {
                        var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == titleProp.Id);
                        if (prop != null)
                        {
                            bomItem.Title = prop.Val;
                        }
                    }

                    var descProp = cldBom.PropArray.FirstOrDefault(p => p.DispName == "Description");
                    if (descProp != null)
                    {
                        var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == descProp.Id);
                        if (prop != null)
                        {
                            bomItem.Description = prop.Val;
                        }
                    }

                    var matProp = cldBom.PropArray.FirstOrDefault(p => p.DispName == "Material");
                    if (matProp != null)
                    {
                        var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == matProp.Id);
                        if (prop != null)
                        {
                            bomItem.Material = prop.Val;
                        }
                    }

                    // Function Designation is a bom row property in Vault, and optionally an instance property in Inventor; we need to to handle both cases to get the value if it exists
                    var funcProp = parentBom.PropArray.FirstOrDefault(p => p.DispName == "Functional Designation");
                    if (funcProp != null)
                    {
                        // we need to lookup the instances matching the current occurrence that have an instance property with funcProp.PropId
                        if (parentBom.InstArray.Length >= 1)
                        {
                            // occurrences on structured BOM differ from occurrences within phantom subassemblies; we need different logic to find the matching instance for each case
                            var instArrayMatch = parentBom.InstArray.FirstOrDefault(i => i.SchemeOccurrenceId == occur.Id || i.Id == occur.Id);
                            if (instArrayMatch != null)
                            {
                                var instProp = parentBom.InstPropArray.FirstOrDefault(p => p.InstId == instArrayMatch.Id);
                                if (instProp != null && instProp.PropId == funcProp.Id)
                                {
                                    bomItem.FunctionalDesignation = instProp.Val;
                                }
                                else // no instance property, check for a component property
                                {
                                    var compProp = cldBom.PropArray.FirstOrDefault(p => p.DispName == "Functional Designation");
                                    if (compProp != null)
                                    {
                                        var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == compProp.Id);
                                        if (prop != null)
                                        {
                                            bomItem.FunctionalDesignation = prop.Val;
                                        }
                                    }
                                }
                            }
                            else // no matching instance array, check for a component property
                            {
                                var compProp = cldBom.PropArray.FirstOrDefault(p => p.DispName == "Functional Designation");
                                if (compProp != null)
                                {
                                    var prop = cldCompAttrArray.FirstOrDefault(ca => ca.PropId == compProp.Id);
                                    if (prop != null)
                                    {
                                        bomItem.FunctionalDesignation = prop.Val;
                                    }
                                }
                            }
                        }
                    }

                    // Check if we need to process nested BOM structure
                    // Add criteria here to determine if we should iterate cldBom occurrences
                    if (ShouldProcessNestedBOM(cldComp, cldBom))
                    {
                        var nestedSchm = cldBom.SchmArray.FirstOrDefault(s => s.SchmTyp == SchemeTypeEnum.Structured && s.RootCompId == cldComp.Id);
                        if (nestedSchm != null)
                        {
                            ReadStructuredBom(conn, cldBom, nestedSchm, bomItems);
                        }
                    }
                }

                bomItems.Add(bomItem);
            }
        }

        /// <summary>
        /// Determines if a nested BOM should be processed
        /// </summary>
        private bool ShouldProcessNestedBOM(ACW.BOMComp component, ACW.BOM bom)
        {
            // Add your criteria here to determine if nested iteration is needed
            // Example criteria:
            // - Component type check
            // - Specific property values
            // - Number of child components

            // Default: don't process nested BOMs
            return false;
        }

        #endregion CAD-BOM methods

    }

    #endregion

    #region InvHelpers Class

    /// <summary>
    /// Provides helper methods for interacting with hosting Inventor session.
    /// </summary>
    public class InvHelpers
    {
        private Inventor.Application? _inventorApp;
        private Inventor.Document? _document;
        private DrawingDocument? _drawingDoc;
        private PresentationDocument? _presentationDoc;
        private AssemblyDocument? _assemblyDoc;
        private PartDocument? _partDoc;
        private string? _modelPath;
        private CommandManager? _cmdManager;

        [System.Runtime.InteropServices.DllImport("User32.dll", SetLastError = true)]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        private const string FduAddInId = "{031C8B05-13C0-4C6C-B8FD-5A19DACCB64F}";
        private const string FdsLayoutInterest = "factory.filetype.factory_layout_template";
        private const string FdsAssetInterest = "factory.filetype.factory_asset";
        private const string FdsPropSetName = "autodesk.factory.inventor.DwgInv";
        private const string AssemblyPlaceComponentCmd = "AssemblyPlaceComponentCmd";

        #region Property Retrieval

        /// <summary>
        /// Retrieves property value of main view referenced model.
        /// </summary>
        /// <param name="inventorApp">Connect to the hosting instance of the VDS dialog.</param>
        /// <param name="viewModelFullName">Full name of the view model.</param>
        /// <param name="propertyName">Display name of the property.</param>
        /// <returns>The property value, or null if not found.</returns>
        public object? GetMainViewModelPropValue(object inventorApp, string viewModelFullName, string propertyName)
        {
            try
            {
                _inventorApp = (Inventor.Application)inventorApp;

                if (_inventorApp == null)
                {
                    return null;
                }

                _document = _inventorApp.Documents.Open(viewModelFullName, false);
                
                foreach (PropertySet propSet in _document.PropertySets)
                {
                    foreach (Property prop in propSet)
                    {
                        if (prop.Name == propertyName)
                        {
                            return prop.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to get property value: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Retrieve property value of the given Inventor file.
        /// </summary>
        /// <param name="inventorApp">Connect to the hosting instance of the VDS dialog $Application</param>
        /// <param name="fullFileName"></param>
        /// <param name="propertyName">Display Name</param>
        /// <returns></returns>
        public object? GetInventorPropertyValue(object inventorApp, String fullFileName, String propertyName)
        {
            try
            {
                _inventorApp = (Inventor.Application)inventorApp;
                _document = _inventorApp.Documents.Open(fullFileName, false);
                foreach (Inventor.PropertySet m_PropSet in _document.PropertySets)
                {
                    foreach (Inventor.Property m_Prop in m_PropSet)
                    {
                        if (m_Prop.Name == propertyName)
                        {
                            return m_Prop.Value;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        /// <summary>
        /// Gets the 3D model (ipt/iam/ipn) linked to the main view of the current drawing or presentation.
        /// </summary>
        /// <param name="inventorApp">Running host (instance of Inventor) of calling VDS Dialog.</param>
        /// <returns>Full filename (path and filename including extension) of the referenced model, or null if not found.</returns>
        public string? GetMainViewModelPath(object inventorApp)
        {
            try
            {
                _inventorApp = (Inventor.Application)inventorApp;

                if (_inventorApp == null)
                {
                    return null;
                }

                if (_inventorApp.ActiveDocumentType == DocumentTypeEnum.kDrawingDocumentObject)
                {
                    return GetDrawingModelPath();
                }

                if (_inventorApp.ActiveDocumentType == DocumentTypeEnum.kPresentationDocumentObject)
                {
                    return GetPresentationModelPath();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to get model path: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Gets the 3D model file path of the first Shrinkwrap Feature's referenced file.
        /// </summary>
        /// <param name="inventorApp">Running host (instance of Inventor) of calling VDS Dialog.</param>
        /// <returns>Returns the fullfilename (path\filename.ext) of the referenced model as string.</returns>
        public String? GetShrinkWrapParentFullFileName(object inventorApp)
        {
            try
            {
                _inventorApp = (Inventor.Application)inventorApp;

                if (_inventorApp == null)
                {
                    return null;
                }

                if (_inventorApp.ActiveDocumentType == DocumentTypeEnum.kPartDocumentObject)
                {
                    _partDoc = (PartDocument)_inventorApp.ActiveDocument;
                    PartComponentDefinition componentDefinition = _partDoc.ComponentDefinition;
                    ShrinkwrapComponent shrinkwrapComponent = componentDefinition.ReferenceComponents.ShrinkwrapComponents[1];
                    if ((shrinkwrapComponent?.ReferencedFile != null))
                    {
                        _modelPath = shrinkwrapComponent.ReferencedFile.FullFileName;
                        return _modelPath;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string? GetDrawingModelPath()
        {
            if (_inventorApp == null)
            {
                return null;
            }

            _drawingDoc = (DrawingDocument)_inventorApp.ActiveDocument;
            var sheet = _drawingDoc.ActiveSheet;
            
            if (sheet.DrawingViews.Count > 0)
            {
                var drawingView = sheet.DrawingViews[1];
                if (drawingView?.ReferencedFile != null)
                {
                    return drawingView.ReferencedFile.FullFileName;
                }
            }

            return null;
        }

        private string? GetPresentationModelPath()
        {
            if (_inventorApp == null)
            {
                return null;
            }

            _presentationDoc = (PresentationDocument)_inventorApp.ActiveDocument;
            
            if (_presentationDoc.ReferencedDocuments.Count >= 1)
            {
                return _presentationDoc.ReferencedDocuments[1].FullDocumentName;
            }

            return null;
        }

        #endregion

        #region Document Operations

        /// <summary>
        /// Deletes orphaned drawing sheets. Sheet format consuming workflows likely cause an unused Sheet1.
        /// </summary>
        /// <param name="inventorApp">Inventor Application.</param>
        /// <returns>False on unhandled errors, true otherwise.</returns>
        public bool RemoveOrphanedSheets(object inventorApp)
        {
            try
            {
                _inventorApp = (Inventor.Application)inventorApp;

                if (_inventorApp == null || _inventorApp.ActiveDocumentType != DocumentTypeEnum.kDrawingDocumentObject)
                {
                    return false;
                }

                _drawingDoc = (DrawingDocument)_inventorApp.ActiveDocument;
                var activeSheet = _drawingDoc.ActiveSheet;

                foreach (Sheet sheet in _drawingDoc.Sheets)
                {
                    if (sheet.DrawingViews.Count == 0 && sheet != activeSheet)
                    {
                        sheet.Delete(false);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to remove orphaned sheets: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets the running Inventor application instance.
        /// </summary>
        /// <returns>Inventor application instance, or null if not found.</returns>
        public Inventor.Application? GetInventorApplication()
        {
            try
            {
                return MarshalCore.GetActiveObject("Inventor.Application") as Inventor.Application;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to get Inventor application: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets the full filename of the active Inventor document.
        /// </summary>
        /// <param name="inventorApp">Inventor Application.</param>
        /// <returns>Full filename, or null if no active document.</returns>
        public string? GetActiveDocFullFileName(object inventorApp)
        {
            _inventorApp = (Inventor.Application)inventorApp;
            return _inventorApp?.ActiveDocument?.FullFileName;
        }

        /// <summary>
        /// Places a component in the active Inventor assembly document.
        /// Deprecated: VDS includes 'Insert to CAD' as a default.
        /// </summary>
        /// <param name="inventorApp">Inventor Application.</param>
        /// <param name="componentFullFileName">Full filename of the component to place.</param>
        [Obsolete("VDS includes 'Insert to CAD' as a default feature.")]
        public void PlaceComponent(object inventorApp, string componentFullFileName)
        {
            _inventorApp = (Inventor.Application)inventorApp;

            if (_inventorApp == null || _inventorApp.ActiveDocumentType != DocumentTypeEnum.kAssemblyDocumentObject)
            {
                return;
            }

            try
            {
                _cmdManager = _inventorApp.CommandManager;
                _cmdManager.PostPrivateEvent(PrivateEventTypeEnum.kFileNameEvent, componentFullFileName);
                
                var ctrlDef = (ControlDefinition)_cmdManager.ControlDefinitions[AssemblyPlaceComponentCmd];
                
                var windowHandle = (IntPtr)_inventorApp.MainFrameHWND;
                SwitchToThisWindow(windowHandle, true);
                
                ctrlDef.Execute();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to place component: {ex.Message}");
            }
        }

        #endregion

        #region Factory Design Utility Methods

        /// <summary>
        /// Validates whether the Factory Design Utility AddIn is active.
        /// </summary>
        /// <param name="inventorApp">Inventor Application.</param>
        /// <returns>True if FDU is active, false otherwise.</returns>
        public bool IsFDUActive(object inventorApp)
        {
            try
            {
                _inventorApp = (Inventor.Application)inventorApp;

                if (_inventorApp == null)
                {
                    return false;
                }

                var fduAddIn = _inventorApp.ApplicationAddIns.get_ItemById(FduAddInId);
                return fduAddIn?.Activated ?? false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to check FDU status: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Returns FDU key/value pairs to identify Factory Layout or Factory Asset files.
        /// </summary>
        /// <param name="inventorApp">Inventor Application.</param>
        /// <param name="fdsKeys">Empty dictionary to be populated.</param>
        /// <returns>Dictionary containing FDS keys and values.</returns>
        public Dictionary<string, string> GetFdsKeys(object inventorApp, Dictionary<string, string> fdsKeys)
        {
            try
            {
                _inventorApp = (Inventor.Application)inventorApp;

                if (_inventorApp == null)
                {
                    return fdsKeys;
                }

                _document = _inventorApp.ActiveDocument;

                if (_document == null)
                {
                    return fdsKeys;
                }

                ProcessFdsLayout(fdsKeys);
                ProcessFdsAsset(fdsKeys);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to get FDS keys: {ex.Message}");
            }

            return fdsKeys;
        }

        private void ProcessFdsLayout(Dictionary<string, string> fdsKeys)
        {
            if (!_document.DocumentInterests.HasInterest(FdsLayoutInterest))
            {
                return;
            }

            fdsKeys.Add("FdsType", "FDS-Layout");

            foreach (PropertySet propSet in _document.PropertySets)
            {
                if (propSet.Name != FdsPropSetName)
                {
                    continue;
                }

                foreach (Property prop in propSet)
                {
                    fdsKeys.Add(prop.Name, (string)prop.Value);
                }

                fdsKeys.Add("FdsNewFullFileName", _document.File.FullFileName);
                var fileInfo = new FileInfo(_document.File.FullFileName);
                fdsKeys.Add("FdsNewPath", fileInfo.Directory.FullName);
            }
        }

        private void ProcessFdsAsset(Dictionary<string, string> fdsKeys)
        {
            if (_document.DocumentInterests.HasInterest(FdsAssetInterest))
            {
                fdsKeys.Add("FdsType", "FDS-Asset");
            }
        }

        /// <summary>
        /// Returns custom iProperty set for AutoCAD files handled by Inventor FDU.
        /// </summary>
        /// <param name="inventorApp">Inventor Application.</param>
        /// <param name="fdsKeys">Empty dictionary to be populated.</param>
        /// <returns>Dictionary containing FDS AutoCAD properties.</returns>
        public Dictionary<string, string> GetFdsAcadProps(object inventorApp, Dictionary<string, string> fdsKeys)
        {
            Inventor.Document? dwgSource = null;
            var userOpenOpt = DefaultNonInventorDWGFileOpenBehaviorEnum.kRegularOpenNonInventorDWGFile;

            try
            {
                _inventorApp = (Inventor.Application)inventorApp;
                
                if (_inventorApp == null)
                {
                    return fdsKeys;
                }

                _document = _inventorApp.ActiveDocument;

                if (_document == null || !_document.DocumentInterests.HasInterest(FdsLayoutInterest))
                {
                    return fdsKeys;
                }

                fdsKeys.Add("FdsType", "FDS-Layout");

                foreach (PropertySet propSet in _document.PropertySets)
                {
                    if (propSet.Name != FdsPropSetName)
                    {
                        continue;
                    }

                    foreach (Property prop in propSet)
                    {
                        fdsKeys.Add(prop.Name, (string)prop.Value);
                    }

                    fdsKeys.Add("FdsNewFullFileName", _document.File.FullFileName);
                    var fileInfo = new FileInfo(_document.File.FullFileName);
                    var fdsPath = fileInfo.Directory.FullName;
                    fdsKeys.Add("FdsNewPath", fdsPath);

                    if (_document.FileSaveCounter >= 0)
                    {
                        dwgSource = OpenAndReadDwgProperties(fdsKeys, fdsPath, ref userOpenOpt);
                    }
                    else
                    {
                        fdsKeys.Add("FdsAcadProps", "Cannot retrieve properties before the calling file is saved.");
                    }
                }

                ProcessFdsAsset(fdsKeys);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to get FDS AutoCAD properties: {ex.Message}");
            }
            finally
            {
                if (dwgSource != null)
                {
                    dwgSource.Close(true);
                    if (_inventorApp != null)
                    {
                        _inventorApp.DrawingOptions.DefaultNonInventorDWGFileOpenBehavior = userOpenOpt;
                    }
                }
            }

            return fdsKeys;
        }

        private Inventor.Document? OpenAndReadDwgProperties(
            Dictionary<string, string> fdsKeys,
            string fdsPath,
            ref DefaultNonInventorDWGFileOpenBehaviorEnum userOpenOpt)
        {
            try
            {
                if (_inventorApp == null)
                {
                    return null;
                }

                var sourceFullFileName = System.IO.Path.Combine(fdsPath, fdsKeys["DwgFileName"]);
                userOpenOpt = _inventorApp.DrawingOptions.DefaultNonInventorDWGFileOpenBehavior;
                _inventorApp.DrawingOptions.DefaultNonInventorDWGFileOpenBehavior = 
                    DefaultNonInventorDWGFileOpenBehaviorEnum.kRegularOpenNonInventorDWGFile;
                
                var dwgSource = _inventorApp.Documents.Open(sourceFullFileName, false);

                foreach (PropertySet propSet in dwgSource.PropertySets)
                {
                    if (!propSet.DisplayName.Contains("Summary") && 
                        propSet.DisplayName != "User Defined Properties")
                    {
                        continue;
                    }

                    foreach (Property prop in propSet)
                    {
                        var value = prop.Value as string;
                        if (!string.IsNullOrEmpty(value))
                        {
                            fdsKeys.Add(prop.Name, value);
                        }
                    }
                }

                return dwgSource;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to open and read DWG properties: {ex.Message}");
                return null;
            }
        }

        #endregion
    }

    #endregion

    #region AcadHelpers Class

    /// <summary>
    /// Provides helper methods for interacting with hosting AutoCAD session.
    /// </summary>
    public class AcadHelpers
    {
        private AcadApplication? _acadApp;
        private AcadDocument? _acadDoc;

        [System.Runtime.InteropServices.DllImport("User32.dll", SetLastError = true)]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        private const string AutoCadProgId = "AutoCAD.Application";
        private const string FdsBlockPrefix = "FDS";

        /// <summary>
        /// Gets the AutoCAD session hosting.
        /// Deprecated as VDS >2017 dialogs share the hosting application object.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        [Obsolete("VDS >2017 dialogs share the hosting application object.")]
        private bool ConnectAcad()
        {
            try
            {
                _acadApp = MarshalCore.GetActiveObject(AutoCadProgId) as AcadApplication;
                return _acadApp != null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to connect to AutoCAD: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Checks for FDS blocks in AutoCAD drawings.
        /// </summary>
        /// <param name="acadApp">AutoCAD Application.</param>
        /// <returns>True if block names containing "FDS" are found.</returns>
        public bool IsFdsDrawing(object acadApp)
        {
            try
            {
                _acadApp = (AcadApplication)acadApp;
                _acadDoc = _acadApp.ActiveDocument;

                foreach (AcadBlock block in _acadDoc.Blocks)
                {
                    if (block.Name.Contains(FdsBlockPrefix))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to check FDS drawing: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Switches to the running AutoCAD application window.
        /// </summary>
        /// <param name="acadApp">AutoCAD Application.</param>
        private void GoToAcad(object acadApp)
        {
            try
            {
                _acadApp = (AcadApplication)acadApp;
                _acadDoc = _acadApp.ActiveDocument;
                var windowHandle = (IntPtr)_acadApp.HWND;
                SwitchToThisWindow(windowHandle, true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to switch to AutoCAD: {ex.Message}");
            }
        }
    }

    #endregion
}

