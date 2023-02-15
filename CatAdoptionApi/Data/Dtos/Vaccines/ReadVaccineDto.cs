using CatAdoptionApi.Data.ViewModels;

namespace CatAdoptionApi.Data.Dtos.Vaccines;

public class ReadVaccineDto
{
    public int Id { get; set; }

    public int CatId { get; set; }

    public DateTime Applied_at { get; set; }

    public string Name { get; set; } = null!;

    public string Producer { get; set; } = null!;

    public CatViewModel? Cat { get; set; }
}

