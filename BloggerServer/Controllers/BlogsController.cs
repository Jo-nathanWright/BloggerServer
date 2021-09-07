using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloggerServer.Models;
using BloggerServer.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggerServer.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
public class BlogsController : ControllerBase
    {
    private readonly BlogsService _bs;
    public BlogsController(BlogsService service)
        {
            _bs = service;
        }

        [HttpGet]
        public  ActionResult<List<Blog>> Get()
        {
            try
            {
        List<Blog> blogs = _bs.Get();
        return Ok(blogs);
      }
            catch (Exception err)
            {
            return BadRequest(err.Message);
        }
        } 

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Blog>> Create([FromBody] Blog newBlog)
        {
            try
            {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newBlog.CreatorId = userInfo.Id;
        Blog created = _bs.Create(newBlog);
        return Ok(created);
      }
            catch (Exception err)
            {
            return BadRequest(err.Message);
        }
        }
  }
}