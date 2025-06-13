using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repository.Implementation;
using CodePulse.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost( CreateBlogpostRequestDTO request)
        {
            var blogPost = new BlogPost
            {
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Author = request.Author,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                PublishedDate = request.PublishedDate,
                Content = request.Content,
                IsVisible = request.IsVisible,
            };

            await blogPostRepository.CreateAsync(blogPost);


            var response = new BlogPostDTO
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Author = blogPost.Author,
                ShortDescription = blogPost.ShortDescription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishedDate = blogPost.PublishedDate,
                Content = blogPost.Content,
                IsVisible = blogPost.IsVisible,
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPost()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();

            var response = new List<BlogPostDTO>();
            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDTO
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                    Author = blogPost.Author,
                    ShortDescription = blogPost.ShortDescription,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    PublishedDate = blogPost.PublishedDate,
                    Content = blogPost.Content,
                    IsVisible = blogPost.IsVisible,

                });
                
            }
       
            return Ok(response);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await blogPostRepository.GetById(id);

            if (blogPost == null)
            {
                return NotFound();
            }
            var response = new BlogPostDTO
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Author = blogPost.Author,
                ShortDescription = blogPost.ShortDescription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishedDate = blogPost.PublishedDate,
                Content = blogPost.Content,
                IsVisible = blogPost.IsVisible,
            };

            return Ok(response);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        
            public async Task<IActionResult> EditBlogPost(Guid id, UpdateBlogPostDTO request)
            {
                    var blogPost = new BlogPost
                    {
                        Id = id,
                        Title = request.Title,
                        UrlHandle = request.UrlHandle,
                        Author = request.Author,
                        ShortDescription = request.ShortDescription,
                        FeaturedImageUrl = request.FeaturedImageUrl,
                        PublishedDate = request.PublishedDate,
                        Content = request.Content,
                        IsVisible = request.IsVisible,
                    };

                    blogPost = await blogPostRepository.UpdateAsync(blogPost);

                    if (blogPost == null)
                    {
                        return NotFound();
                    }

                    var response = new BlogPostDTO
                    {
                        Id = blogPost.Id,
                        Title = blogPost.Title,
                        UrlHandle = blogPost.UrlHandle,
                        Author = blogPost.Author,
                        ShortDescription = blogPost.ShortDescription,
                        FeaturedImageUrl = blogPost.FeaturedImageUrl,
                        PublishedDate = blogPost.PublishedDate,
                        Content = blogPost.Content,
                        IsVisible = blogPost.IsVisible
                    };

                    return Ok(response);

            }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            var blogPost = await blogPostRepository.DeleteAsync(id);


            if (blogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDTO
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Author = blogPost.Author,
                ShortDescription = blogPost.ShortDescription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishedDate = blogPost.PublishedDate,
                Content = blogPost.Content,
                IsVisible = blogPost.IsVisible,
            };

            return Ok(response);


        }

    }
}
