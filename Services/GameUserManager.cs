using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Chaos.Models.DbModels;
using Chaos.Data;

namespace Chaos.Services;

public class GameUserManager : UserManager<GameUser>
{
    readonly GameDbContext _dbContext;

    public GameUserManager(GameDbContext gameDbContext,
                        IUserStore<GameUser> store,
                        IOptions<IdentityOptions> optionsAccessor,
                        IPasswordHasher<GameUser> passwordHasher,
                        IEnumerable<IUserValidator<GameUser>> userValidator,
                        IEnumerable<IPasswordValidator<GameUser>> passwordValidator,
                        ILookupNormalizer keyNormalizer,
                        IdentityErrorDescriber errors,
                        IServiceProvider services,
                        ILogger<UserManager<GameUser>> logger)
                    : base(store, optionsAccessor, passwordHasher, userValidator, passwordValidator, keyNormalizer, errors, services, logger) 
    {
        _dbContext = gameDbContext;
    }

}
