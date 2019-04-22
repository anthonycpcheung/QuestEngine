using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuestEngine.Models;
using QuestEngine.PlayerQuestData;
using QuestEngine.Settings;

namespace QuestEngine.Service
{
    // Implementation of quest engine required business logics
    public class QuestEngineService : IQuestEngineService
    {
        private QuestSettings config;
        private PlayerQuestDbContext db;
        private ILogger logger;

        public QuestEngineService(PlayerQuestDbContext context, IOptions<QuestSettings> settings, ILogger<QuestEngineService> logger)
        {
            config = settings.Value;
            db = context;
            this.logger = logger;
        }

        public StateOutput GetState(string playerId)
        {
            var player = db.Get(playerId);
            StateOutput output = null;

            if (player == null) 
            {
                output = new StateOutput { 
                    TotalQuestPercentCompleted = 0,
                    LastMilestoneIndexCompleted = 0 };
            }
            else
            {
                output = new StateOutput { 
                    TotalQuestPercentCompleted = player.TotalQuestPercentCompleted,
                    LastMilestoneIndexCompleted = player.LastMilestoneIndex };
            }

            return output;
        }

        public ProgressResult Progress(ProgressInput input)
        {
            PlayerQuestState newPlayerState = null;

            // get persisted last player state object
            var lastPlayerState = db.Get(input.PlayerId);

            // calculate quest point earned and other figures
            var questPointsEarned = (input.ChipAmountBet * config.RateFromBet) + (input.PlayerLevel * config.LevelBonusRate);
            var lastTotalQuestPoint = (lastPlayerState != null) ? lastPlayerState.TotalQuestPoint : 0.0;
            var accumulatedQuestPoints = lastTotalQuestPoint + questPointsEarned;
            var percenCompleted = Math.Min(accumulatedQuestPoints / config.TotalQuestPoint * 100.0, 100.0);
            var index = 0;
            var milestones = config.MileStones.Where(m => m.QuestPointRequired <= accumulatedQuestPoints)
                                              .Select<MileStoneSettings, MilestonesRecords>(m => {
                                                  index++;
                                                  return new MilestonesRecords {
                                                    MilestoneIndex = index,
                                                    ChipsAwarded = m.ChipsAward
                                                  };
                                                }
                                             ).ToList();
            var lastMileStoneIndex = milestones.Count();

            // Player has just started the questj or Player has not yet reach the last milestone, can progress
            // Then persist new player state
            if (lastPlayerState == null || (lastPlayerState != null && lastPlayerState.TotalQuestPercentCompleted < 100.0))
            {
                newPlayerState = new PlayerQuestState {
                    PlayerId = input.PlayerId,
                    QuestPointEarned = questPointsEarned,
                    TotalQuestPoint = accumulatedQuestPoints,
                    TotalQuestPercentCompleted = percenCompleted,
                    LastMilestoneIndex = lastMileStoneIndex,
                    TimeStamp = DateTime.Now
                };
                db.Save(newPlayerState);
            }
            else 
            {
                // Player has already completed the quest, no more quest point earned
                // and no new state persists.
                questPointsEarned = 0.0;
            }

            // prepare return
            var output = new ProgressResult {
                QuestPointsEarned = questPointsEarned,
                TotalPercentCompleted = percenCompleted,
                MilestonesCompleted = milestones
            };

            // return result
            return output;
        }
    }
}