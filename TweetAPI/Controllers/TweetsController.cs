using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TweetAPI.Data.Repositories.Interfaces;
using TweetAPI.Domain.Entities;

namespace TweetAPI.Controllers
{
    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        private ITweetRepository _tweetRepository;

        public TweetsController(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }
        // GET api/tweet
        [HttpGet]
        public IEnumerable<Tweet> Get()
        {
           return _tweetRepository.GetAll();
        }

        // GET api/tweet/5
        [HttpGet("{id}")]
        public Tweet Get(string id)
        {
            return _tweetRepository.Get(id);
        }

        // POST api/tweet
        [HttpPost]
        public void Post([FromBody]Tweet tweet)
        {
            _tweetRepository.Add(tweet);
        }

        // PUT api/tweet/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            
        }

        // DELETE api/tweet/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _tweetRepository.Remove(id);
        }
    }
}
