using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAPI.Controllers.V1
{
    public class SortDetail
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "int")]
        public int UserId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Artists { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Albums { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public int Songs { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Genres { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Created { get; set; }
    }
}
