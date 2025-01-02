using MedicalAppointmentManagement.Domain.Entities;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Infrastructure.DateAccess;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentManagement.Infrastructure.Repository;
public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private MedicalAppointmentDbContext _dbContext;
    public UserRepository(MedicalAppointmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistAciveUserWithEmail(string email)
    {
        var user = await _dbContext.Users.AsNoTracking().AnyAsync(user => user.Email == email);

        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);

        return user;
    }
}
