using System.ComponentModel.DataAnnotations;

namespace CatAdoptionApi.Data.Dtos;

public class UpdateCatDto
{
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [StringLength(30, ErrorMessage = "O campo Nome não deve exceder 30 caracteres")]
    public string Name { get; set; } = null!;

    [StringLength(30, ErrorMessage = "O campo Raça não deve exceder 30 caracteres")]
    public string? Breed { get; set; }

    [Required(ErrorMessage = "O campo Peso é obrigatório")]
    [Range(0.1, 20, ErrorMessage = "O peso deve estar entre 0.1kg e 20kg")]
    public double Weight { get; set; }

    [Required(ErrorMessage = "O campo Cor é obrigatório")]
    [StringLength(30, ErrorMessage = "O campo Cor não deve exceder 30 caracteres")]
    public string Color { get; set; } = null!;

    [Required(ErrorMessage = "O campo Idade é obrigatório")]
    [Range(0.1, 30, ErrorMessage = "A idade deve estar entre 0.1 e 30 anos")]
    public int Age { get; set; }

    [Required(ErrorMessage = "O campo Gênero é obrigatório")]
    public string Gender { get; set; } = null!;
}

