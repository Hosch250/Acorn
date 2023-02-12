using Acorn.ApiContracts;

namespace Acorn.Application;

public interface ITagApplication
{
    Task<List<Tag>> GetAll(int skip = 0);
    Task<Tag?> Get(Guid id);
    Task<Tag?> Edit(EditTag post);
    Task Delete(Guid id);
}
