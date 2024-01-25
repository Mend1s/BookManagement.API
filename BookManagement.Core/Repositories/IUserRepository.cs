﻿using BookManagement.Core.Entities;

namespace BookManagement.Core.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<User> CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}
