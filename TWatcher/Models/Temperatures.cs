namespace TWatcher.Models
{
    public class Temperatures
    {
        public double CPU { get; set; }
        public double GPU { get; set; }

        public override string ToString()
        {
            return $"CPU: {CPU} GPU: {GPU} T";
        }
    }
}
