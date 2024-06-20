using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawsomeAdoptBackEnd.Entitites
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }

        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PetName { get; set; }
        public string PetCategory { get; set; }

        // Navigation property
        public Pet Pet { get; set; }
    }
}
