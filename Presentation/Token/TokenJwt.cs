using System.IdentityModel.Tokens.Jwt;

namespace Presentation.Token
{
    public class TokenJwt
    {
        private JwtSecurityToken token;
        internal TokenJwt( JwtSecurityToken token) 
        {
            this.token = token;
        }
        public DateTime ValidTo => token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}
