using System.ComponentModel.DataAnnotations;

namespace ComplexObject.Data
{
    public class Update
    {
        [Required]
        [MaxLength(140)]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
    }

}
