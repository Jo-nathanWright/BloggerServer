using System;
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

    internal Blog Get(int id, bool checkPublished = true)
    {
      Blog blog = _repo.GetById(id);
      if(blog == null || (checkPublished && blog.Published == false)){
        throw new Exception("Invalid Id");
      }
      return blog;
    }

    internal Blog Update(Blog editedBlog)
    {
      Blog original = Get(editedBlog.Id, false);
      if(original.CreatorId != editedBlog.CreatorId)
      {
        throw new Exception("Invalid Access");
      }
      original.Body = editedBlog.Body.Length > 0 ? editedBlog.Body : original.Body;
      original.Title = editedBlog.Title.Length > 0 ? editedBlog.Title : original.Title;
      original.ImgUrl = editedBlog.ImgUrl != null && editedBlog.ImgUrl.Length > 0 ? editedBlog.ImgUrl : original.ImgUrl;
      original.Published = editedBlog.Published != null ? editedBlog.Published : original.Published;
      return _repo.Update(original);
    }
  }
}