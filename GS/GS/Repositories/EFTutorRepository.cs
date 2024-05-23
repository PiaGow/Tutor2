using GS.Models;
using Microsoft.EntityFrameworkCore;

namespace GS.Repositories
{
	public class EFTutorRepository : ITutorRepository
	{
		private readonly DACSDbContext _context;
		public EFTutorRepository(DACSDbContext context)
		{
			_context = context;
		}


		public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
		{
			// return await _context.Products.ToListAsync();
			return await _context.ApplicationUsers
			.Include(p => p.IdentityRole) // Include thông tin về category
			.ToListAsync();
		}
		public async Task<ApplicationUser> GetByIdAsync(string id)
		{
			// return await _context.Products.FindAsync(id);
			// lấy thông tin kèm theo category
			return await _context.ApplicationUsers.Include(p => p.IdentityRole).FirstOrDefaultAsync(p => p.UserRole == id);
		}
		public async Task AddAsync(ApplicationUser product)
		{
			_context.ApplicationUsers.Add(product);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(ApplicationUser product)
		{
			_context.ApplicationUsers.Update(product);
			await _context.SaveChangesAsync();
		}


		public async Task<int> CountTutorsBySubjectAsync(int categoryId)
		{
			// Sử dụng LINQ để đếm số lượng sách có categoryId (Subject) tương ứng
			return await _context.ApplicationUsers.CountAsync(b => b.Idst == categoryId);
		}
		public async Task<int> CountTutorsByClassAsync(int categoryId)
		{
			// Sử dụng LINQ để đếm số lượng sách có categoryId (Class) tương ứng
			return await _context.ApplicationUsers.CountAsync(b => b.Idcs == categoryId);
		}
	}
}
