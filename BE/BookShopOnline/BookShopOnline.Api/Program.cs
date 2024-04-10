using BookShopOnline.Api.Middleware;
using BookShopOnline.Core.AutoMapper;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Interfaces.Services.Base;
using BookShopOnline.Core.Interfaces.UnitOfWork;
using BookShopOnline.Core.Services;
using BookShopOnline.Infrastructure.DbContext;
using BookShopOnline.Infrastructure.Interface;
using BookShopOnline.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adding Jwt Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,

        ValidateLifetime = true,//kiểm tra token hết hạn
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        //chỉ ra key mà token sẽ dùng sau này
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Handle pascalcase ouput result Json
builder.Services.AddControllers()
.AddJsonOptions(x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
});

//Add the CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//config Auto Mapper
builder.Services.AddAutoMapper
(typeof(AutoMapperProfile).Assembly);

//config DI
builder.Services.AddScoped<IDbContext, MariaDbContext>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IJWTAuthenticationService, JWTAuthenticationService>();
builder.Services.AddScoped<IJWTAuthenticationRepository, JWTAuthenticationRepository>();

builder.Services.AddScoped<IDeliveryAddressService, DeliveryAddressService>();
builder.Services.AddScoped<IDeliveryAddressRepository, DeliveryAddressRepository>();

builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

builder.Services.AddScoped<ICartRepository, CartRepository>();


builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Use the CORS policy
app.UseCors("AllowOrigin");

//config middleware
app.UseMiddleware<ExceptionMiddleware>();

//Tạo file tĩnh wwwroot
app.UseStaticFiles();

app.Run();
