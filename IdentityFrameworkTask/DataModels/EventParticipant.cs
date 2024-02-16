using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace IdentityFrameworkTask.DataModels
{
    public class EventParticipant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }

        public int EventID { get; set; }
        public Event Event { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }
    }
}
