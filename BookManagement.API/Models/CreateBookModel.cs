﻿namespace BookManagement.API.Models;

public class CreateBookModel
{
    // verificar se é pra tirar esse tipo de model ou nao
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int YearOfPublication { get; set; }
}