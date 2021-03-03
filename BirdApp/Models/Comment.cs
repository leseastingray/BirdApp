using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BirdApp.Models
{
    public class Comment
    {
        // Id for primary key in the database
        [Key]
        public int CommentID { get; set; }

        [Required(ErrorMessage = "Comment text is required.")]
        public string CommentText { get; set; }
        // For Comment time
        public DateTime CommentDate { get; set; }

        // Refers to BirdWatcher class object; each Comment can only be associated with one BirdWatcher
        public BirdWatcher Commenter { get; set; }

        // Refers to Review class object; each Comment can only be associated with one Review
        //public Review CommentReview { get; set; }
    }
}
