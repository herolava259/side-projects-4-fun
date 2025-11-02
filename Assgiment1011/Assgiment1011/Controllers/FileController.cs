using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Assgiment1011.Utilities.Constants;
using Assgiment1011.Utilities.OutMemoryUtilities;
using System;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
//using Assgiment1011.Utilities.Constants;
namespace Assgiment1011.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        [Route("UploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                var result = await FileUtility.WriteFile(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("DownloadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DownloadFile([FromQuery]string? fileName = "")
        {
            fileName = fileName?.Trim();
            if(String.IsNullOrEmpty(fileName))
            {
                return BadRequest("File Name is null or empty");
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),PathConstant.RelativeUploadFilePath ,fileName);

            if(!FileUtility.IsExist(filePath))
            {
                return BadRequest($"File with name is {fileName} cannot find or is not existed.");
            }

            try
            {
                var provider = new FileExtensionContentTypeProvider();

                if(!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, contentType, Path.GetFileName(filePath));
            }
            catch(Exception ex)
            {
                return BadRequest($"Error: Cannot uploading file {fileName}.\n-Exception Message: {ex.ToString()}");
            }
        }
        
    }

    
}
