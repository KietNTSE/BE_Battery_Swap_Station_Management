using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FileController(IFileService fileService): ControllerBase
{
    [HttpPost("upload/avatar")]
    public async Task<ActionResult<ResponseObject<string>>> AvatarUpload([FromQuery] string fileName)
    {
        var result = await fileService.UploadAvatarAsync(fileName);
        return Ok(new ResponseObject<string>
        {
            Content = result.ToString(),
            Message = "Login successful",
            Code = "200",
            Success = true
        });
    }
}