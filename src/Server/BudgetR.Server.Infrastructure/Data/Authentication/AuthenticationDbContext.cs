using BudgetR.Core.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetR.Server.Infrastructure.Data.Authentication;
public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
}
