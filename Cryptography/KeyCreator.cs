using System.Security.Cryptography;

namespace ElectronicVoting.Cryptography
{
    public static class KeyCreator
    {
        public static byte[] CreatePrivateKey()
        {
            var rsa = RSA.Create();
            return rsa.ExportRSAPrivateKey();
        }
        public static byte[] CreatePublicKey(byte[] privateKey)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);
            return rsa.ExportRSAPublicKey();
        }
    }
}