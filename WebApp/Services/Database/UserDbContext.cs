using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Services.Database;

public class UserDbContext(DbContextOptions options) : IdentityUserContext<User, long>(options)
{
}
