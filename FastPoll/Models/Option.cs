using System.ComponentModel.DataAnnotations;

namespace FastPoll.Models
{
    public class Option
    {
        public int OptionId { get; set; }

        [Required]
        public int PollId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Text { get; set; }

        [Required]
        public int Votes { get; set; }

        public Poll? Poll { get; set; }
    }
}
