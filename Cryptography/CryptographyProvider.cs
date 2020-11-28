using System;
using System.Security.Cryptography;

namespace ElectronicVoting.Cryptography
{
    public static class CryptographyProvider
    {

        public static byte[] Encrypt(byte[] publicKey, byte[] data)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(publicKey, out _);
            var result = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
            return result;
        }

        public static byte[] Decrypt(byte[] secretKey, byte[] data)
        {
            throw new NotImplementedException();
        }

        public static byte[] SignData(byte[] privateKey, byte[] data)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);
            var result = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            return result;
        }
        public static bool VerifyData(byte[] publicKey, byte[] data, byte[] signedData)
        {
            throw new NotImplementedException();
        }
    }
}