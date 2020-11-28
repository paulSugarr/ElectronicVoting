using System;

namespace ElectronicVoting.Cryptography
{
    public static class CryptographyProvider
    {

        public static byte[] Encrypt(byte[] publicKey, byte[] data)
        {
            throw new NotImplementedException();
        }

        public static byte[] Decrypt(byte[] secretKey, byte[] data)
        {
            throw new NotImplementedException();
        }

        public static byte[] SignData(byte[] privateKey, byte[] data)
        {
            throw new NotImplementedException();
        }
        public static bool VerifyData(byte[] publicKey, byte[] data, byte[] signedData)
        {
            throw new NotImplementedException();
        }
    }
}