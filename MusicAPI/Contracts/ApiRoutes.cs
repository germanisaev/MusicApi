using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAPI.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class PlayList
        {
            public const string GetAll = Base + "/playlist";
            public const string GetBy = Base + "/playlist/{id}";
            public const string GetByUser = Base + "/playlist/user/{id}";
            public const string Create = Base + "/playlist";
            public const string Update = Base + "/playlist/{id}";
            public const string Delete = Base + "/playlist/{id}";
        }

        public static class SongsPlayList
        {
            public const string GetAll = Base + "/playlist/songs";
            public const string GetBy = Base + "/playlist/song/{id}";
            public const string GetByUser = Base + "/playlist/song/user/{id}";
            public const string GetByPlaylist = Base + "/playlist/songs/{id}";
            public const string Create = Base + "/playlist/song";
            public const string Update = Base + "/playlist/song/{id}";
            public const string DeleteBySong = Base + "/playlist/song/{id}";
            public const string DeleteByPlaylist = Base + "/song/playlist/{id}";
        }

        public static class AlbumList
        {
            public const string GetAll = Base + "/album";
            public const string GetBy = Base + "/album/{id}";
            public const string Create = Base + "/album";
            public const string Update = Base + "/album/{id}";
            public const string Delete = Base + "/album/{id}";
        }

        public static class SongList
        {
            public const string GetAll = Base + "/song";
            public const string GetBy = Base + "/song/{id}";
            public const string GetByUser = Base + "/song/user/{id}";
            public const string GetByAlbum = Base + "/song/album/{id}";
            public const string Create = Base + "/song";
            public const string Update = Base + "/song/{id}";
            public const string Delete = Base + "/song/{id}";
        }

        public static class SortList
        {
            public const string GetAll = Base + "/sort";
            public const string GetByUser = Base + "/sorts/user/{id}";
            public const string GetArtists = Base + "/sort/artist";
            public const string GetCreateds = Base + "/sort/created/{name}";
            public const string GetByArtist = Base + "/sort/artist/{id}";
            public const string GetByCreated = Base + "/sort/created/{id}";
            public const string Create = Base + "/sort";
            public const string Update = Base + "/sort/{id}";
            public const string PutByUser = Base + "/sortput/user/{id}";
            public const string PatchByUser = Base + "/sortpatch/user/{id}";
            public const string Delete = Base + "/sort/{id}";
            public const string DeleteByUser = Base + "/sort/user/{id}";
        }

        public static class UserList
        {
            public const string GetAll = Base + "/users";
            public const string GetBy = Base + "/users/{id}";
            public const string Register = Base + "/users/register";
            public const string Authenticate = Base + "/users/authenticate";
            public const string Update = Base + "/users/{id}";
            public const string Delete = Base + "/users/{id}";
        }

        public static class Identity
        {
            public const string GetAll = Base + "/identity";
            public const string GetBy = Base + "/identity/{id}";
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
        }
    }
}
