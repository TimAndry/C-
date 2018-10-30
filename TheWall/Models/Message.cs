using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models {
    public class Message {      
        [Key]
        public int MessageId { get; set; }
        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public string AuthorName {get; set;}

        [Required (ErrorMessage = "Message field cannot be blank")]
        [MinLength (10, ErrorMessage = "Message must be at least 10 characters")]
        [Display (Name = "Message: ")]
        public string WallMessage { get; set; }

        public List<Comment> Comments { get; set; }

        public Message () {
            Comments = new List<Comment> ();
        }
    }
}