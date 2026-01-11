namespace Asandului_Oana_Maria_Insurance.Models
{
    public class ChargesApiRequest
    {
        public float Age { get; set; }
        public string Sex { get; set; } = "";
        public float Bmi { get; set; }
        public float Children { get; set; }
        public bool Smoker { get; set; }
        public string Region { get; set; } = "";
        public float Charges { get; set; } = 0;
    }
}
