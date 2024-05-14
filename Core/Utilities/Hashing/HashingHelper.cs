using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac= new System.Security.Cryptography.HMACSHA512()) //burada passwornd oluşturduk.
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

        }
        //password kontrol etme işlemi Burada tekrardan kriptoladık.
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++) //Gönderdiğimiz harfleri tek tek karşılaştıracağız.
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }

                }
            }
            return true;
        }
    }
}
