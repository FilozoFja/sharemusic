using sharemusic.Db;
using sharemusic.Interface;
using SpotifyAPI.Web;
using AutoMapper;
using sharemusic.Models;
using Microsoft.EntityFrameworkCore;
using sharemusic.DTO.PlaylistModel;
using sharemusic.DTO;

namespace sharemusic.Service;

public class PlaylistService : IPlaylistService
{
    private readonly MusicDbContext _musicDbContext;
    private readonly IMapper _mapper;

    public PlaylistService(MusicDbContext musicDbContext, IMapper mapper)
    {
        _musicDbContext = musicDbContext;
        _mapper = mapper;
    }
    public async Task AddPlaylistAsync(FullPlaylist playlistToAdd)
    {
        var playlist = _mapper.Map<PlaylistModel>(playlistToAdd);
        await _musicDbContext.Playlists.AddAsync(playlist);
        await _musicDbContext.SaveChangesAsync();
    }
    public async Task<PlaylistModel> AddPlaylistAsync(PlaylistModelDTO playlistToAdd)
    {
        var playlist = _mapper.Map<PlaylistModel>(playlistToAdd);
        await _musicDbContext.Playlists.AddAsync(playlist);
        await _musicDbContext.SaveChangesAsync();

        return playlist;
    }
    public async Task AddSongToPlaylistAsync(int playlistId, int songId)
    {
        var playlist = await _musicDbContext.Playlists.FindAsync(playlistId);
        if (playlist == null)
        {
            throw new Exception("Playlist not found.");
        }

        var song = await _musicDbContext.Songs.FindAsync(songId);
        if (song == null)
        {
            throw new Exception("Song not found.");
        }

        playlist.Songs.Add(song);
        _musicDbContext.Entry(playlist).State = EntityState.Modified;
        await _musicDbContext.SaveChangesAsync();
    }
    public async Task DeleteSongFromPlaylistAsync(int playlistId, int songId)
    {
        var playlist = await _musicDbContext.Playlists.FindAsync(playlistId);
        if (playlist == null)
        {
            throw new Exception("Playlist not found.");
        }

        var song = await _musicDbContext.Songs.FindAsync(songId);
        if (song == null)
        {
            throw new Exception("Song not found.");
        }

        playlist.Songs.Remove(song);
        _musicDbContext.Entry(playlist).State = EntityState.Modified;
        await _musicDbContext.SaveChangesAsync();
    }
    public async Task<PlaylistModel> GetPlaylistByIdAsync(int id)
    {
        var playlist = await _musicDbContext.Playlists
            .Include(p => p.Songs)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (playlist == null)
        {
            throw new Exception("Playlist not found.");
        }

        return playlist;
    }
    public async Task<List<PlaylistShortModelDTO>> GetPlaylistByNameAsync(string name, int? take = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Search term cannot be empty.", nameof(name));
        }

        name = name.Trim();

        var query = _musicDbContext.Playlists
            .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{name.ToLower()}%"));

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        var playlists = await query.ToListAsync();

        return _mapper.Map<List<PlaylistShortModelDTO>>(playlists);
    }

    public async Task<List<PlaylistShortModelDTO>> GetAllPlaylistsAsync()
    {
        var playlists = await _musicDbContext.Playlists
            .Include(p => p.Songs)
            .ToListAsync();

        if (playlists == null || !playlists.Any())
        {
            throw new Exception("No playlists found.");
        }

        return _mapper.Map<List<PlaylistShortModelDTO>>(playlists);
    }
    public async Task<PlaylistModel> UpdatePlaylistAsync(int id, PlaylistModelDTO updatedPlaylist)
    {
        var playlist = await _musicDbContext.Playlists.FindAsync(id);
        if (playlist == null)
        {
            throw new Exception("Playlist not found.");
        }

        _mapper.Map(updatedPlaylist, playlist);
        _musicDbContext.Entry(playlist).State = EntityState.Modified;
        await _musicDbContext.SaveChangesAsync();
        return playlist;
    }
    public async Task DeletePlaylistAsync(int id)
    {
        var playlist = await _musicDbContext.Playlists.FindAsync(id);
        if (playlist == null)
        {
            throw new Exception("Playlist not found.");
        }

        _musicDbContext.Playlists.Remove(playlist);
        await _musicDbContext.SaveChangesAsync();
    }
}