using Microsoft.AspNetCore.Identity;
using Chaos.Business;

namespace Chaos.Models.DbModels;

public class GameUser : IdentityUser
{
    public Factions Faction { get; set; }
}
