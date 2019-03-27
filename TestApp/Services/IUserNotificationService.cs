using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestApp.Services
{
    public interface IUserNotificationService
    {
        Task ShowMessageAsync(string message, string title = null);
        Task ShowErrorMessageAsync(string errorMessage, string title = null);
        Task<bool> AskQuestion(string question, string title = null);
        Task<bool> ShowOptions(string question, string firstOption, string secondOption, string title = null);
        Task<object> ShowOptions(string question, List<Option> options, string title = null);
    }
}