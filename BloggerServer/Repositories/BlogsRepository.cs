using System.Collections.Generic;
using System.Data;
using System.Linq;
using BloggerServer.Models;
using Dapper;

namespace BloggerServer.Repositories
{
    public class BlogsRepository
    {
    private readonly IDbConnection _db;
    public BlogsRepository(IDbConnection db)
        {
        _db = db;
        }


            internal List<Blog> GetAll()
        {
      string sql = @"
            SELECT
              a.*,
              b.*
            FROM blogs b
            JOIN accounts a ON a.id = b.creatorId;
            ";
      return _db.Query<Profile, Blog, Blog>(sql, (profile, blog) => {
        blog.Creator = profile;
        return blog;
      }, splitOn: "id").ToList<Blog>();
    }
        internal Blog GetById(int id)
        {
      string sql = @"
            SELECT
              a.*,
              b.*
            FROM blogs b
            JOIN accounts a ON a.id = b.creatorId
            WHERE b.id = @id;
            ";
      return _db.Query<Profile, Blog, Blog>(sql, (profile, blog) => {
        blog.Creator = profile;
        return blog;
      }, new {id}, splitOn: "id").FirstOrDefault();
    }
        internal Blog Create(Blog newBlog)
        {
      string sql = @"
            INSERT INTO blogs
            (title, body, imgUrl, published, creatorId)
            VALUES
            (@Title, @Body. @ImgUrl, @Published, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
      newBlog.Id = _db.ExecuteScalar<int>(sql, newBlog);
      return newBlog;
    }
    }
}