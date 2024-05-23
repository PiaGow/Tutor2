using GS.Models;

namespace GS.Repositories
{
	public interface ITutorRepository
	{
		Task<IEnumerable<ApplicationUser>> GetAllAsync();
		Task<ApplicationUser> GetByIdAsync(string id);
		Task AddAsync(ApplicationUser product);
		Task UpdateAsync(ApplicationUser product);
		Task<int> CountTutorsBySubjectAsync(int categoryId);
		Task<int> CountTutorsByClassAsync (int categoryId);

	}
}
