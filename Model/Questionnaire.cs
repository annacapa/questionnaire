using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecard.Model
{
    public class Questionnaire
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("What is the first concert you attended?")]
        [Display(Prompt = "First Concert?")]
        [Required(ErrorMessage = "Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You must enter between 2 to 100 characters")]
        public string Concert { get; set; }

        [DisplayName("What was your best Halloween costume?")]
        [Display(Prompt = "Best Costume?")]
        [Required(ErrorMessage = "Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You must enter between 2 to 100 characters")]
        public string Costume { get; set; }

        [DisplayName("What is your hidden talent?")]
        [Display(Prompt = "Make it not so hidden here.")]
        [Required(ErrorMessage = "Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You must enter between 2 to 100 characters")]
        public string Talent { get; set; }

        [DisplayName("What is your dream job?")]
        [Display(Prompt = "Dream it and bring it here.")]
        [Required(ErrorMessage = "Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You must enter between 2 to 100 characters")]
        public string Job { get; set; }

        [DisplayName("Where do you most hope to visit?")]
        [Display(Prompt = "Start planning now by writing it here.")]
        [Required(ErrorMessage = "Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You must enter between 2 to 100 characters")]
        public string Visit { get; set; }

        //TEMPORARY VARIABLE FOR CHECKBOX USE ONLY
        [NotMapped]
        public IEnumerable<string> Visit_array { get; set; }

        public string created { get; set; }

        public string created_ip { get; set; }
    }
}
