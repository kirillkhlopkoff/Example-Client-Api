using System.ComponentModel.DataAnnotations;

namespace TestApi_ClientMVC.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
