using System.ComponentModel.DataAnnotations;
using SavePets.Business.Models.Abstract;

namespace SavePets.Business.Models.Response
{
    public class ContactsResponse : BaseResult{
        public string? TelegramUrl { set; get; } = string.Empty;
        public string? InstagramUrl { set; get; } = string.Empty;
        public string? FacebookUrl { set; get; } = string.Empty;
        public string? ViberUrl { set; get; } = string.Empty;
        public string? Phone { set; get; } = string.Empty;
    }
}
