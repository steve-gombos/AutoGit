using Octokit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGit.Core.Extensions
{
    public static class RepositoryContentExtensions
    {
        public static async Task<bool> CheckIfFileExists(this IRepositoryContentsClient client, long repositoryId, string path)
        {
            try
            {
                var result = await client.GetAllContents(repositoryId, path);
                return true;
            }
            catch (Exception ex)
            {
                // do nothing
            }

            return false;
        }
        
        public static async Task<RepositoryContent> GetAllContentsOrNull(this IRepositoryContentsClient client, long repositoryId, string path)
        {
            try
            {
                var result = await client.GetAllContents(repositoryId, path);
                return result.First();
            }
            catch (Exception ex)
            {
                // do nothing
            }

            return null;
        }
    }
}