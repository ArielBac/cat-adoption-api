using CatAdoptionApi.ViewModels;

namespace CatAdoptionApi.Requests.Vaccines;

public class GetVaccineRequest
{
    public int Id { get; set; }
    public int CatId { get; set; }
    public DateTime Applied_at { get; set; }
    public string Name { get; set; } = null!;
    public string Producer { get; set; } = null!;
    public CatViewModel? Cat { get; set; }
}

