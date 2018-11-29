using System;
using System.IO;
using System.Security.Cryptography;

namespace Crypt
{
    /// <summary>
    /// Класс методов для AES шифрования/дешифрования
    /// </summary>
    public class AesCrypt
    {
        /// <summary>
        /// Метод AES шифрования
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <param name="key">Ключ</param>
        /// <param name="IV">Вектор</param>
        /// <returns>Зашифрованный массив байтов</returns>
        public static byte[] EncryptStringToBytes(string text, byte[] key, byte[] IV)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            if (key == null)
                throw new ArgumentNullException("key");
            if (IV == null)
                throw new ArgumentNullException("IV");
            byte[] encryptedText;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = IV;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(text);
                        }
                        encryptedText = memoryStream.ToArray();
                    }
                }
            }
            return encryptedText;
        }

        /// <summary>
        /// Метод AES дешифрования
        /// </summary>
        /// <param name="buffer">Зашифрованный массив байтов</param>
        /// <param name="key">Ключ</param>
        /// <param name="IV">Вектор</param>
        /// <returns>Расшифрованная строка</returns>
        public static string DecryptStringFromBytes(byte[] buffer, byte[] key, byte[] IV)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (key == null)
                throw new ArgumentNullException("key");
            if (IV == null)
                throw new ArgumentNullException("IV");
            string text;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = IV;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            text = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return text;
        }
    }
}
