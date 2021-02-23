namespace CovidSim.PersonClasses
{
    public class PersonOptions
    {
        /// <summary>
        /// Probability of complying to quarantine regulations. Default 100%
        /// </summary>
        public Modifier QuarantineCompliance { get; set;} = new Modifier{Value = 100};

        /// <summary>
        /// Probability of complying to mandatory mask/glove regulations. Default 80%
        /// </summary>
        public Modifier GearCompliance { get; set; } = new Modifier {Value = 80};

        /// <summary>
        /// Days to become healthy again. Patient may die during this time. Default is 14 days
        /// </summary>
        public short CureCountdown { get; set; } = 14;

        /// <summary>
        /// Days before symptoms show. Default is 5 days
        /// </summary>
        public short SymptomCountdown { get; set; } = 5;

        /// <summary>
        /// Probability of more serious symptoms. Default 20%
        /// </summary>
        public Modifier EscalatedSymptoms { get; set; } = new Modifier {Value = 20};

        /// <summary>
        /// Probability of dying due to COVID-19 serious symptoms or related complications
        /// </summary>
        public Modifier DeathRate { get; set; } = new Modifier {Value = 8};

        /// <summary>
        /// Probability of patient staying asymptomatic
        /// </summary>
        public Modifier AsymptomaticProbability { get; set; } = new Modifier {Value = 30};
    }
}
