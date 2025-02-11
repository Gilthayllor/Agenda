namespace Evently.Modules.Users.PublicApi;

public interface IUsersApi
{
    Task<UserResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}
