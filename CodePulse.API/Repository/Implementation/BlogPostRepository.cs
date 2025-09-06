using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repository.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository( ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();

            return blogPost;

        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            //var existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            var existingBlogPost = await GetById(id);
            if (existingBlogPost == null)
            {
                return null;
            }

            dbContext.BlogPosts.Remove(existingBlogPost);
            await dbContext.SaveChangesAsync();
            return existingBlogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            var blogPosts = await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
            return blogPosts;
        }

        public async Task<BlogPost?> GetById(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);       
            
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlogPost != null)
            {
                //update the existing blog post with the new values
                dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

                //update the categories
                existingBlogPost.Categories = blogPost.Categories;

                await dbContext.SaveChangesAsync();
                return blogPost;
            }
            return null;
        }
    }
}
