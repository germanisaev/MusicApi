﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAPI.Controllers.V1
{
    public class PlaylistDetail
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Photo { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Created { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public SortDetail Sorted { get; set; }
        [Column(TypeName = "int")]
        public int UserId { get; set; }
    }
}
