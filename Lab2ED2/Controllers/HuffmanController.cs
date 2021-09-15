using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using System.Threading;
using System.Text.Json;
using Lab2ED2.Models;

namespace Lab2ED2.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HuffmanController : ControllerBase
    {
        [HttpPost("compress/{name}")]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file, [FromRoute] string name)
        {
            try
            {
                var filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Temp\\" + file.FileName);
                if (file != null)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else { return StatusCode(500); }
                Compression.CompressFile(filePath, file.FileName, name);
                FileStream Sender = new FileStream(Directory.GetCurrentDirectory() + "\\Compressed\\" + name + ".huff", FileMode.OpenOrCreate);
                return File(Sender, "text/plain", name + ".huff");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("decompress")]

        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file)
        {
            try
            {
                var Extension = file.FileName.Split('.');
                //Esto valida si no es .huff
                if (Extension[Extension.Length - 1] != "huff")
                {
                    return StatusCode(500);
                }
                var filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Temp\\" + file.FileName);
                if (file != null)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                string OriginalName = Compression.DecompressFile(filePath);
                FileStream Sender = new FileStream(Directory.GetCurrentDirectory() + "\\Decompressed\\" + OriginalName, FileMode.OpenOrCreate);
                return File(Sender, "text/plain", OriginalName);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpGet("compressions")]

        public ActionResult GetCompressionsJSON()
        {
            var Registries = Compression.GetAllCompressions();
            if (Registries != null)
            {
                return Created("", Registries);
            }
            return StatusCode(500);
        }

    }
}
