using Chaos.Business;

namespace Chaos.Data;

public class DbResult
{
    public TaskResults Result { get; set; }
    public string Message { get; set; }

    public DbResult(TaskResults result, string message) => (Result, Message) = (result, message);

    public void Deconstruct(out TaskResults result, out string message) => (result, message) = (Result, Message);
}
