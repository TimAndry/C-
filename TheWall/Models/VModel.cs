using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using TheWall.Models;

namespace TheWall.Models
{
    public class VModel
    {
        public Message MessageForm {get; set;}
        public Comment CommentForm {get; set;}
    }
}