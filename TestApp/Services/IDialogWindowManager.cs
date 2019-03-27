using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public interface IDialogWindowManager
    {
        Task<bool> ShowDialogAsync(object rootModel, object context = null, IDictionary<string, object> settings = null);
    }
}