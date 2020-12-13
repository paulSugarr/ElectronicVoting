﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public byte[] SignData(Dictionary<string, object> privateKey, byte[] data)
        {
            var d = BigInt.Parse(privateKey.GetString("d"));
            var n = BigInt.Parse(privateKey.GetString("n"));

            var m = new BigInt(data);

            var s = BigInt.ModPow(m, d, n);
            var result = s.ToByteArray();
            return result;
        }

        public bool VerifyData(Dictionary<string, object> publicKey, byte[] data, byte[] signedData)
        {
            var e = BigInt.Parse(publicKey.GetString("e"));
            var n = BigInt.Parse(publicKey.GetString("n"));

            var s = new BigInt(signedData);

            var m = BigInt.ModPow(s, e, n);
            var result = m.ToByteArray();
            return result.SequenceEqual(data);
        }
    }
}