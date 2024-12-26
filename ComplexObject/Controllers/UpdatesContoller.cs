using ComplexObject.Data;
using Microsoft.AspNetCore.Mvc;

namespace ComplexObject.Controllers
{
    [Route("api/updates")]
    [ApiController]
    public class UpdatesController : ControllerBase
    {
        private static readonly Dictionary<Guid, Update> Updates = new();

        // Метод для обработки POST-запросов с массивом объектов Update и файла
        [HttpPost("complex")]
        public async Task<IActionResult> PostComplex([FromForm] ComplexUpdate complexUpdate)
        {
            if (complexUpdate?.Updates == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            // Обработка массива Update
            foreach (var update in complexUpdate.Updates)
            {
                update.Status = System.Net.WebUtility.HtmlEncode(update.Status);

                var id = Guid.NewGuid();
                Updates[id] = update;
            }

            // Обработка загруженного файла
            if (complexUpdate.File != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, complexUpdate.File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await complexUpdate.File.CopyToAsync(stream);
                }
            }

            return Ok("Updates and file processed successfully");
        }

        // Метод для обработки GET-запроса: api/updates/status/{id}
        [HttpGet("status/{id}")]
        public IActionResult Status(Guid id)
        {
            if (Updates.TryGetValue(id, out var update))
            {
                return Ok(update);
            }
            return NotFound("Update not found");
        }
    }

}
