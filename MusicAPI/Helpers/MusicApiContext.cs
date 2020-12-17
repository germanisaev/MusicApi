using Microsoft.EntityFrameworkCore;
using MusicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAPI.Controllers.V1
{
    public class MusicApiContext : DbContext
    {
        public MusicApiContext(DbContextOptions<MusicApiContext> options) : base(options) { }

        public DbSet<AlbumDetail> AlbumDetails { get; set; }
        public DbSet<SongDetail> SongDetails { get; set; }
        public DbSet<PlaylistDetail> PlaylistDetails { get; set; }
        public DbSet<SortDetail> SortDetails { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SongsPlatlistDetail> SongsPlatlistDetails { get; set; }
        public DbSet<UserDto> UserDtos { get; set; }
    }
}
