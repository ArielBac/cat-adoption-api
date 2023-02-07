namespace CatAdotionApi.Data.Dtos;

public class ReadCatDto
{
    public string Name { get; set; } = null!;
    public string? Breed { get; set; }
    public double Weight { get; set; }
    public string Color { get; set; } = null!;
    public int Age { get; set; }
    public string Gender { get; set; } = null!;
}

