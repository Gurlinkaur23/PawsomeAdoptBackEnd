using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsomeAdoptBackEnd.Context;
using PawsomeAdoptBackEnd.DTOs;
using PawsomeAdoptBackEnd.Entitites;

namespace PawsomeAdoptBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class applicationsController : ControllerBase
    {
        private readonly PawsomeContext _context;
        private readonly IMapper _mapper;

        public applicationsController(PawsomeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{email}")]
        public ActionResult<ApplicationDTO> GetByEmail(string email)
        {
            var form = _context.Applications.FirstOrDefault(a => a.Email.ToLower() == email.ToLower());
            if (form == null)
            {
                return NotFound();
            }

            var formDto = new ApplicationDTO
            {
                FullName = form.FullName,
                Email = form.Email,
                PhoneNumber = form.PhoneNumber,
                PetName = form.PetName,
                PetCategory = form.PetCategory
            };

            return Ok(formDto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ApplicationDTO formDto)
        {
            // First, find the associated pet by name and category to ensure it exists.
            var pet = _context.Pets.FirstOrDefault(p => p.Name.ToLower() == formDto.PetName.ToLower()
                                                        && p.Category.ToLower() == formDto.PetCategory.ToLower());
            if (pet == null)
            {
                // If the pet doesn't exist, return a NotFound (or you might want to handle it differently)
                return NotFound("The specified pet does not exist.");
            }

            // Check if the pet is already adopted
            if (pet.Status.ToLower() == "adopted")
            {
                return BadRequest("This pet has already been adopted.");
            }

            // Create the application using the found PetId
            var form = new Application
            {
                PetId = pet.PetId, // Set the PetId from the found pet
                FullName = formDto.FullName,
                Email = formDto.Email,
                PhoneNumber = formDto.PhoneNumber,
                PetName = formDto.PetName,
                PetCategory = formDto.PetCategory
            };

            _context.Applications.Add(form);

            // Optionally update the pet status if needed
            pet.Status = "Adopted";
            _context.Pets.Update(pet);

            _context.SaveChanges();

            var formDtoResponse = new ApplicationDTO
            {
                FullName = form.FullName,
                Email = form.Email,
                PhoneNumber = form.PhoneNumber,
                PetName = form.PetName,
                PetCategory = form.PetCategory
            };

            return Ok("Application added successfully.");
        }

    }
}
