using BlogAPI.DTOs.AccountDTOs;

namespace BlogAPI.Repository.AuthRepo
{
    public interface IAuthRepository
    {
        Task<AuthDTO> RegisterAsync(RegisterDTO dto);
        Task<AuthDTO> LoginAsync(LoginDTO dto);
    }
}
