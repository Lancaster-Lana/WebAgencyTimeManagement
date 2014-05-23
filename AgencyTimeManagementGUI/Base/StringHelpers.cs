using System;
using System.Web;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using System.Security.Cryptography;

public class StringHelpers
{
    #region Constants

    private const char QUERY_STRING_DELIMITER = '&';

    #endregion Constants

    #region Members

    private static RijndaelManaged _cryptoProvider;
    //128 bit encyption: DO NOT CHANGE    
    private static readonly byte[] Key = { 18, 19, 8, 24, 36, 22, 4, 22, 17, 5, 11, 9, 13, 15, 06, 23 };
    private static readonly byte[] IV = { 14, 2, 16, 7, 5, 9, 17, 8, 4, 47, 16, 12, 1, 32, 25, 18 };

    #endregion Members

    #region Constructor

    static StringHelpers()
    {
        _cryptoProvider = new RijndaelManaged();
        _cryptoProvider.Mode = CipherMode.CBC;
        _cryptoProvider.Padding = PaddingMode.PKCS7;
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Encrypts a given string.
    /// </summary>
    /// <param name="unencryptedString">Unencrypted string</param>
    /// <returns>Returns an encrypted string</returns>
    public static string Encrypt(string unencryptedString)
    {
        byte[] bytIn = Encoding.ASCII.GetBytes(unencryptedString);

        // Create a MemoryStream
        using (var ms = new MemoryStream())
        {
            // Create Crypto Stream that encrypts a stream
            using (var cs = new CryptoStream(ms, _cryptoProvider.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
            {

                // Write content into MemoryStream
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();

                byte[] bytOut = ms.ToArray();
                return Convert.ToBase64String(bytOut);
            }
        }
    }

    /// <summary>
    /// Decrypts a given string.
    /// </summary>
    /// <param name="encryptedString">Encrypted string</param>
    /// <returns>Returns a decrypted string</returns>
    public static string Decrypt(string encryptedString)
    {
        if (encryptedString.Trim().Length != 0)
        {
            // Convert from Base64 to binary
            byte[] bytIn = Convert.FromBase64String(encryptedString);

            // Create a MemoryStream
            using (var ms = new MemoryStream(bytIn, 0, bytIn.Length))
            {
                // Create a CryptoStream that decrypts the data
                using (var cs = new CryptoStream(ms, _cryptoProvider.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                {
                    // Read the Crypto Stream
                    var sr = new StreamReader(cs);
                    return sr.ReadToEnd();
                }
            }
        }
        return string.Empty;
    }

    public static NameValueCollection DecryptQueryString(string queryString)
    {
        if (queryString.Length != 0)
        {
            //Decode the string
            string decodedQueryString = HttpUtility.UrlDecode(queryString);

            //Decrypt the string
            string decryptedQueryString = Decrypt(decodedQueryString);

            //Now split the string based on each parameter
            string[] actionQueryString = decryptedQueryString.Split(new char[] { QUERY_STRING_DELIMITER });

            var newQueryString = new NameValueCollection();

            //loop around for each name value pair.
            for (int index = 0; index < actionQueryString.Length; index++)
            {
                string[] queryStringItem = actionQueryString[index].Split(new char[] { '=' });
                newQueryString.Add(queryStringItem[0], queryStringItem[1]);
            }

            return newQueryString;
        }
        //No query string was passed in.
        return null;
    }

    public static string EncryptQueryString(NameValueCollection queryString)
    {
        //create a string for each value in the query string passed in.
        string tempQueryString = string.Empty;

        for (int index = 0; index < queryString.Count; index++)
        {
            tempQueryString += queryString.GetKey(index) + "=" + queryString[index];
            if (index != queryString.Count - 1)
            {
                tempQueryString += QUERY_STRING_DELIMITER;
            }
        }
        return EncryptQueryString(tempQueryString);
    }

    /// <summary>
    /// You must pass in a string that uses the QueryStringHelper.DELIMITER as the delimiter.
    /// This will also append the "?" to the beginning of the query string.
    /// </summary>
    /// <param name="queryString"></param>
    /// <returns></returns>
    public static string EncryptQueryString(string queryString)
    {
        return "?" + HttpUtility.UrlEncode(Encrypt(queryString));
    }

    #endregion Methods
}