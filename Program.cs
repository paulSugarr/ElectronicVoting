using System;
using System.Linq;
using System.Text;
using ElectronicVoting.Cryptography;

namespace ElectronicVoting
{
    class Program
    {
        static void Main(string[] args)
        {
            var rsa = new RSACryptography();
            var privateKey = rsa.KeyCreator.CreatePrivateKey();
            var publicKey = rsa.KeyCreator.CreatePublicKey(privateKey);
            var msg = Encoding.UTF7.GetBytes("sdlfjsf;slkfslkfddfshkfksjfhsjhfsjldfslkjfslakddsfklsjdflsdkfjsjgakdglkjjkdfhgdkjfghlskdhgklshdglsjdfhgjshfjsdhfkshfsdgfjshgfasldhalskdjalskfsdhjkfsdhfgsfkjaghsjlkfhasdohdfgskljafskhfgkflsdjfkljnzjzzmnznzzzzzzzzzzzzzz");
            Console.WriteLine(Encoding.UTF7.GetString(msg));
            var encrypted = rsa.Encrypt(publicKey, msg);
            var decrypted = rsa.Decrypt(privateKey, encrypted);
            Console.WriteLine(Encoding.UTF7.GetString(decrypted));

            var B = Encoding.UTF8.GetBytes("B");
            var blindB = rsa.Encrypt(publicKey, B);
            var signB = rsa.SignData(privateKey, B);
            var signBlindB = rsa.SignData(privateKey, blindB);
            var blindSignB = rsa.Encrypt(publicKey, signB);
            
            Console.WriteLine($"Commutative = {signBlindB.SequenceEqual(blindSignB)}");
            Console.WriteLine($"Verify test = {rsa.VerifyData(publicKey, B, signB)}");
        }
    }
}