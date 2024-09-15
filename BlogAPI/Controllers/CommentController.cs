using BlogAPI.DTOs.CommentDTOs;
using BlogAPI.Models;
using BlogAPI.Repository.CommentRepo;
using BlogAPI.Repository.PostRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IPostRepository _postRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentController(ICommentRepository commentRepo, IPostRepository postRepo, UserManager<ApplicationUser> userManager)
        {
            _commentRepo = commentRepo;
            _postRepo = postRepo;
            _userManager = userManager;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var comments = await _commentRepo.GetAllAsync();

            var commentDTOs = comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Content = c.Content,
                CreateDate = c.CreateDate,
                PostId = c.Post.Id,
                PostTitle = c.Post.Title,
                PostContent = c.Post.Content,
                PostCreateDate = c.Post.CreateDate,
                PostUserName = c.Post.User.UserName,
                UserId = c.User.Id,
                UserName = c.User.UserName,
                Email = c.User.Email
            }).ToList();

            return Ok(commentDTOs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
                return NotFound($"No Comment With {id} Was Found!");

            var commentDTO = new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                CreateDate = comment.CreateDate,
                PostId = comment.Post.Id,
                PostTitle = comment.Post.Title,
                PostContent = comment.Post.Content,
                PostCreateDate = comment.Post.CreateDate,
                PostUserName = comment.Post.User.UserName,
                UserId = comment.User.Id,
                UserName = comment.User.UserName,
                Email = comment.User.Email
            };

            return Ok(commentDTO);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCommentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postRepo.GetByIdAsync(dto.PostId);

            if (post == null)
                return NotFound($"No Post With {dto.PostId} Was Found!");

            var userId = User.Claims.FirstOrDefault().Value;

            if (userId == null)
                return Unauthorized("Invalid User");

            var user = await _userManager.FindByIdAsync(userId);

            var comment = new Comment
            {
                Content = dto.Content,
                CreateDate = DateTime.Now,
                PostId = post.Id,
                UserId = userId
            };

            await _commentRepo.CreateAsync(comment);

            var commentDTO = new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                CreateDate = comment.CreateDate,
                PostId = comment.Post.Id,
                PostTitle = comment.Post.Title,
                PostContent = comment.Post.Content,
                PostCreateDate = comment.Post.CreateDate,
                PostUserName = comment.Post.User.UserName,
                UserId = comment.User.Id,
                UserName = comment.User.UserName,
                Email = comment.User.Email
            };

            return Ok(commentDTO);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
                return NotFound($"No Comment With {id} Was Found!");

            var userId = User.Claims.FirstOrDefault().Value;

            if (userId == null)
                return Unauthorized("Invalid User");

            if (comment.User.Id == userId)
            {
                comment.Content = dto.Content;

                _commentRepo.Update(comment);

                var commentDTO = new CommentDTO
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreateDate = comment.CreateDate,
                    PostId = comment.Post.Id,
                    PostTitle = comment.Post.Title,
                    PostContent = comment.Post.Content,
                    PostCreateDate = comment.Post.CreateDate,
                    PostUserName = comment.Post.User.UserName,
                    UserId = comment.User.Id,
                    UserName = comment.User.UserName,
                    Email = comment.User.Email
                };

                return Ok(commentDTO);
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

            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
                return NotFound($"No Comment With {id} Was Found!");

            var userId = User.Claims.FirstOrDefault().Value;

            if (userId == null)
                return Unauthorized("Invalid User");

            if (comment.User.Id == userId)
            {
                _commentRepo.Delete(comment);

                var commentDTO = new CommentDTO
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreateDate = comment.CreateDate,
                    PostId = comment.Post.Id,
                    PostTitle = comment.Post.Title,
                    PostContent = comment.Post.Content,
                    PostCreateDate = comment.Post.CreateDate,
                    PostUserName = comment.Post.User.UserName,
                    UserId = comment.User.Id,
                    UserName = comment.User.UserName,
                    Email = comment.User.Email
                };

                return Ok(commentDTO);
            }
            else
            {
                return Unauthorized("Invalid Permession");
            }
        }
    }
}
