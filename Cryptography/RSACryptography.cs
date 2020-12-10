using System;
using System.Collections.Generic;
using ElectronicVoting.Extensions;
using BigInt = System.Numerics.BigInteger;

namespace ElectronicVoting.Cryptography
{
    public class RSACryptography : ICryptographyProvider
    {
        public IKeyCreator KeyCreator { get; }

        public RSACryptography()
        {
            KeyCreator = new RSAKeyCreator();
        }

        public byte[] Encrypt(Dictionary<string, object> publicKey, byte[] data)
        {
            var e = BigInt.Parse(publicKey.GetString("e"));
            var n = BigInt.Parse(publicKey.GetString("n"));

            var m = new BigInt(data);

            var c = BigInt.ModPow(m, e, n);
            var result = c.ToByteArray();
            return result;
        }

        public byte[] Decrypt(Dictionary<string, object> privateKey, byte[] data)
        {
            var d = BigInt.Parse(privateKey.GetString("d"));
            var n = BigInt.Parse(privateKey.GetString("n"));

            var c = new BigInt(data);

            var m = BigInt.ModPow(c, d, n);
            var result = m.ToByteArray();
            return result;
        }

        public byte[] SignData(byte[] privateKey, byte[] data)
        {
            throw new NotImplementedException();
        }

        public bool VerifyData(byte[] publicKey, byte[] data, byte[] signedData)
        {
            throw new NotImplementedException();
        }
    }
}