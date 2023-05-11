using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Presentation.Token
{
    public class TokenJwtBuilder
    {
        private SecurityKey securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expiryInMinutes = 5;

        public TokenJwtBuilder AddSecurityKey (SecurityKey security)
        {
            this.securityKey = securityKey;
            return this;
        }
        public TokenJwtBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }
        public TokenJwtBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }
        public TokenJwtBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }
        public TokenJwtBuilder AddClaims(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }
        public TokenJwtBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }
        private void EnsureArguments()
        {
            if(this.securityKey == null)
            {
                throw new ArgumentNullException("security key");
            }
            if(string.IsNullOrEmpty(this.subject))
            {
                throw new ArgumentNullException("subject");
            }
            if (string.IsNullOrEmpty(this.issuer))
            {
                throw new ArgumentNullException("issuer");
            }
            if (string.IsNullOrEmpty(this.subject))
            {
                throw new ArgumentNullException("audience");
            }
        }
        public TokenJwt Builder()
        {
            EnsureArguments();
            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Sub,this.subject),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            }.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));
            
            var token = new JwtSecurityToken(
                issuer: this.issuer,
                audience: this.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: new SigningCredentials(
                                    this.securityKey,
                                    SecurityAlgorithms.HmacSha256)
                );
            return new TokenJwt(token);
        }

    }

}
