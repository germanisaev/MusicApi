using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAPI.Models
{
    public class AlbumDetail
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Artist { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Photo { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Album { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Genres { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Created { get; set; }
    }
}
