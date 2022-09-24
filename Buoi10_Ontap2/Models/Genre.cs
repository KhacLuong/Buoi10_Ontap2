using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Buoi10_Ontap2.Models
{
    public class Genre
    {
        
       
        [Key]
        public long GenreId { get; set; }

        [Required(ErrorMessage = "Không được bỏ qua trường này")]
        public string GenreName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }

}