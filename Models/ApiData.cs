using System;
using System.Collections.Generic;

namespace QuestEngine.Models
{
    // Input for progress api
    public class ProgressInput
    {
        public string PlayerId { get; set; }
        public int PlayerLevel { get; set; }
        public double ChipAmountBet { get; set; }
    }

    // Return of progress api
    public class ProgressResult
    {
        public double QuestPointsEarned { get; set; }
        public double TotalPercentCompleted { get; set; }
        public List<MilestonesRecords> MilestonesCompleted { get; set; }
    }

    public class MilestonesRecords
    {
        public int MilestoneIndex { get; set; }
        public double ChipsAwarded { get; set; }
    }

    // Return of state api
    public class StateOutput
    {
        public double TotalQuestPercentCompleted { get; set; }
        public int LastMilestoneIndexCompleted { get; set; }
    }
}