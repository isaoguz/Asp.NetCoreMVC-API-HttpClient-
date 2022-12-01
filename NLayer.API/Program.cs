using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.API.Modules;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using NLayer.Service.Validation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>  options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidatior>());


//Burada diyoruz ki sen bize filter d�nme biz kendimizinkini kullanaca��z.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    //kendi filtremizi aktif hale getirdik sistemin kendisini pasif yapt�k.
    options.SuppressModelStateInvalidFilter = true;
});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Cachingi implemente etmek i�in.
builder.Services.AddMemoryCache();


builder.Services.AddScoped(typeof(NotFoundFilter<>));
//Mapping i�lemlerini entegre etmek i�in
builder.Services.AddAutoMapper(typeof(MapProfile));


//Veri Taban� Adresimiz i�in
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),option =>
    {
        //burada diyoruzki appdbcontextin bulundu�u assmepliye migrationsu olu�tur.
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});


//AutoFac i�lemini ger�ekle�tirmek i�in.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//AutoFac mod�l�n� tan�mlad�k.
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Global exceptionumuzu entegre ettik.
app.UserCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
