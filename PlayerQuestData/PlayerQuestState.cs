using System;
using System.ComponentModel.DataAnnotations;

namespace QuestEngine.PlayerQuestData
{
    // Represent a player's state in the the quest
    public class PlayerQuestState
    {
        [Key]
        // Internal record ID
        public string RecId { get; set; }

        // ID of the player
        public string PlayerId { get; set; }

        // Index of last milestone completed.
        public int LastMilestoneIndex { get; set; }

        // Quest point earned in last milestone reached
        public double QuestPointEarned { get; set; }

        // Total quest point earned up to last milestone completed
        public double TotalQuestPoint { get; set; }

        // Total quest percent completed in terms of quest point earn
        public double TotalQuestPercentCompleted { get; set; }

        // Timestamp
        public DateTime TimeStamp { get; set; }
    }
}