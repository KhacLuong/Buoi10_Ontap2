using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Buoi10_Ontap2.Models
{
    public class Movie
    {
        [Key]
        public long MovieId { get; set; }

        [Required(ErrorMessage = "Không được bỏ qua trường này")]
        public string MovieTitle { get; set; }

        [Required(ErrorMessage = "Không được bỏ qua trường này")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Không được bỏ qua trường này")]
        [Range(0,int.MaxValue, ErrorMessage ="thời lượng không hợp lệ")]
        public int RunningTime { get; set; }

        [Required(ErrorMessage = "Không được bỏ qua trường này")]
        public long GenreId { get; set; }

        public virtual Genre Genres { get; set; }
    }

   
}