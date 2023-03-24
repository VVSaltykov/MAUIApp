using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAUIAppCommon.Models
{
    public class File
    {
        [Key]
        public int FileId { get; set; }
        public string? FileName { get; set; }
        public string? Path { get; set; }
        public int? DeleteStatus { get; set; } = 0;
    }
}
