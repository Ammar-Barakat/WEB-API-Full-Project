using BlogAPI.DTOs.CommentDTOs;
using BlogAPI.DTOs.PostDTOs;
using BlogAPI.DTOs.TagDTOs;
using BlogAPI.Models;
using BlogAPI.Repository.PostRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BlogAPI.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public PostController(IPostRepository postRepo, UserManager<ApplicationUser> userManager)
        {
            _postRepo = postRepo;
            _userManager = userManager;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var posts = await _postRepo.GetAllAsync();

            var postDTOs = posts.Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreateDate = p.CreateDate,
                UpdateDate = p.UpdateDate,
                UserId = p.User.Id,
                UserName = p.User.UserName,
                Email = p.User.Email,
                Comments = p.Comments.Select(c => new CommentPostDTO
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreateDate = c.CreateDate,
                    UserId = c.User.Id,
                    UserName = c.User.UserName,
                    Email = c.User.Email
                }).ToList(),
                Tags = p.PostTags.Select(pt => new TagDTO
                {
                    Id = pt.TagId,
                    Name = pt.Tag.Name
                }).ToList(),
            }).ToList();

            return Ok(postDTOs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var post = await _postRepo.GetByIdAsync(id);

            if (post == null)
                return NotFound($"No Post With {id} Was Found!");

            var postDTO = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                UserId = post.User.Id,
                UserName = post.User.UserName,
                Email = post.User.Email,
                Comments = post.Comments.Select(c => new CommentPostDTO
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreateDate = c.CreateDate,
                    UserId = c.User.Id,
                    UserName = c.User.UserName,
                    Email = c.User.Email
                }).ToList(),
                Tags = post.PostTags.Select(pt => new TagDTO
                {
                    Id = pt.TagId,
                    Name = pt.Tag.Name
                }).ToList()
            };

            return Ok(postDTO);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.Claims.FirstOrDefault().Value;

            if (userId == null)
                return Unauthorized("Invalid User");

            var user = await _userManager.FindByIdAsync(userId);

            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                UserId = userId,
            };

            await _postRepo.CreateAsync(post);

            var result = await _postRepo.AssignPostTag(post, dto.tagIds);

            if (result == null)
                return NotFound($"No Tag Was Found!");

            var postDTO = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                UserId = post.User.Id,
                UserName = post.User.UserName,
                Email = post.User.Email,
                Comments = post.Comments?.Select(c => new CommentPostDTO
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreateDate = c.CreateDate,
                    UserId = c.User.Id,
                    UserName = c.User.UserName,
                    Email = c.User.Email
                }).ToList(),
                Tags = post.PostTags.Select(pt => new TagDTO
                {
                    Id = pt.TagId,
                    Name = pt.Tag.Name
                }).ToList()
            };

            return Ok(postDTO);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postRepo.GetByIdAsync(id);

            if (post == null)
                return NotFound($"No Post With {id} Was Found!");

            var userId = User.Claims.FirstOrDefault().Value;

            if (userId == null)
                return Unauthorized("Invalid User");

            if (post.User.Id == userId)
            {
                post.Title = dto.Title;
                post.Content = dto.Content;
                post.UpdateDate = DateTime.Now;

                _postRepo.Update(post);

                var postDTO = new PostDTO
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    CreateDate = post.CreateDate,
                    UpdateDate = post.UpdateDate,
                    UserId = post.User.Id,
                    UserName = post.User.UserName,
                    Email = post.User.Email,
                    Comments = post.Comments.Select(c => new CommentPostDTO
                    {
                        Id = c.Id,
                        Content = c.Content,
                        CreateDate = c.CreateDate,
                        UserId = c.User.Id,
                        UserName = c.User.UserName,
                        Email = c.User.Email
                    }).ToList(),
                    Tags = post.PostTags.Select(pt => new TagDTO
                    {
                        Id = pt.TagId,
                        Name = pt.Tag.Name
                    }).ToList()
                };

                return Ok(postDTO);
            }
            else
            {
                return Unauthorized("Invalid Permession");
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postRepo.GetByIdAsync(id);

            if (post == null)
                return NotFound($"No Post With {id} Was Found!");

            var userId = User.Claims.FirstOrDefault().Value;

            if (userId == null)
                return Unauthorized("Invalid User");

            if (post.User.Id == userId)
            {
                _postRepo.Delete(post);

                var postDTO = new PostDTO
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    CreateDate = post.CreateDate,
                    UpdateDate = post.UpdateDate,
                    UserId = post.User.Id,
                    UserName = post.User.UserName,
                    Email = post.User.Email,
                    Comments = post.Comments.Select(c => new CommentPostDTO
                    {
                        Id = c.Id,
                        Content = c.Content,
                        CreateDate = c.CreateDate,
                        UserId = c.User.Id,
                        UserName = c.User.UserName,
                        Email = c.User.Email
                    }).ToList(),
                    Tags = post.PostTags.Select(pt => new TagDTO
                    {
                        Id = pt.TagId,
                        Name = pt.Tag.Name
                    }).ToList()
                };

                return Ok(postDTO);
            }
            else
            {
                return Unauthorized("Invalid Permession");
            }
        }
    }
}
