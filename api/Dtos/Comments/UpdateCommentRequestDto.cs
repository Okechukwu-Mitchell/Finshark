using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comments
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must not be less than 5 characters")]
        [MaxLength(180, ErrorMessage = "Title cannot be over 180 characters")]

         public string Title {get; set;}  = string.Empty;
        [MinLength(5, ErrorMessage = "Content must not be less than 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]
        public string Content {get; set;} = string.Empty;
    }
}