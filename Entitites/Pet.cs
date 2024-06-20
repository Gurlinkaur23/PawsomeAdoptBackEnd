using System.ComponentModel.DataAnnotations;

namespace PawsomeAdoptBackEnd.Entitites
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string ImageUrl { get; set; }
    }
}
