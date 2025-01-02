using MedicalAppointmentManagement.Application.UseCases.Appointments.Delete;
using MedicalAppointmentManagement.Application.UseCases.Appointments.GetById;
using MedicalAppointmentManagement.Application.UseCases.Appointments.Register;
using MedicalAppointmentManagement.Application.UseCases.Doctors.GetAll;
using MedicalAppointmentManagement.Application.UseCases.Doctors.Register;
using MedicalAppointmentManagement.Application.UseCases.Doctors.Update;
using MedicalAppointmentManagement.Application.UseCases.Login;
using MedicalAppointmentManagement.Application.UseCases.Users.GetAll;
using MedicalAppointmentManagement.Application.UseCases.Users.Register;
using MedicalAppointmentManagement.Application.UseCases.Users.Update;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Domain.Security.Cryptography;
using MedicalAppointmentManagement.Domain.Tokens;
using MedicalAppointmentManagement.Infrastructure.DateAccess;
using MedicalAppointmentManagement.Infrastructure.Repository;
using MedicalAppointmentManagement.Infrastructure.Security.Cryptography;
using MedicalAppointmentManagement.Infrastructure.Security.Jwt;
using MedicalAppointmentManagement.Infrastructure.Security.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Settings:Jwt").Get<JwtSettings>();

var signingKey = builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey");

builder.Services.AddSingleton(jwtSettings!);
builder.Services.AddScoped<IAccessTokenGenerator, JwtTokenGenerator>();


builder.Services.AddControllers();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = new TimeSpan(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!))
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey
                      
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<MedicalAppointmentDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
});



//Repositories
builder.Services.AddScoped<IPatientWriteOnlyRepository, PatientRepository>();
builder.Services.AddScoped<IPatientReadOnlyRepository, PatientRepository>();

builder.Services.AddScoped<IDoctorWriteOnlyRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorReadOnlyRepository, DoctorRepository>();

builder.Services.AddScoped<IAppointmentWriteOnlyRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentReadOnlyRepository, AppointmentRepository>();

builder.Services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
builder.Services.AddScoped<IUserReadOnlyRepository, UserRepository>();

builder.Services.AddScoped<IPasswordEncripter, Cryptography>();

//UseCases
builder.Services.AddScoped<IRegisterPatientUseCase, RegisterPatientUseCase>();
builder.Services.AddScoped<IGetAllPatientsUseCase, GetAllPatientsUseCase>();
builder.Services.AddScoped<IUpdatePatientUseCase, UpdatePatientUseCase>();
builder.Services.AddScoped<IRegisterDoctorUseCase, RegisterDoctorUseCase>();
builder.Services.AddScoped<IGetAllDoctorUseCase, GetAllDoctorUseCase>();
builder.Services.AddScoped<IUpdateDoctorUseCase, UpdateDoctorUseCase>();
builder.Services.AddScoped<IGetByIdAppointmentUseCase, GetByIdAppointmentUseCase>();

builder.Services.AddScoped<IRegisterAppointmentUseCase, RegisterAppointmentUseCase>();
builder.Services.AddScoped<IDeleteByIdAppointmentUseCase, DeleteByIdAppointmentUseCase>();

builder.Services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
builder.Services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();

builder.Services.AddAuthorization();





var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
