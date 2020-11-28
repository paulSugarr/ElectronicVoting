using System;
using System.Linq;
using System.Text;
using ElectronicVoting.Cryptography;
using ElectronicVoting.Electors;

namespace ElectronicVoting
{
    class Program
    {
        static void Main(string[] args)
        {
            var privateKey = KeyCreator.CreatePrivateKey();
            var publicKey = KeyCreator.CreatePublicKey(privateKey);

            var B = Encoding.UTF8.GetBytes("b");

            var blindB = CryptographyProvider.Encrypt(publicKey, B);
            var signB = CryptographyProvider.SignData(privateKey, B);
            var signBlindB = CryptographyProvider.SignData(privateKey, blindB);
            var blindSignB = CryptographyProvider.Encrypt(publicKey, signB);

            Console.WriteLine(signBlindB.SequenceEqual(blindSignB));
        }
    }
}