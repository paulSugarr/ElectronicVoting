using System.Collections.Generic;

namespace ElectronicVoting.Cryptography
{
    public interface ICryptographyProvider
    {
        byte[] Encrypt(Dictionary<string, object> publicKey, byte[] data);
        byte[] Decrypt(Dictionary<string, object> privateKey, byte[] data);
        byte[] SignData(byte[] privateKey, byte[] data);
        bool VerifyData(byte[] publicKey, byte[] data, byte[] signedData);
    }
}