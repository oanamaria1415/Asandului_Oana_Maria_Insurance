namespace Asandului_Oana_Maria_Insurance.Models
{
    public class ChargesPredictionViewModel
    {
        public float Age { get; set; }
        public string Sex { get; set; } = "female";
        public float Bmi { get; set; }
        public float Children { get; set; }
        public bool Smoker { get; set; }
        public string Region { get; set; } = "southwest";

        public float? PredictedCharges { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
