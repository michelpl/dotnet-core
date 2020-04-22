using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using customercrud.Models;
using customercrud.Data;
using MongoDB.Driver;

namespace customercrud.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("v1/posts")]
    public class PostController : Controller
    {
        private Context context;

        public PostController()
        {
            context = new Context();
        }

        // GET: api/Post
        [HttpGet]
        [Route("")]
        public IEnumerable<Post> Get()
        {
            var posts = context.Posts.Find(_ => true).ToList();
            return posts;
        }

        [HttpPost]
        [Route("")]
        public void Post([FromBody]Post post)
        {
            context.Posts.InsertOne(post);

        }
    }
}