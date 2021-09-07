using System.Collections.Generic;
using BloggerServer.Models;
using BloggerServer.Repositories;


namespace BloggerServer.Services
{
  public class BlogsService
  {
    private readonly BlogsRepository _repo;

    public BlogsService(BlogsRepository repo)
    {
      _repo = repo;
    }

    internal Blog Create(Blog newBlog)
    {
      return _repo.Create(newBlog);
    }

    internal List<Blog> Get()
    {
      List<Blog> blogs = _repo.GetAll();

      return blogs.FindAll(b => b.Published == true);
    }
  }
}