using BodyaFen_API_;
using BodyaFen_API_.Contexts;
using BodyaFen_API_.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddOData(opts => opts.AddRouteComponents("odata", GetEdmModel())
    .Count().Filter().OrderBy().Expand().Select().SetMaxTop(100)
);



// Add services to the container.
builder.Services.AddHttpClient("Privat24");
builder.Services.AddScoped<BonkRequester>();
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
object value = builder.Services.AddDbContext<BodyaFenDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseDefaultFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

IEdmModel GetEdmModel()
{
    var edmBuilder = new ODataConventionModelBuilder();
    edmBuilder.EntitySet<Song>("Songs");
    return edmBuilder.GetEdmModel();
}