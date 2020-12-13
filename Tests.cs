using System;
using System.Linq;
using System.Text;
using ElectronicVoting.Cryptography;
using ElectronicVoting.Electors;
using ElectronicVoting.Extensions;
using ElectronicVoting.Validators;

namespace ElectronicVoting
{
    public static class Tests
    {
        public static void TestCommutative()
        {
            var rsa = new RSACryptography();
            var privateKey = rsa.KeyCreator.CreatePrivateKey();
            var publicKey = rsa.KeyCreator.CreatePublicKey(privateKey);
            var blindKey = rsa.KeyCreator.CreateBlindKey();
            var msg = Encoding.UTF7.GetBytes("sdlfjsf;slkfslkfddfshkfksjfhsjhfsjldfslkjfslakddsfklsjdflsdkfjsjgakdglkjjkdfhgdkjfghlskdhgklshdglsjdfhgjshfjsdhfkshfsdgfjshgfasldhalskdjalskfsdhjkfsdhfgsfkjaghsjlkfhasdohdfgskljafskhfgkflsdjfkljnzjzzmnznzzzzzzzzzzzzzz");
            Console.WriteLine(Encoding.UTF7.GetString(msg));
            var encrypted = rsa.Encrypt(publicKey, msg);
            var decrypted = rsa.Decrypt(privateKey, encrypted);
            Console.WriteLine(Encoding.UTF7.GetString(decrypted));

            var B = Encoding.UTF8.GetBytes("B");
            var blindB = rsa.BlindData(blindKey, publicKey, B);
            var signB = rsa.SignData(privateKey, B);
            var signBlindB = rsa.SignData(privateKey, blindB);
            var blindSignB = rsa.BlindData(blindKey, publicKey, signB);
            
            Console.WriteLine($"Commutative = {signBlindB.SequenceEqual(blindSignB)}");
            Console.WriteLine($"Verify test = {rsa.VerifyData(publicKey, B, signB)}");
        }

        public static void TestProtocol()
        {
            var rsa = new RSACryptography();
            var validator = new Validator(rsa);
            validator.CreateKeys();
            
            var elector = new Elector(rsa, validator.PublicKey.GetChangeableCopy());
            elector.CreateNewKeys();

            var blinded = elector.CreateBlindedMessage(0);
            var blindedSigned = elector.CreateBlindedSignedMessage(0);
            
            if (validator.VerifyBulletin(blindedSigned, blinded, elector.PublicSignKey.GetChangeableCopy()))
            {
                var signedByValidator = validator.SignBulletin(blinded);
            }
        }
    }
}