using System.Collections.Generic;
using System.Text;
using ElectronicVoting.Cryptography;
using ElectronicVoting.Extensions;

namespace ElectronicVoting.Electors
{
    public class Elector
    {
        private byte[] _privateKey;
        private byte[] _publicEncryptionKey;
        private byte[] _publicSignKey;
        private byte[] _blindKey;

        public Elector()
        {
            
        }
        public Elector(Dictionary<string, object> initialData)
        {
            var privateKeyString = initialData.GetString("private_key");
            _privateKey = Encoding.UTF8.GetBytes(privateKeyString);
            var publicEncryptKeyString = initialData.GetString("public_encrypt_key");
            _publicEncryptionKey = Encoding.UTF8.GetBytes(publicEncryptKeyString);
            var publicSignKeyString = initialData.GetString("public_sign_key");
            _publicSignKey = Encoding.UTF8.GetBytes(publicSignKeyString);
            var blindKeyString = initialData.GetString("blind_key");
            _blindKey = Encoding.UTF8.GetBytes(blindKeyString);
        }

        public void CreateNewKeys()
        {
            _privateKey = KeyCreator.CreatePrivateKey();
            _publicEncryptionKey = KeyCreator.CreatePublicKey(_privateKey);
            _publicSignKey = KeyCreator.CreatePublicKey(_privateKey);
            _blindKey = KeyCreator.CreatePublicKey(_privateKey);
        }
        
        /// <summary> Step 2 in E-voting protocol</summary>
        public byte[] CreateBlindedSignedMessage(int choiceIndex)
        {
            var bulletin = CreateBulletin(0);
            var data = Encoding.UTF8.GetBytes(bulletin);
            var encryptB = CryptographyProvider.Encrypt(_publicEncryptionKey, data);
            var signEncryptB = CryptographyProvider.SignData(_publicSignKey, encryptB);
            var blindSignEncryptB = CryptographyProvider.Encrypt(_blindKey, signEncryptB);
            return blindSignEncryptB;
        }
        
        /// <summary> Step 4 in E-voting protocol</summary>
        public byte[] RemoveBlindEncryption(byte[] blindedMessage)
        {
            var result = CryptographyProvider.Decrypt(_privateKey, blindedMessage);
            return result;
        }
        
        /// <summary> Step 6 in E-voting protocol</summary>
        public byte[] GetPrivateKey()
        {
            return _privateKey;
        }
        private string CreateBulletin(int choiceIndex)
        {
            var bulletin = choiceIndex.ToString();
            return bulletin;
        }
    }
}