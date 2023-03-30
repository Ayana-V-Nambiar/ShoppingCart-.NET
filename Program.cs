using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShoppingCart.Context;
using ShoppingCart.ServiceLayer;
using ShoppingCart.ShoppingCart.Repository;
using ShoppingCart.ShoppingCart.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(C =>
{
    C.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnetClaimAuthorization", Version = "v1" });
    C.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme{
        In = ParameterLocation.Header,
        Description = "Please Insert Token",
        Name ="Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    C.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[]{}
    }
    });
});


 
builder.Services.AddCors(option => 
//TO Solve angular connection error
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

//connection string
builder.Services.AddDbContext<AppDbContext>(option=>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnStr"));
});

//only after configure this dependency injection will work
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemService, CartService>();


// builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// builder.Services.AddScoped<ICategoryService, CategoryService>();



//Configure the Token
builder.Services.AddAuthentication(x =>
{
     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret.....")),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//FOR ENABLE TOKEN BEARER FUNCTION IN SWAGGER
app.UseSwagger();
app.UseSwaggerUI(c =>{
    c.SwaggerEndpoint("/swagger/v1/swagger.json","MY API"); 

});



app.UseHttpsRedirection();
//pipeline added after // angular conn error
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


