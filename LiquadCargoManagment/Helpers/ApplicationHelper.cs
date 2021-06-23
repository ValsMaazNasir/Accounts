using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Microsoft.SqlServer.Server;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Caching;

namespace LiquadCargoManagment.Helpers
{
    public static class ApplicationHelper
    {
        #region Path
        public static string UserProfileImagePath = "~/assets/UserPofileImages/";
        public static string ApplicationLogo = "~/assets/Logo/";
        public static string VehicleDocuments = "~/assets/VehicleDocuments/";
        public static string DriverDocuments = "~/assets/DriverDocuments/";
        public static string EmployeeDocuments = "~/assets/EmployeeDocuments/";
        public static string EmployeeNICDocuments = "~/assets/EmployeeNICDocuments/";
        public static string FleetVehicleDocument = "~/assets/FleetVehicleDocument/";
        public static string OwnCompanyLogo = "~/assets/OwnCompanyLogo/";
        public static string ReceivingDocument = "~/assets/ReceivingDocument/";
        #endregion
        public interface IMyEntityInterface
        {
            long OwnCompanyId { get; set; }
        }

        public class MyEntity : IMyEntityInterface
        {
            public long OwnCompanyId { get; set; }
            //other stuff
        }

        #region Value Helper
        public const string EncryptKey = "FCSystem";
        #endregion

        #region UserSettings
        public static string ProfileImage { get; set; }
        public static long UserID { get; set; }
        public static string Username { get; set; }
        public static string Name { get; set; }
        public static long SubcriptionID { get; set; }
        public static long RoleID { get; set; }
        public static long OwnCompanyID { get; set; }
        public static List<RolePermission> lstRolePerm { get; set; }
        public static List<long?> lstAssignedCompanies { get; set; }
        public static List<string> lstBiltyRole { get; set; }

        #endregion
        #region CustomFunctions
        public static string UploadFile(HttpPostedFileBase file, string Path, HttpServerUtilityBase Server)
        {
            if (file != null)
            {
                file.SaveAs(System.IO.Path.Combine(Server.MapPath(Path), file.FileName));
                return file.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        public static long SplitNumberFromString(string input)
        {
            try
            {
                Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                Match result = re.Match(input);
                //string alphaPart = result.Groups[1].Value;
                return Convert.ToInt64(result.Groups[2].Value);
            }
            catch (Exception)
            {
                return 0;
            }

        }



        #endregion


        #region Encryption

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "abc123";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        #endregion
        #region "Encrypt Decrypt Helper"
        public static byte[] GetKey
        {
            get
            {
                return ASCIIEncoding.ASCII.GetBytes(EncryptKey);
            }
        }
        public static string IEncrypt(string _value)
        {
            string ReturnValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(_value))
            {
                ReturnValue = EncryptString(_value, GetKey);
            }
            return ReturnValue;
        }
        private static string EncryptString(string _value, byte[] _bytes)
        {
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(_bytes, _bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(_value);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }
        public static string IDecrypt(string _value)
        {
            string ReturnValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(_value))
            {
                ReturnValue = DecryptString(_value, GetKey);
            }
            return ReturnValue;
        }
        private static string DecryptString(string _value, byte[] _bytes)
        {
            _value = Regex.Replace(_value, "[ ]", "+");
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(_value));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(_bytes, _bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
        public static string CreatePassword(int _length)
        {
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder ReturnValue = new StringBuilder();
            Random rnd = new Random();
            while (0 < _length--)
            {
                ReturnValue.Append(characters[rnd.Next(characters.Length)]);
            }
            return ReturnValue.ToString();
        }
        public static string CreateTokenURL()
        {
            Random rnd = new Random();
            string str = rnd.Next(0, 10000) + DateTime.UtcNow.ToString();
            ASCIIEncoding AE = new ASCIIEncoding();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(AE.GetBytes(str));
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion

        #region "Cookie Helper"
        public static void AddCookie(string _key, string _value, int _numberOfHourAdd = 0)
        {
            System.Web.HttpCookie CookieObject = new System.Web.HttpCookie(_key);
            CookieObject.Value = _value;
            if (_numberOfHourAdd > 0)
            {
                CookieObject.Expires = DateTime.UtcNow.AddHours(_numberOfHourAdd);
            }
            HttpContext.Current.Response.Cookies.Add(CookieObject);
        }
        public static string GetCookie(string _key)
        {
            string ReturnValue = string.Empty;
            System.Web.HttpCookie CookieObject = HttpContext.Current.Request.Cookies[_key];
            if (CookieObject != null)
            {
                ReturnValue = CookieObject.Value;
            }
            return ReturnValue;
        }
        public static void RemoveCookie(string _key)
        {
            System.Web.HttpCookie CookieObject = HttpContext.Current.Request.Cookies[_key];
            if (CookieObject != null)
            {
                CookieObject.Expires = DateTime.UtcNow.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(CookieObject);
            }
        }

        #endregion

        #region testing Encrypt
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string xEncrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string xDecrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        #endregion

        public static class CustomRoles
        {
            #region Roles
            public static string SuperUser = "SUPER";
            public static string Save = "C";
            public static string Read = "R";
            public static string Update = "U";
            public static string Delete = "D";
            #endregion
        }

        public static class EnumBiltyRole
        {
            public const string User1 = "User1";
            public const string User2 = "User2";
            public const string User3 = "User3";
        }

        public static void AddWithTimeout(this HttpSessionStateBase session,
              string name,
              object value,
              TimeSpan expireAfter)
        {
            lock (session)
            {
                session[name] = value;
            }

            //add cleanup task that will run after "expire"
            Task.Delay(expireAfter).ContinueWith((task) => {
                lock (session)
                {
                    session.Remove(name);
                }
            });
        }

        public static List<SaleOrder> SearchSalesOrder(LCMEntities db, SearchOrder param)
        {
            var orders = db.SaleOrders.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId) && x.IsDeleted != true).AsQueryable();
            var ordersConditions = new List<IQueryable<SaleOrder>>();
            if (param.searchFrom != null)
            {
                if (param.searchTo != null)
                    ordersConditions.Add(orders.Where(s => s.CreatedDate >= param.searchFrom && param
                    .searchTo <= param.searchTo && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
                else
                    ordersConditions.Add(orders.Where(s => s.CreatedDate >= param.searchFrom && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.searchTo != null)
            {
                ordersConditions.Add(orders.Where(s => s.CreatedDate <= param.searchTo && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.LoadingPoint != null)
            {
                ordersConditions.Add(orders.Where(s => s.LoadingLocationId == param.LoadingPoint && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.GSTType != null && param.GSTType != "")
            {
                ordersConditions.Add(orders.Where(s => s.GSTType.Contains(param.GSTType) && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.Orderno != null)
            {
                if (param.ShipmentTypeId != null)
                {
                    if (param.ShipmentTypeId > 0)
                    {
                        if (db.ShipmentTypes.Where(x => x.ID == param.ShipmentTypeId).FirstOrDefault().Name.Contains("local"))
                        {
                            ordersConditions.Add(orders.Where(s => s.InvoiceNo == param.Orderno && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
                        }
                        else
                        {
                            ordersConditions.Add(orders.Where(s => s.SaleOrderNo == param.Orderno && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
                        }
                    }
                    else
                    {
                        ordersConditions.Add(orders.Where(s => s.SaleOrderNo == param.Orderno && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
                    }
                }
                else
                {
                    ordersConditions.Add(orders.Where(s => s.SaleOrderNo == param.Orderno && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
                }
            }
            if (param.vehicleId != null)
            {
                ordersConditions.Add(orders.Where(s => s.VehicleId == param.vehicleId && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.ShipmentTypeId != null)
            {
                ordersConditions.Add(orders.Where(s => s.ShipmentTypeId == param.ShipmentTypeId && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.Tax != null)
            {
                ordersConditions.Add(orders.Where(s => s.TaxID == param.Tax && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            switch (ordersConditions.Count)
            {
                case 0:
                    {
                        ordersConditions.Add(db.SaleOrders.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId) && x.IsDeleted != true).OrderByDescending(x => x.ID));
                        orders = ordersConditions[0];
                        break;
                    }
                case 1:
                    {
                        orders = ordersConditions[0];
                        break;
                    }
                default:
                    {
                        orders = ordersConditions.Aggregate((left, right) => left.Union(right));
                        break;
                    }
            }
            return orders.ToList();
        }

        public static List<SaleOrderChild> SearchBill(LCMEntities db, SearchOrder param)
        {
            var orders = db.SaleOrderChilds.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId) && x.IsDeleted != true).AsQueryable();
            var ordersConditions = new List<IQueryable<SaleOrderChild>>();
            if (param.searchFrom != null)
            {
                if (param.searchTo != null)
                    ordersConditions.Add(orders.Where(s => s.CreatedDate >= param.searchFrom && param
                    .searchTo <= param.searchTo && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
                else
                    ordersConditions.Add(orders.Where(s => s.CreatedDate >= param.searchFrom && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.searchTo != null)
            {
                ordersConditions.Add(orders.Where(s => s.CreatedDate <= param.searchTo && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.LoadingPoint != null)
            {
                ordersConditions.Add(orders.Where(s => s.LoadingLocationId == param.LoadingPoint && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.GSTType != null && param.GSTType != "")
            {
                ordersConditions.Add(orders.Where(s => s.GSTType.Contains(param.GSTType) && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.Orderno != null)
            {
                if (param.ShipmentTypeId != null)
                {
                    if (param.ShipmentTypeId > 0)
                    {
                        if (db.ShipmentTypes.Where(x => x.ID == param.ShipmentTypeId).FirstOrDefault().Name.Contains("local"))
                        {
                            ordersConditions.Add(orders.Where(s => s.InvoiceNo == param.Orderno && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
                        }
                        else
                        {
                            ordersConditions.Add(orders.Where(s => s.SaleOrderNo == param.Orderno && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
                        }
                    }
                    else
                    {
                        ordersConditions.Add(orders.Where(s => s.SaleOrderNo == param.Orderno && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
                    }
                }
                else
                {
                    ordersConditions.Add(orders.Where(s => s.SaleOrderNo == param.Orderno && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
                }
            }
            if (param.vehicleId != null)
            {
                ordersConditions.Add(orders.Where(s => s.VehicleId == param.vehicleId && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.ShipmentTypeId != null)
            {
                ordersConditions.Add(orders.Where(s => s.ShipmentTypeId == param.ShipmentTypeId && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            if (param.Tax != null)
            {
                ordersConditions.Add(orders.Where(s => s.TaxID == param.Tax && lstAssignedCompanies.Contains(s.OwnCompanyId) && s.IsDeleted != true));
            }
            switch (ordersConditions.Count)
            {
                case 0:
                    {
                        ordersConditions.Add(new List<SaleOrderChild>().AsQueryable());
                        orders = ordersConditions[0];
                        break;
                    }
                case 1:
                    {
                        orders = ordersConditions[0];
                        break;
                    }
                default:
                    {
                        orders = ordersConditions.Aggregate((left, right) => left.Union(right));
                        break;
                    }
            }
            return orders.ToList();
        }



        #region "Constant Values"
        public const string Car_Image_Path = "~/assets/cars";
        public const string User_Document_Path = "~/assets/userDocuments";


        public const string Cookie_User_Email_Address = "Cookie_CRM_Email_Address";
        public const string Cookie_User_Password = "Cookie_CRM_Password";
        public const string Session_User_Login = "Session_CRM_User_Login";
        public const string jQuery_Date_Format = "DD/MM/YYYY";
        public const string jQuery_Date_Time_Format = "DD/MM/YYYY HH:mm:ss";
        public const string Website_Date_Format = "dd MMMM yyyy";
        public const string Simple_Date_Format = "yyyy-MM-dd";
        public const string Website_Date_Format_With_Month_Letter = "dd/MMM/yyyy";
        public const string Website_Date_Format_With_Month_Only_Letter = "MMMM, yyyy";
        public const string Website_Date_Time_Format = "dd MMMM yyyy hh:mm tt";
      
        public static string[] allowedImageExtensions = { ".jpg", ".png", ".gif", ".jpeg" };
        public static string[] allowedResumeExtensions = { ".pdf", ".docx" };
        public static string[] allowedExcelExtensions = { ".xls", ".xlsx" };
        public static string[] DayNames = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public static Dictionary<int, string> DayDictionary = new Dictionary<int, string>() { { 1, "Monday" }, { 2, "Tuesday" }, { 3, "Wednesday" }, { 4, "Thursday" }, { 5, "Friday" }, { 6, "Saturday" }, { 7, "Sunday" } };
        public static string[] MonthNames = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        #endregion
        #region "Enum Functions"
        public static class EnumJQueryResponseType
        {
            public const string DataOnly = "D";
            public const string MessageOnly = "M";
            public const string FieldOnly = "F";
            public const string RedirectOnly = "T";
            public const string RefreshOnly = "R";
            public const string ReloadOnly = "RL";
            public const string MessageAndRedirect = "M-T";
            public const string MessageAndRedirectWithDelay = "M-TD";
            public const string MessageAndRefresh = "M-R";
            public const string MessageRefreshRedirect = "M-R-T";
            public const string MessageRefreshRedirectWithDelay = "M-R-TD";
            public const string RefreshAndRedirect = "R-T";
            public const string RefreshAndRedirectWithDelay = "R-TD";
            public const string RedirectWithDelay = "TD";
            public const string MessageAndReloadWithDelay = "M-RLD";
        }
        public static class EnumRole
        {
            public const int SuperAdministrator = 1;
            public const int Administrator = 3;
            public const int Member = 4;
        }

        public static class EnumParentTables
        {
            public const string AccountsHead = "AccountsHead";
            public const string GLTypes = "GLTypes";
            public const string SubAccounts = "SubAccounts";
        }
        public static class EnumStatus
        {
            public const string Enable = "Enable";
            public const string Disable = "Disable";
            public const string Approved = "Approved";
            public const string Rejected = "Rejected";
            public const string Pending = "Pending";
            public const string NotUploaded = "NotUploaded";
        }

        public static class EnumYesNo
        {
            public const string Yes = "Yes";
            public const string No = "No";
        }


        public static class EnumPageType
        {
            public const string Index = "Index";
            public const string Add = "Add";
            public const string Edit = "Edit";
            public const string View = "View";
        }
        public static class EnumMenuType
        {
            public const string All = "All";
            public const string Both = "Both";
            public const string SuperAdmin = "Super Admin";
            public const string Admin = "Admin";
            public const string Member = "Member";
        }
        public static class EnumPriority
        {
            public const string High = "High";
            public const string Medium = "Medium";
            public const string Low = "Low";
            public const string Closed = "Closed";
            public const string VisitingOffice = "Visiting Office";
        }

        public static class EnumPolicyType
        {
            public const string Engineering = "Engineering";
            public const string Power = "Power";
            public const string Inspect = "InspectTest";
            public const string Chemical = "Chemical";
        }
        public static class EnumPolicyCategory
        {
            public const string Exclusive = "Exclusive";
            public const string Corporate = "Corporate";
            public const string Premium = "Premium";
        }
        #endregion
        #region "Core Functions"
        public class AjaxResponse
        {
            public bool Success { get; set; }
            public string Type { get; set; }
            public string FieldName { get; set; }
            public string Message { get; set; }
            public string TargetURL { get; set; }
            public string Data { get; set; }
        }
        public static List<string> AllowedUrlList()
        {
            List<string> PageList = new List<string>();
            PageList.Add("dashboard");
            PageList.Add("dashboard/logout");
            PageList.Add("dashboard/profiles");
            PageList.Add("dashboard/settings");
            PageList.Add("dashboard/accessunauthorized");
            return PageList;
        }
        public static DateTime GetDateTime()
        {
            string timeZoneId = "Pakistan Standard Time";
            TimeZoneInfo time_zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, time_zone);
        }
        public static DateTime GetUtcDateTime()
        {
            //TimeZoneInfo time_zone = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            //return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, time_zone);
            return DateTime.UtcNow;
        }
        public static string TextToSlug(string _value)
        {
            Regex rgx = new Regex(@"[^-a-zA-Z0-9\d\s]");
            // Replace Special Charater and space with emptystring 
            string finalOutput = rgx.Replace(_value, "");
            Regex rgx1 = new Regex("\\s+");
            // Replace space with underscore 
            finalOutput = rgx1.Replace(finalOutput, "-");
            if (finalOutput.StartsWith("-") || finalOutput.StartsWith(" "))
            {
                finalOutput = finalOutput.TrimStart(finalOutput[0]);
            }
            if (finalOutput.EndsWith("-") || finalOutput.EndsWith(" "))
            {
                finalOutput = finalOutput.TrimEnd(finalOutput[finalOutput.Length - 1]);
            }
            return finalOutput.ToLower();
        }
        public static string TrimCharacters(string _value, int _length = 1)
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                return _value;
            }
            else
            {
                return _value.TrimEnd(_value[_value.Length - _length]);
            }
        }
        public static string StripHtmlTags(string _value)
        {
            return Regex.Replace(_value, "<.*?>|&.*?;", string.Empty);
        }
        public static string CreateFileName(string fileName)
        {
            string[] strGUID = Guid.NewGuid().ToString().Split(new char[] { '-' });
            return fileName + "-" + strGUID[0];
        }
        public static string UpperCaseFirstLetter(string _value)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(_value))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(_value[0]) + _value.Substring(1);
        }
        public static string UpperCaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
        public static int CountStringOccurrences(string _value, string _pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = _value.IndexOf(_pattern, i)) != -1)
            {
                i += _pattern.Length;
                count++;
            }
            return count;
        }
        public static double Round(double d)
        {
            var absoluteValue = Math.Abs(d);
            var integralPart = (long)absoluteValue;
            var decimalPart = absoluteValue - integralPart;
            var sign = Math.Sign(d);

            double roundedNumber;

            if (decimalPart > 0.5)
            {
                roundedNumber = integralPart + 1;
            }
            else if (decimalPart == 0)
            {
                roundedNumber = absoluteValue;
            }
            else
            {
                roundedNumber = integralPart + 0.5;
            }

            return sign * roundedNumber;
        }
        public static List<string> GetTimeZoneList()
        {
            List<string> timeList = new List<string>();
            var tzone = TimeZoneInfo.GetSystemTimeZones();
            foreach (TimeZoneInfo tzi in tzone)
            {
                timeList.Add(tzi.Id);
            }
            return timeList;
        }
        //public static string ConvertToWebURL(string _value)
        //{
        //    if (!string.IsNullOrWhiteSpace(_value) && !_value.Equals("#"))
        //    {
        //        if (_value.IndexOf("www.") == -1 && _value.IndexOf("http") == -1 && _value.IndexOf("https") == -1)
        //        {
        //            if (_value.ToLower().Equals("home"))
        //            {
        //                _value = GetSettingContentByName("Website URL");
        //            }
        //            else if (_value.Equals("/"))
        //            {
        //                _value = GetSettingContentByName("Website URL");
        //            }
        //            else
        //            {
        //                _value = GetSettingContentByName("Website URL") + _value;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        _value = "#";
        //    }
        //    return _value;
        //}


        public static void SendEmail(string subject, string body, string emailTo)
        {
            string FromMail = ConfigurationManager.AppSettings["Username"];
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["host"]);
            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;
            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            //SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Username"], ConfigurationManager.AppSettings["Password"]);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
        #endregion
        #region "Mail Helper"
        public class MailObject
        {
            public string MailTo { get; set; }
            public string MailFrom { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }
        }
        public static void SendEmail(MailObject mailer)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mailer.MailFrom) && !string.IsNullOrWhiteSpace(mailer.MailTo) && !string.IsNullOrWhiteSpace(mailer.Subject) && !string.IsNullOrWhiteSpace(mailer.Message))
                {
                    if (IsEmailAddressValid(mailer.MailFrom))
                    {
                        mailer.Message = mailer.Message.Replace("{Current Year}", GetUtcDateTime().Year.ToString());
                        //mailer.Message = mailer.Message.Replace("{Footer Menus}", GetEmailFooterTable());
                        string[] mail_to_array = mailer.MailTo.Split(',');
                        foreach (string mail_to in mail_to_array)
                        {
                            var emailTo = mail_to.Trim();
                            if (!string.IsNullOrWhiteSpace(emailTo))
                            {
                                if (IsEmailAddressValid(emailTo))
                                {
                                    MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["Username"], emailTo);
                                    mm.Subject = mailer.Subject;
                                    mm.Body = mailer.Message;
                                    mm.IsBodyHtml = true;
                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Host = ConfigurationManager.AppSettings["host"];
                                    smtp.EnableSsl = true;
                                    NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["Username"], ConfigurationManager.AppSettings["Password"]);
                                    smtp.UseDefaultCredentials = true;
                                    smtp.Credentials = NetworkCred;
                                    //smtp.Port = 587;
                                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                                    smtp.Send(mm);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }

        }
        #endregion
        #region "Default Values Helper"
        public static int ParseInt(object value)
        {
            int parseVal;
            return ((value == null) || (value == DBNull.Value)) ? 0 : int.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        }
        public static decimal ParseDecimal(object value)
        {
            decimal parseVal;
            return ((value == null) || (value == DBNull.Value)) ? 0 : decimal.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        }
        public static double ParseDouble(object value)
        {
            double parseVal;
            return ((value == null) || (value == DBNull.Value)) ? 0 : double.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        }
        public static DateTime ParseDateTime(object value)
        {
            DateTime parseVal;
            return ((value == null) || (value == DBNull.Value)) ? new DateTime(1900, 1, 1) : DateTime.TryParse(value.ToString(), out parseVal) ? parseVal : new DateTime(1900, 1, 1);
        }
        public static DateTime ParseExactDateTime(object value)
        {
            DateTime parseVal;
            CultureInfo ci = CultureInfo.CreateSpecificCulture("ur-PK");
            return ((value == null) || (value == DBNull.Value)) ? new DateTime(1900, 1, 1) : DateTime.TryParseExact(value.ToString(), Website_Date_Format, ci, DateTimeStyles.None, out parseVal) ? parseVal : new DateTime(1900, 1, 1);
        }
        public static DateTime ParsePickerDateTime(object value)
        {
            DateTime parseVal;
            CultureInfo ci = CultureInfo.CreateSpecificCulture("ur-PK");
            return ((value == null) || (value == DBNull.Value)) ? new DateTime(1900, 1, 1) : DateTime.TryParse(value.ToString(), ci.DateTimeFormat, DateTimeStyles.None, out parseVal) ? parseVal : new DateTime(1900, 1, 1);
        }
        public static string ParseString(object value)
        {
            return ((value == null) || (value == DBNull.Value)) ? string.Empty : value.ToString();
        }
        public static bool ParseBoolean(object value)
        {
            bool parseVal;
            return ((value == null) || (value == DBNull.Value)) ? false : bool.TryParse(value.ToString(), out parseVal) ? parseVal : false;
        }
        public static bool IsEmailAddressValid(string EmailAddress)
        {
            string pattern = @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(EmailAddress);
        }
        #endregion
        //#region "Setting Table Helper"
        //public static string GetSettingContentByName(string _Key)
        //{
        //    string ReturnContent = "#";
        //    var db = new LCMEntities();
        //    var SettingRecord = db.Settings.FirstOrDefault(o => o.Name.Equals(_Key));
        //    if (SettingRecord != null)
        //    {
        //        ReturnContent = SettingRecord.Content;
        //    }
        //    return ReturnContent;
        //}
        //public static string GetSettingContentByID(int _Key)
        //{
        //    string ReturnContent = "#";
        //    var db = new LCMEntities();
        //    var SettingRecord = db.Settings.FirstOrDefault(o => o.ID == _Key);
        //    if (SettingRecord != null)
        //    {
        //        ReturnContent = SettingRecord.Content;
        //    }
        //    return ReturnContent;
        //}
        //public static string GetSettingSessionData(string _settingName, string _sessionName)
        //{
        //    string ReturnContent = (string)HttpContext.Current.Session[_sessionName];
        //    if (string.IsNullOrWhiteSpace(ReturnContent))
        //    {
        //        ReturnContent = GetSettingContentByName(_settingName);
        //        HttpContext.Current.Session.Add(_sessionName, ReturnContent);
        //    }
        //    return ReturnContent;
        //}
        //#endregion
       
        #region "Cache Helper"
        public static bool isCacheContextAvailable()
        {
            bool ReturnValue = true;
            if (HttpContext.Current.Cache == null)
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }
        public static void AddItemToCache(string _key, object _value)
        {
            if (isCacheContextAvailable() == true)
            {
                HttpContext.Current.Cache.Add(_key, _value, null, DateTime.Now.AddDays(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
        }
        public static object GetItemFromCache(string _key)
        {
            object ReturnObject = string.Empty;
            if (isCacheContextAvailable() == true)
            {
                if (HttpContext.Current.Cache.Get(_key) != null)
                {
                    ReturnObject = HttpContext.Current.Cache.Get(_key);
                }
            }
            return ReturnObject;
        }
        public static void RemoveItemFromCache(string _key)
        {
            if (isCacheContextAvailable() == true)
            {
                if (HttpContext.Current.Cache.Get(_key) != null)
                {
                    HttpContext.Current.Cache.Remove(_key);
                }
            }
        }
        public static void ClearAllCache()
        {
            if (isCacheContextAvailable() == true)
            {
                var CacheEnum = HttpContext.Current.Cache.GetEnumerator();
                while (CacheEnum.MoveNext())
                {
                    HttpContext.Current.Cache.Remove(CacheEnum.Key.ToString());
                }
            }
        }
        #endregion
        
        #region "Session Helper"
        public static void AddSession(string _key, object _value)
        {
            HttpContext.Current.Session.Add(_key, _value);
        }
        public static object GetSession(string _key)
        {
            object ReturnObject = null;
            var SessionObject = HttpContext.Current.Session[_key];
            if (SessionObject != null)
            {
                ReturnObject = SessionObject;
            }
            return ReturnObject;
        }
        public static void RemoveSession(string _key)
        {
            var SessionObject = HttpContext.Current.Session[_key];
            if (SessionObject != null)
            {
                HttpContext.Current.Session.Remove(_key);
            }
        }
        #endregion
        #region "Session Objects Helper"
        public static bool IsUserLogin()
        {
            bool ReturnValue = false;
            if (GetSession(Session_User_Login) != null)
            {
                ReturnValue = true;
            }
            return ReturnValue;
        }
      
        public static string GetCaptchaTextFromSession(string _type)
        {
            return (string)GetSession(_type + "_captcha_string");
        }
        #endregion
        public static string CallApi(string apiUrl)
        {
            string data = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                var httpClient = new HttpClient();
                HttpResponseMessage response = httpClient.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    data = response.Content.ReadAsStringAsync().Result;
                }
                return data;

            }
        }



      
    }

    public static class EnumBiltyRole
    {
        public const string User1 = "User1";
        public const string User2 = "User2";
        public const string User3 = "User3";
    }

    public static class EnumGSTType
    {
        public const string Intra_Province_Primary = "Intra Province Primary";
        public const string Intra_Province_Secondary = "Intra Province Secondary";
        public const string Inter_Province_Secondary = "Inter Province Secondary";
        public const string Inter_Province_Primary = "Inter Province Primary";
        public const string Inter_Province_Primary_ZeroGST = "Inter Province Primary Zero GST";
        public const string Inter_Province_Secondary_ZeroGST = "Inter Province Secondary Zero GST";
        public const string Non_GST = "NON GST";

    }


}

public class SideBarExtracter
{
    private readonly LCMEntities context;
    public List<Menu> Menus;
    public List<SecondLevelMenu> subMenu;
    public List<NavMenu> Forms;
    public List<RolePermission> Authentication;
    public List<SubcriptionModule> Subcription;
    public SideBarExtracter()
    {
        context = new LCMEntities();
        Menus = context.Menus.Where(x => x.IsActive == true).OrderBy(x => x.SequenceOrder).ToList();
        subMenu = context.SecondLevelMenus.OrderBy(x => x.SequenceOrder).ToList();
        Forms = context.NavMenus.Where(x => x.IsActive == true).OrderBy(x => x.SequenceOrder).ToList();
        Subcription = context.SubcriptionModules.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList();
    }



}
