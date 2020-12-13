using System;
using System.Linq;
using System.Text;
using ElectronicVoting.Agencies;
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
            
            var agency = new Agency(rsa, validator.PublicKey.GetChangeableCopy(), 10);
            
            var elector = new Elector(rsa, validator.PublicKey.GetChangeableCopy());
            elector.CreateNewKeys();

            var blinded = elector.CreateBlindedMessage(0);
            var blindedSigned = elector.CreateBlindedSignedMessage(0);
            

            
            if (validator.VerifyBulletin(blindedSigned, blinded, elector.PublicSignKey.GetChangeableCopy()))
            {
                var signedByValidator = validator.SignBulletin(blinded);
                var signedValidatorUnBlind = elector.RemoveBlindEncryption(signedByValidator);
                var encryptedBulletin = elector.GetEncryptedBulletin(0);
                var signedEncryptedBulletin = elector.GetSignedEncryptedBulletin(0);
                
                agency.AddBulletin(signedValidatorUnBlind, encryptedBulletin, signedEncryptedBulletin, elector.PublicSignKey.GetChangeableCopy(), 1);
            }
        }

        public static void TestVerify()
        {
            var rsa = new RSACryptography();
            var privateKey1 = rsa.KeyCreator.CreatePrivateKey();
            var publicKey1 = rsa.KeyCreator.CreatePublicKey(privateKey1);

            var privateKey2 = rsa.KeyCreator.CreatePrivateKey();
            var publicKey2 = rsa.KeyCreator.CreatePublicKey(privateKey2);
            
            var B = Encoding.UTF8.GetBytes("Bababa");
            var signed = rsa.SignData(privateKey1, B);
            
            
            Console.WriteLine(rsa.VerifyData(privateKey1, B, signed));

        }

        public static void TestBlind()
        {
            var rsa = new RSACryptography();
            var blindKey = rsa.KeyCreator.CreateBlindKey();
            var privateKey = rsa.KeyCreator.CreatePrivateKey();
            var privateKey1 = rsa.KeyCreator.CreatePrivateKey();
            var publicKey = rsa.KeyCreator.CreatePublicKey(privateKey);
            var publicKey1 = rsa.KeyCreator.CreatePublicKey(privateKey1);
            
            
            var B = Encoding.UTF8.GetBytes("B");
            var blinded = rsa.BlindData(blindKey, publicKey, B);
            var blindSigned = rsa.SignData(privateKey, blinded);
            var unblindSigned = rsa.UnBlindData(blindKey, publicKey, blindSigned);
            Console.WriteLine($"Verified? = {rsa.VerifyData(publicKey, B, unblindSigned)}");
        }
        
    }
}