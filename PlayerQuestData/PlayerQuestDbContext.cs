using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace QuestEngine.PlayerQuestData
{
    public class PlayerQuestDbContext : DbContext
    {
        public DbSet<PlayerQuestState> PlayerQuestStates { get; set; }

        public PlayerQuestDbContext(DbContextOptions<PlayerQuestDbContext> options)
            : base(options)
        {}

        public PlayerQuestState Get(string playerId)
        {
            return PlayerQuestStates.Where(s => s.PlayerId == playerId)
                                    .OrderByDescending(s => s.TimeStamp)
                                    .FirstOrDefault();
        }

        public void Save(PlayerQuestState state)
        {
            PlayerQuestStates.Add(state);
            SaveChanges();
        }    
    }
}