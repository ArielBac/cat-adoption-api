﻿namespace CatAdoptionApi.Models;

public class Vaccine
{
    public int Id { get; set; }
    public int CatId { get; set; }
    public DateTime Applied_at { get; set; }
    public string Name { get; set; } = null!;
    public string Producer { get; set; } = null!;

    public Cat? Cat { get; set; }
}
