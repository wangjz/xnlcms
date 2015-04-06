using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
namespace COM.SingNo.Common
{
  public class EncrypString
    {

      public static string Encrypto(string keyStr,string inputStr)
      {

          byte[] rgbKey = null;
          byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
          rgbKey = Encoding.UTF8.GetBytes(keyStr.Substring(0, 8));
          DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
          byte[] bytes = Encoding.UTF8.GetBytes(inputStr);
         
          MemoryStream stream = new MemoryStream();
          CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
          stream2.Write(bytes, 0, bytes.Length);
          stream2.FlushFinalBlock();
          string returnstr;

          returnstr = Convert.ToBase64String(stream.ToArray());
          return returnstr;
      }


      public static string Decrypto(string keyStr, string inputStr)
      {
        
          byte[] rgbKey = null;
          byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };  //原来的
          byte[] buffer = new byte[inputStr.Length];
          rgbKey = Encoding.UTF8.GetBytes(keyStr.Substring(0, 8));
          DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
          buffer = Convert.FromBase64String(inputStr);
          MemoryStream stream = new MemoryStream(buffer, 0, buffer.Length);
          CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Read);
         // stream2.Write(buffer, 0, buffer.Length);
          StreamReader reader = new StreamReader(stream2, Encoding.UTF8);
         // return reader.ReadToEnd();
          string returnstr = reader.ReadToEnd(); ;
         // returnstr = Encoding.ASCII.GetString(stream.ToArray());
          // returnstr = new UTF8Encoding() .GetString(stream.ToArray());
          return Regex.Unescape(returnstr);
      }

      public static string MD5( string inputStr)
      {
          string md5="";
          byte[] bytes = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(inputStr));

          for (int i = 0; i < bytes.Length; i++)
          {
              md5 = md5 + bytes[i].ToString("X");

          }  
          //return Encoding.Default.GetString(bytes);
          return md5;
      }
    }
}
