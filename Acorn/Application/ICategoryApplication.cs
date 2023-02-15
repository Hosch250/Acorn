using Acorn.ApiContracts;

namespace Acorn.Application;

public interface ICategoryApplication
{
    Task<List<Category>> GetAll();
    Task<Category?> Get(Guid id);
    Task<Category> Create(CreateCategory category);
    Task<Category?> Edit(Category category);
    Task Delete(Guid id);
}
