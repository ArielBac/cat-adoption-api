using System.ComponentModel.DataAnnotations;

namespace CatAdoptionApi.Data.Dtos.Vaccines;

public class CreateVaccineDto
{
 
    [Required(ErrorMessage = "O campo ID do Gatinho é obrigatório")]
    public int CatId { get; set; }

    [Required(ErrorMessage = "O campo Data da Aplicação é obrigatório")]
    public DateTime Applied_at { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [StringLength(30, ErrorMessage = "O campo Nome não deve exceder 30 caracteres")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "O campo Fabricante é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo Fabricante não deve exceder 50 caracteres")]
    public string Producer { get; set; } = null!;
}

