using SavePets.Business.Models.Abstract;

namespace SavePets.Business.Models.Response
{
    public class PhotoResponse : BaseResult
    {
        public string AnimalId { get; set; } = null!;
        public string FilePath { get; set; } = null!;
    }
}
