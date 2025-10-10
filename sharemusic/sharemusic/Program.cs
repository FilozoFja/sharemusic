using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using sharemusic.Interface;
using sharemusic.Service;

var builder = WebApplication.CreateBuilder(args);

var projectRoot = Path.Combine(AppContext.BaseDirectory, "..", "..", "..");
var storagePath = Path.Combine(projectRoot, "FileStorage");

storagePath = Path.GetFullPath(storagePath);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddDbContext<sharemusic.Db.MusicDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISongService,SongService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ISpotifyService, SpotifyService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IListeningHistoryService, ListeningHistoryService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();

builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(storagePath),
    RequestPath = "/files"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
