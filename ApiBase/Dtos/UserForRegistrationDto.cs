namespace ApiBase.Dtos
{
    public partial class UserForRegistrationDto
    {
        // If you require creating a UserForLoginDto object before setting, you can initialize the properties using a default non-null
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}