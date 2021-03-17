namespace CovidSim.PersonClasses
{
    public class PersonOptions
    {
        /// <summary>
        /// Probability of complying to quarantine regulations. Default 100%
        /// </summary>
        public Modifier QuarantineCompliance { get; set; } = new() {Value = 80};

        /// <summary>
        /// Probability of complying to mandatory mask/glove regulations. Default 90%
        /// Source https://www.statista.com/statistics/1114375/wearing-a-face-mask-outside-in-european-countries/
        /// </summary>
        public Modifier GearCompliance { get; set; } = new() {Value = 90};

        /// <summary>
        /// Days to become healthy again. Patient may die during this time. Default is 14 days
        /// Source https://www.cdc.gov/coronavirus/2019-ncov/hcp/clinical-guidance-management-patients.html#:~:text=Among%20patients%20in%20multiple%20early,median%20time%20from%20onset%20of
        /// </summary>
        public short CureCountdown { get; set; } = 14;

        /// <summary>
        /// Days before symptoms show. Default is 5 days
        /// Source https://www.who.int/news-room/commentaries/detail/transmission-of-sars-cov-2-implications-for-infection-prevention-precautions#:~:text=The%20incubation%20period%20of%20COVID,to%20a%20confirmed%20case.
        /// </summary>
        public short SymptomCountdown { get; set; } = 5;

        /// <summary>
        /// Probability of more serious symptoms. Default 20%
        /// Source https://www.webmd.com/lung/covid-19-symptoms
        /// Source https://www.healthline.com/health-news/what-its-like-to-survive-covid-19
        /// </summary>
        public Modifier EscalatedSymptoms { get; set; } = new Modifier {Value = 20};

        /// <summary>
        /// Probability of dying due to COVID-19 serious symptoms or related complications
        /// Source https://coronavirus.jhu.edu/data/mortality
        /// </summary>
        public Modifier DeathRate { get; set; } = new Modifier {Value = 8};

        /// <summary>
        /// Probability of patient staying asymptomatic
        /// Source https://www.webmd.com/lung/covid-19-symptoms
        /// Source https://www.healthline.com/health-news/what-its-like-to-survive-covid-19
        /// </summary>
        public Modifier AsymptomaticProbability { get; set; } = new Modifier {Value = 30};
    }
}