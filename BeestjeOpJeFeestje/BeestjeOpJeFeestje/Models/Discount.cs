namespace BeestjeOpJeFeestje.Models
{
    public class Discount
    {
        public string Type { get; set; }
        public int Percentage { get; set; }
        public Discount(string Type, int Percentage) 
        {
            this.Type = Type;
            this.Percentage = Percentage;
        }
    }
}