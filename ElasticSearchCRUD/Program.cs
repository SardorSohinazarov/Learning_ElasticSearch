using ElasticSearchCRUD.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ElasticSearchService>();

#region Agar IElasticClient dan constructor inject orqali foydalanmoqchi bo'lsangiz
// ElasticClient ni containerga registratsiya qilish
//builder.Services.AddSingleton<IElasticClient>(provider =>
//{
//    var config = provider.GetRequiredService<IConfiguration>();

//    var settings = new ConnectionSettings(new Uri(config["ElasticSearch:Url"]))
//        .DefaultIndex(config["ElasticSearch:Index"]);
//        //.BasicAuthentication(config["ElasticSearch:Username"], config["ElasticSearch:Password"]) // agar kerak bo‘lsa
//        //.EnableApiVersioningHeader(false) // yoki kerakli sozlamalar

//    return new ElasticClient(settings);
//});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();