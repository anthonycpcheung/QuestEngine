using QuestEngine.Models;
using QuestEngine.PlayerQuestData;

namespace QuestEngine.Service
{
    public interface IQuestEngineService
    {
        // Get the current state of player
        StateOutput GetState(string playerId);

        // Advance the progress of player
        ProgressResult Progress(ProgressInput input);
    }
}