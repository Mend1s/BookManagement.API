using BookManagement.Application.Commands.CreateBook;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence;
using BookManagement.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BooksManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookManagement")));

builder.Services.AddControllers();
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssembly(typeof(CreateBookCommand).Assembly));

builder.Services.AddScoped<IBookReposiroty, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
