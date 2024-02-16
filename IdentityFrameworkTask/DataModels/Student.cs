using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityFrameworkTask.DataModels
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string Phone { get; set; }

        public int Age { get; set; }

        public ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
