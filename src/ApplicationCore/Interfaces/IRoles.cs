namespace ApplicationCore.Interfaces
{
    public interface IRoles
    {
        Task GenerateAllExistingRolesAsync();

        Task AssignAllRolesToUserAsync(string applicationUserId);
    }
}
