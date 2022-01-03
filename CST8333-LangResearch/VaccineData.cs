//<Author = Anoopbir Singh Sidhu>
//<Sno = 040984994>

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    /// <summary>
    /// The Model class which contains the DTO in whose form information is stored
    /// </summary>
    public class VaccineData
    {
        /// <summary>
        /// The unique Id mongo generates for its Bson document objects
        /// </summary>
        [BsonId]
        public ObjectId Id { get; private set; }

        /// <summary>
        /// The province ID
        /// </summary>
        public string Pruid { get; set; }

        /// <summary>
        /// The province's name in English
        /// </summary>
        public string PreName { get; set; }

        /// <summary>
        /// The province's name in French
        /// </summary>
        public string PrfName { get; set; }

        /// <summary>
        /// Date when the data was collected posted
        /// </summary>
        public string WeekEnd { get; set; }

        /// <summary>
        /// Name of the vaccine supplier
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Number of people who have had one shot or more of the vaccine
        /// </summary>
        public string NumTotalAtLeast1Dose { get; set; }

        /// <summary>
        /// Number of people who have been partially vaccinated
        /// </summary>
        public string NumTotalPartially { get; set; }

        /// <summary>
        /// Number of fully vaccinated people
        /// </summary>
        public string NumTotalFully { get; set; }

        /// <summary>
        /// Proportion of people who have had one shot or more of the vaccine
        /// </summary>
        public string PropAtLeast1Dose { get; set; }

        /// <summary>
        /// Proportion of people who have been partially vaccinated
        /// </summary>
        public string PropPartially { get; set; }

        /// <summary>
        /// Proportion of fully vaccinated people
        /// </summary>
        public string PropFully { get; set; }

        /// <summary>
        /// Constructor for instantiating new objects in one line
        /// </summary>
        public VaccineData(string[] vaccineData)
        {
            Pruid = vaccineData[0];
            PreName = vaccineData[1];
            PrfName = vaccineData[2];
            WeekEnd = vaccineData[3];
            ProductName = vaccineData[4];
            NumTotalAtLeast1Dose = vaccineData[5];
            NumTotalPartially = vaccineData[6];
            NumTotalFully = vaccineData[7];
            PropAtLeast1Dose = vaccineData[8];
            PropPartially = vaccineData[9];
            PropFully = vaccineData[10];
        }
    }
}
