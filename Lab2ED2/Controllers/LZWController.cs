﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using System.Threading;
using Lab2ED2.Models;
using System.Text.Json;

namespace Lab2ED2.Controllers
{
    [Route("api/")]
    [ApiController]

    public class LZWController : ControllerBase
    {
        [HttpPost("lzw/compress/{name}")]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file, [FromRoute] string name)
        {
            try
            {
                Compression.DirectoryCreation();
                var filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Temp\\" + file.FileName);
                if (file != null)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else { return StatusCode(500); }
                string FinalFile = Compression.CompressFileLZW(filePath, file.FileName, name);
                FileStream Sender = new FileStream(FinalFile, FileMode.OpenOrCreate);
                return File(Sender, "text/plain", name + ".lzw");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("lzw/decompress")]

        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file)
        {
            try
            {
                Compression.DirectoryCreation();
                var Extension = file.FileName.Split('.');
                //Esto valida si no es .lzw
                if (Extension[Extension.Length - 1] != "lzw")
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
                var OriginalName = Compression.DecompressFileLZW(filePath);
                FileStream Sender = new FileStream(OriginalName[1], FileMode.OpenOrCreate);
                return File(Sender, "text/plain", OriginalName[0]);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpGet("compressions")]

        public ActionResult GetCompressionsJSON()
        {
            Compression.DirectoryCreation();
            var Registries = Compression.GetAllCompressions();
            if (Registries != null)
            {
                return Created("", Registries);
            }
            return StatusCode(500);
        }
    }
}
