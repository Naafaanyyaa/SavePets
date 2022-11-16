namespace SavePets.Business.Models.PayModels
{
    public class PayOptions
    {
        public string Version { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
    }
}