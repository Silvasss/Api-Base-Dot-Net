namespace ApiBase.Dtos
{
    public partial class UserForLoginConfirmationDto
    {
        // If you require creating a UserForLoginConfirmationDto object before setting, you can initialize the properties using a default 
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];
        public string Email { get; set; } = string.Empty;
    }
}
