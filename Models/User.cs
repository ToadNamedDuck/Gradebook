using System;
using System.ComponentModel.DataAnnotations;

namespace Gradebook.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(28, MinimumLength = 28)]
        public string FirebaseId { get; set; }
    }
}
