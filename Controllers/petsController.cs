using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawsomeAdoptBackEnd.Context;
using PawsomeAdoptBackEnd.DTOs;
using PawsomeAdoptBackEnd.Entitites;

namespace PawsomeAdoptBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]

    public class petsController : ControllerBase
    {
        private readonly PawsomeContext _context;
        private readonly IMapper _mapper;

        public petsController(PawsomeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("category/{category}")]
        public ActionResult<IEnumerable<PetDTO>> GetByCategory(string category)
        {
            var pets = _context.Pets
                .Where(p => p.Category.ToLower() == category.ToLower())
                .Select(p => new PetDTO
                {
                    Name = p.Name,
                    Age = p.Age,
                    Category = p.Category,
                    Status = p.Status,
                    ImageUrl = p.ImageUrl
                }).ToList();

            return Ok(pets);
        }

        // Post Pet Api only for back-end
        [HttpPost]
        public IActionResult Post([FromBody] PetDTO petDto)
        {
            var pet = new Pet
            {
                Name = petDto.Name,
                Age = petDto.Age,
                Category = petDto.Category,
                Status = petDto.Status,
                ImageUrl = petDto.ImageUrl
            };

            _context.Pets.Add(pet);
            _context.SaveChanges();
            return Ok("Pet created successfully");
        }

        // Put Pet Api only for back-end
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PetDTO petDto)
        {
            var pet = _context.Pets.Find(id);
            if (pet == null)
            {
                return NotFound();
            }

            pet.Name = petDto.Name;
            pet.Age = petDto.Age;
            pet.Category = petDto.Category;
            pet.Status = petDto.Status;
            pet.ImageUrl = petDto.ImageUrl;

            _context.Entry(pet).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok("Pet updated successfully");
        }

        // Delete Pet Api only for back-end
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pet = _context.Pets.Find(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            _context.SaveChanges();
            return Ok("Pet deleted successfully");
        }
    }
}
