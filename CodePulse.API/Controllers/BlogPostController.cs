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
        private readonly ICategoryRepository categoryRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogpostRequestDTO request)
        {
            //convert from DTo to domain model
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
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            await blogPostRepository.CreateAsync(blogPost);

            //convert from domain model to DTO
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
                    Categories = blogPost.Categories.Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        UrlHandle = c.UrlHandle
                    }).ToList()
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
                        Categories = blogPost.Categories.Select(c => new CategoryDTO
                        {
                            Id = c.Id,
                            Name = c.Name,
                            UrlHandle = c.UrlHandle
                        }).ToList()

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
                    Categories = blogPost.Categories.Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        UrlHandle = c.UrlHandle
                    }).ToList()

                };

                return Ok(response);

            }

            [HttpPut]
            [Route("{id:Guid}")]

            public async Task<IActionResult> EditBlogPost([FromRoute]Guid id,[FromBody] UpdateBlogPostDTO request)
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
                Categories = new List<Category>()
            };
            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }



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
                    IsVisible = blogPost.IsVisible,
                    Categories = blogPost.Categories.Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        UrlHandle = c.UrlHandle
                    }).ToList()
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

