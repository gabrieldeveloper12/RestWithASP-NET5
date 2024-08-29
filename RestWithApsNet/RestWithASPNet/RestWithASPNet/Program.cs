using Microsoft.EntityFrameworkCore;
using RestWithASPNet.Model.Context;
using RestWithASPNet.Business;
using RestWithASPNet.Business.Implementations;
using MySqlConnector;
using EvolveDb;
using Serilog;
using RestWithASPNet.Repository.Generic;
using System.Net.Http.Headers;
using RestWithASPNet.HyperMedia.Filters;
using RestWithASPNet.HyperMedia.Enricher;
using RestWithASPNet.Data.Converter.Implementation;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddCors(options => {
	options.AddDefaultPolicy(b => {
		b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
	});
});



builder.Services.AddControllers();

string conn = builder.Configuration.GetConnectionString("MySQLConnectionString");
builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

if (builder.Environment.IsDevelopment())
{
    MigrateDataBase(conn);
}

builder.Services.AddMvc(options => {
	options.RespectBrowserAcceptHeader = true;

	options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml").ToString());
	options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json").ToString());
}).AddXmlSerializerFormatters();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<PersonConverter>();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponsEnricherList.Add(new PersonEnricher());
builder.Services.AddSingleton(filterOptions);

builder.Services.AddApiVersioning();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => {
	opt.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "REST APIs",
		Version = "v1",
		Description = "REST API's",
		Contact = new OpenApiContact
		{
			Name = "Gabriel"
		}
	});

});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseSwagger();

app.UseSwaggerUI(c => {
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API's - v1");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute("DefaultApi", "{controller = values}/{id}");

app.Run();

void MigrateDataBase(string? conn)
{
	try
	{
		var evolveConnection = new MySqlConnection(conn);
		var evolve = new Evolve(evolveConnection, Log.Information)
		{
			Locations = new List<string> { "db/migrations", "db/dataset"}
		};
		evolve.Migrate();

	}
	catch (Exception ex)
	{
		Log.Error("Database migration failed", ex);
		throw;
	}
}