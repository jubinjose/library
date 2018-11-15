using Jubin.Utils.Encryption;
using Xunit;

namespace Jubin.Utils.Test
{
    public class EncryptionTest
    {
        [Theory]
        [InlineData(7)]
        [InlineData(10)]
        public void Ensure_Generated_Password_Has_Specified_Length(int length)
        {
            var genPassword = PasswordUtil.GeneratePassword(length);
            Assert.Equal(length, genPassword.Length);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(19)]
        public void Ensure_Generated_Salt_Has_Specified_Length(int length)
        {
            var salt = EncryptionUtil.GenerateRandomSaltString(length);
            Assert.Equal(length, salt.Length);
        }

        [Theory]
        [InlineData("firststringtohash", "/o5jCmNhmpIF8F/y8kxzsAEsFBY=")]
        [InlineData("anotherstringtohash", "+Z9NF19/nFg7ap3xJSvz0MSYMRE=")]
        public void Ensure_Generated_pbkdf2_Hash_Is_Always_Same(string stringToHash, string expectedHash)
        {
            var salt = "MySalt124674";
            var hash = EncryptionUtil.GeneratePBKDF2Hash(stringToHash, salt, 16);
            Assert.Equal(hash, expectedHash);
        }
    }
}
