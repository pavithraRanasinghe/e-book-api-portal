using api_portal.Model;

namespace api_portal.Dto.Response
{
    public class LoginResponse
    {
       public string Token { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
