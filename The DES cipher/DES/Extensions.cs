﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace The_DES_cipher.DES
{
    internal static class Extensions
    {
        public static string Encrypt(this Des provider, string plaintext)
        {
            return Convert.ToBase64String(provider.Encryption(BitExtensions.StringToBits(plaintext, provider.Encoding)));
        }
        public static string Decrypt(this Des provider, string ciphertext)
        {
            return provider.Encoding.GetString(provider.Decryption(Convert.FromBase64String(ciphertext)));
        }
        public static string FileEncrypt(this Des provider, string plaintext, string save)
        {
            var result = Convert.ToBase64String(provider.FileEncryption(BitExtensions.StringToBits(plaintext, provider.Encoding)));
            File.WriteAllBytes(save, Convert.FromBase64String(result));
            return result;
        }
        public static string FileDecrypt(this Des provider, string ciphertext, string save)
        {
            var result = provider.Encoding.GetString(provider.FileDecryption(Convert.FromBase64String(ciphertext)));
            File.WriteAllBytes(save, Convert.FromBase64String(result));
            return result;
        }
        internal static string BitsToString(this byte[] bytes, string separator = "")
        {
            var str = "";
            foreach (byte b in bytes)
                str += b.ToString() + separator;
            return str;
        }
        internal static string BytesToString(this byte[] bytes, string separator = "", bool reverse = false)
        {
            var str = "";
            if(reverse)
                bytes = BitExtensions.BitsToByte(bytes).Reverse().ToArray();
            else
                bytes = BitExtensions.BitsToByte(bytes);
            foreach (byte b in bytes)
                str += Convert.ToString(b, 16) + separator;
            return str;
        }
        internal static SolidColorBrush ToColor(this string hex)
        {
            return (SolidColorBrush)new BrushConverter().ConvertFromString(hex);
        }
        internal static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        public static void Invoke(this ContentControl instanse, Action action)
        {
            instanse.Dispatcher?.BeginInvoke(DispatcherPriority.Background, action);
        }
    }
}
