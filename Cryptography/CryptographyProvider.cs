using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using ElectronicVoting.Extensions;
using BigInt = System.Numerics.BigInteger;

namespace ElectronicVoting.Cryptography
{
    public static class CryptographyProvider
    {

        public static byte[] Encrypt(Dictionary<string, object> publicKey, byte[] data)
        {
            var e = BigInt.Parse(publicKey.GetString("e"));
            var n = BigInt.Parse(publicKey.GetString("n"));
            // var dataString = Encoding.UTF8.GetString(data);
            var m = new BigInt(data);

            var c = BigInt.ModPow(m, e, n);
            var result = c.ToByteArray();
            return result;
        }

        public static byte[] Decrypt(Dictionary<string, object> privateKey, byte[] data)
        {
            var d = BigInt.Parse(privateKey.GetString("d"));
            var n = BigInt.Parse(privateKey.GetString("n"));
            // var dataString = Encoding.UTF8.GetString(data);
            var c = new BigInt(data);

            var m = BigInt.ModPow(c, d, n);
            var result = m.ToByteArray();
            return result;
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