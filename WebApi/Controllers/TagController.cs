using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services.TagsServices;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class TagController(ITagService tagService) : ControllerBase
{
}
