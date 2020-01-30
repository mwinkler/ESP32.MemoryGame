using System;

namespace GameLogic
{
    public class Settings
    {
        /// <summary>
        /// Seed for the random generator
        /// </summary>
        public int RandomSeed { get; set; }

        /// <summary>
        /// Number of available leds
        /// </summary>
        public int LedCount { get; set; }

        /// <summary>
        /// Number of steps when start new game
        /// </summary>
        public int MinimumSolutionSteps { get; set; }

        /// <summary>
        /// Interval between every tick in game loop
        /// </summary>
        public TimeSpan TickInterval { get; set; }

        /// <summary>
        /// Interval between every step when display solution
        /// </summary>
        public TimeSpan StepInterval { get; set; }

        /// <summary>
        /// Duration of showing step when display solution
        /// </summary>
        public TimeSpan StepDuration { get; set; }
    }
}
