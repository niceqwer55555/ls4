﻿namespace Chronobreak.GameServer.Content
{
    public class MinionEXPMods
    {
        /// <summary>
        /// Bonus Experience Percentage per neutral minion level ahead
        /// </summary>
        public float BonusExpPercentPerNeutralMinionLevel { get; set; } = 0.0f;
        /// <summary>
        /// Bonus Experience Percentage per lane minion level ahead
        /// </summary>
        public float BonusExpPercentPerLaneMinionLevel { get; set; } = 0.0f;
        /// <summary>
        /// The maximum delta in minion levels where XP can be granted.
        /// </summary>
        public int BonusExpLevelDeltaCap { get; set; } = 0;
        /// <summary>
        /// Minimum level of lane minion before XP kicks in (to avoid lane starvation strategies)
        /// </summary>
        public int BonusExpLaneLevelStart { get; set; } = 0;
        /// <summary>
        /// Minimum level difference before you get bonus lane XP
        /// </summary>
        public int BonusExpLaneLevelDeltaMin { get; set; } = 0;
    }
}
