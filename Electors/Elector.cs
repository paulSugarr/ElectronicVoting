using System.Collections.Generic;
using System.Text;
using ElectronicVoting.Cryptography;
using ElectronicVoting.Extensions;

namespace ElectronicVoting.Electors
{
    public class Elector
    {
        private Dictionary<string, object> _privateKey;
        private Dictionary<string, object> _publicEncryptionKey;
        private Dictionary<string, object> _publicSignKey;
        private Dictionary<string, object> _blindKey;

        private ICryptographyProvider _cryptographyProvider;

        public Elector()
        {
            
        }
        public Elector(ICryptographyProvider cryptographyProvider, Dictionary<string, object> initialData)
        {
            _cryptographyProvider = cryptographyProvider;
            _privateKey = initialData.GetDictionary("private_key");
            _publicEncryptionKey = initialData.GetDictionary("public_encrypt_key");
            _publicSignKey = initialData.GetDictionary("public_sign_key");
            _blindKey = initialData.GetDictionary("blind_key");
        }

        public void CreateNewKeys()
        {
            _privateKey = _cryptographyProvider.KeyCreator.CreatePrivateKey();
            _publicEncryptionKey = _cryptographyProvider.KeyCreator.CreatePublicKey(_privateKey);
            _publicSignKey = _cryptographyProvider.KeyCreator.CreatePublicKey(_privateKey);
            _blindKey = _cryptographyProvider.KeyCreator.CreatePublicKey(_privateKey);
        }
        
        /// <summary> Step 2 in E-voting protocol</summary>
        public byte[] CreateBlindedSignedMessage(int choiceIndex)
        {
            var bulletin = CreateBulletin(0);
            var data = Encoding.UTF8.GetBytes(bulletin);
            var encryptB = _cryptographyProvider.Encrypt(_publicEncryptionKey, data);
            var signEncryptB = _cryptographyProvider.SignData(_publicSignKey, encryptB);
            var blindSignEncryptB = _cryptographyProvider.Encrypt(_blindKey, signEncryptB);
            return blindSignEncryptB;
        }
        
        /// <summary> Step 4 in E-voting protocol</summary>
        public byte[] RemoveBlindEncryption(byte[] blindedMessage)
        {
            var result = _cryptographyProvider.Decrypt(_privateKey, blindedMessage);
            return result;
        }
        
        /// <summary> Step 6 in E-voting protocol</summary>
        public Dictionary<string, object> GetPrivateKey()
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