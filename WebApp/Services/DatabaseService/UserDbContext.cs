using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Users;

namespace WebApp.Services.DatabaseService;

internal class UserDbContext(DbContextOptions options): IdentityUserContext<User, long>(options)
{
}
