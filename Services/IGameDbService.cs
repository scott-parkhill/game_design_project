using Chaos.Business;

namespace Chaos.Services;

public interface IGameDbService
{
    Task<Factions> GetUserFaction(string loggedUserId);
}
