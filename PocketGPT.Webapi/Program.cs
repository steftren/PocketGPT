using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
// SpengernewsContext ist der DbContext, der im Application Project angelegt wurde.
// Aktiviere diese Zeile, wenn du den DB Context definiert hat.
// builder.Services.AddDbContext<SpengernewsContext>(opt =>
//     opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Wir wollen automatisch nach Controllern im Ordner Controllers suchen.
builder.Services.AddControllers();
// Der Vue.JS Devserver läuft auf einem anderen Port, deswegen brauchen wir diese Konfiguration
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
        options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
}

// *************************************************************************************************
// APPLICATION
// *************************************************************************************************
var app = builder.Build();
// Leitet http auf https weiter (http Port 5000 auf https Port 5001)
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseCors();
}

// Im Development Mode erstellen wir bei jedem Serverstart die Datenbank neu.
// Aktiviere diese Zeilen, wenn du den DB Context erstellt hat.
// using (var scope = app.Services.CreateScope())
// {
//     using (var db = scope.ServiceProvider.GetRequiredService<SpengernewsContext>())
//     {
//         db.CreateDatabase(isDevelopment: app.Environment.IsDevelopment());
//     }
// }

// Liefert die statischen Dateien, die von VueJS generiert werden, aus.
app.UseStaticFiles();
// Bearbeitet die Routen, für die wir Controller geschrieben haben.
app.MapControllers();
// Wichtig für das clientseitige Routing, damit wir direkt an eine URL in der Client App steuern können.
app.MapFallbackToFile("index.html");
app.Run();
