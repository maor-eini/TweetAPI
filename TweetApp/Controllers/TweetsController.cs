using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TweetApp.Data.Repositories.Interfaces;
using TweetApp.Domain.Entities;
using TweetApp.Services.Services.Interfaces;

namespace TweetApp.Controllers
{
    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        private ITweetService _tweetService;

        public TweetsController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }
        // GET api/tweet
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tweets = await _tweetService.GetRecentlyAddedTweets();

            if (tweets == null)
            {
                return NotFound();
            }

            return Ok(tweets);
        }

        // GET api/tweet/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (String.IsNullOrEmpty(id)) return BadRequest();

            var tweet = await _tweetService.Get(id);

            if (tweet == null)
            {
                return NotFound();
            }

            return Ok(tweet);     
        }

        // POST api/tweet
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Tweet tweet)
        {
            if (tweet == null)
            {
                BadRequest();
            }

            await _tweetService.Add(tweet);

            return CreatedAtRoute("Get", new { id = tweet.Id }, tweet);
        }

        // PUT api/tweet/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/tweet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (String.IsNullOrEmpty(id)) return BadRequest();

            await _tweetService.Remove(id);

            return Ok();
        }

        // GET api/tweet?x={double}&y={double}&radius={double}
        [HttpGet("WithinCenterSphere")]
        public async Task<IActionResult> GetTweetsWithinCenterSphere([FromQuery]double x, [FromQuery]double y, [FromQuery]double radius)
        {
            if (radius <= 0) return BadRequest();

            var tweets = await _tweetService.GetTweetsWithinCenterSphere(x, y, radius);

            if (tweets == null)
            {
                return NotFound();
            }

            return Ok(tweets);  
        }

        // POST api/tweet
        [HttpPost("WithinPolygon")]
        public async Task<IActionResult> GetTweetsWithinPolygon([FromBody]double[,] points)
        {
            if (points == null || points.Length < 3 ) return BadRequest();

            //Validate that the first and last elements are equals
            if (points[0, 0] != points[points.Length-1, 0] && 
                points[0, 1] != points[points.Length-1, 1])
            {
                return BadRequest();
            }

            var tweets = await _tweetService.GetTweetsWithinPolygon(points);

            if (tweets == null)
            {
                return NotFound();
            }

            return Ok(tweets);
        }
    }
}
