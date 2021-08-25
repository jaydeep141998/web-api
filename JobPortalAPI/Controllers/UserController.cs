using JobPortalAPI.Models;
using JobPortalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JobPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private CosmosSqlService sqlService;
        private StorageBlobService blobService;

        public UserController(CosmosSqlService cosmosSqlService, StorageBlobService storageBlobService)
        {
            sqlService = cosmosSqlService;
            blobService = storageBlobService;
        }

        //POST /api/user
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddProfile(UserProfile profile)
        {
            TryValidateModel(profile);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                profile.Id = Guid.NewGuid().ToString();
                await sqlService.InsertDocument(profile);
                return Created("", profile);
            }catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        //GET /api/user/id
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProfileById(string id)
        {
            var profile = await sqlService.GetProfileById(id);
            if (profile != null)
                return Ok(profile);
            else
                return NotFound();
        }


        [HttpPost("upload")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult UploadProfile(IFormFile profile)
        {
            try
            {
                if (profile.Length > 0)
                {
                    var fs = profile.OpenReadStream();
                    var fileName = ContentDispositionHeaderValue.Parse(profile.ContentDisposition).FileName.Trim('"');
                    var uri= blobService.UploadFile(fs, fileName);
                    return Ok(new
                    {
                        FileUri= uri
                    });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
