namespace ReceptiDeWebAPI.Services.Users.Hasher
{
    public interface IHasher
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
       public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
