var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var app = builder.Build();

//  middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();    


app.MapControllers();

app.Run();
