using System;
using System.Collections.Generic;
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
            var privateKey = RSAKeyCreator.CreatePrivateKey();
            var publicKey = RSAKeyCreator.CreatePublicKey(privateKey);
            
            var rsa = new RSACryptography();
            var msg = Encoding.UTF7.GetBytes("sdlfjsf;slkfslkfddfshkfksjfhsjhfsjldfslkjfslakddsfklsjdflsdkfjsjgakdglkjjkdfhgdkjfghlskdhgklshdglsjdfhgjshfjsdhfkshfsdgfjshgfasldhalskdjalskfsdhjkfsdhfgsfkjaghsjlkfhasdohdfgskljafskhfgkflsdjfkljnzjzzmnznzzzzzzzzzzzzzz");
            Console.WriteLine(Encoding.UTF7.GetString(msg));
            var encrypted = rsa.Encrypt(publicKey, msg);
            var decrypted = rsa.Decrypt(privateKey, encrypted);
            Console.WriteLine(Encoding.UTF7.GetString(decrypted));
            
            // var signB = CryptographyProvider.SignData(privateKey, B);
            // var signBlindB = CryptographyProvider.SignData(privateKey, blindB);
            // var blindSignB = CryptographyProvider.Encrypt(publicKey, signB);
            //
            // Console.WriteLine(signBlindB.SequenceEqual(blindSignB));
        }
    }
}