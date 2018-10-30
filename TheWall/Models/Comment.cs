using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TheWall.Models {
    public class Comment {      
        [Key]
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int MessageId { get; set; }

        public DateTime Date { get; set; }

        public string AuthorName {get; set;}

        [Required (ErrorMessage = "Comment field cannot be blank")]
        [MinLength (10, ErrorMessage = "Comment must be at least 10 characters")]
        [Display (Name = "Comment: ")]
        public string WallComment { get; set; }

    }
}