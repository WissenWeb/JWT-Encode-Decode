using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWT
{
    public class JWT
    {
        public string Secret { get; set; }
        public JWT()
        {
                Secret= "qcBp5FsICvZ69YSDsRcKsBqCACkLV2eVqWuO464HUkkOk4ua + e9ckW2 / Lm3agUFbl0ETebzqG21FSHlRNve29Fl / I501uaTJXPs1VOI + OsZzCxMZK1ZyfxCNn3SDnNjRyjkvZqVDJWrIcrseEInoWNXAjo90JzrTQ / ZZOAf9xSM4jeqCTTI6sl / Z1S2gpgiUQljK2HLc12vs4BxO / LqEGWvL9EKqZqCqOPAiZ6MyzCeOexXh / a6k / JVvkHD7Qrt2aqV / NQ58ff0EezfSJk7X61bJJb7fBDLUyszmc4Nx14gvcKwgxc2A2Pd8DWFnybFetyK5qY2VZBKxQPzHqlD + 26T / vtWEN7Bzx0l5mQYWoj50sQE8GTA0 / QmW + GKUkmWgcW / fpNT6ws / KdMj7U04 + hEHDWIkVB9cvEc6o + xxp7cjESudiHlLa8l2wpfqTJ2d + JjK6oxWq7FaJQGOLGWp1CBrgN3twmXiRIEMQCqssedcgfnWnzTLvhGDsWb1tM0ZwPtJ / hl1NTXK63BmKDODzQcVOwcwayyL25bedCvaPPgBxPSRxVycCGZSqQLv1G0pdulE0fCojGWUgoH3Y / j + WondBhoSapodxrpVrgHclhNT0VWpsoyjxyQVmnGPnLe2wM1ftmfZ7CJZlao / L7t + v8l4mEwA =";
        }
        public string GenerateSecretKey(int sizeInBytes)
        {
            byte[] key = new byte[sizeInBytes];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
                return Convert.ToBase64String(key);
            }
        }
        public string DecodeJWT(string token)
        {
            // Alıcı tarafından bilinen bir güvenlik anahtarı
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Secret));

            // Token doğrulama parametreleri
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,  // Bu, güvenlik anahtarının doğruluğunu kontrol etmeyi kontrol eder
                IssuerSigningKey = securityKey    // Güvenlik anahtarı
            };

            // Token string'i
            var tokenString = token;

            // Tokenı açma
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claimsPrincipal = null;

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false, // Anahtar imza doğrulamasını pas geçmek için false olarak ayarlandı
                ValidateIssuer = false, // Yayıncı doğrulamasını pas geçmek için false olarak ayarlandı
                ValidateAudience = false, // Hedef kitle doğrulamasını pas geçmek için false olarak ayarlandı
                RequireExpirationTime = false, // Geçerlilik süresi gerekliliğini devre dışı bırakmak için false olarak ayarlandı
                ValidateLifetime = false // Token'ın geçerlilik süresini doğrulamayı pas geçmek için false olarak ayarlandı
            };

            try
            {
                SecurityToken validatedToken;
                claimsPrincipal = tokenHandler.ValidateToken(tokenString, validationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
            }

            // Başarıyla açılmışsa, tokenın içeriğini yazdırma
            if (claimsPrincipal != null)
            {

                // Token'ın içeriğini daha fazla işlemek için claims'e erişebilirsiniz
                foreach (var claim in claimsPrincipal.Claims)
                {

                }
            }
            return "";

        }
        public string Encode(string email, string password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var claims = new List<Claim>();

            // Özel talepleri (claims) ekleyin

          

            claims.Add(new Claim("email", email));
            claims.Add(new Claim("password", password));


            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Örnek olarak 1 saat geçerlilik süresi
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }
    }
}
