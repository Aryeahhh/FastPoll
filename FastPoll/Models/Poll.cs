using Microsoft.CodeAnalysis.Options;
using System.ComponentModel.DataAnnotations;

namespace FastPoll.Models
{
    public class Poll
    {
        public int PollId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Question { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public List<Option> Options { get; set; } = new List<Option>();
    }
}
