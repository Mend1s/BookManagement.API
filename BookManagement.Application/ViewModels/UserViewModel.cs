﻿namespace BookManagement.Application.ViewModels;

public class UserViewModel
{
    public UserViewModel(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
