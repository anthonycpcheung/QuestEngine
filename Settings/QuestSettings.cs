using System;
using System.Collections.Generic;

namespace QuestEngine.Settings
{
    // Setting of the quest in appsettings.json
    public class QuestSettings
    {
        // The total quest points needed to complete a quest
        public double TotalQuestPoint { get; set; }

        public double RateFromBet { get; set; }

        public double LevelBonusRate { get; set; }

        public List<MileStoneSettings> MileStones { get; set; }

        public int MileStonesNumber { get { return MileStones.Count; } }
    }

    // Setting of one milestone
    public class MileStoneSettings 
    {
        // Accumunated quest point required to reach this milestone
        public double QuestPointRequired { get; set; }

        // Chips award when reaching this milestone
        public double ChipsAward { get; set; }
    }
}