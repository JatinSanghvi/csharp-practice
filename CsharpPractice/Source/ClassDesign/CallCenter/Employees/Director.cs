using System.Threading.Tasks;

namespace CsharpPractice.Source.ClassDesign.CallCenter
{
    internal sealed class Director : Employee
    {
        public Director(string name) : base(name)
        {
        }

        protected override async Task<bool> TakeCallInternalAsync(Call call)
        {
            await Task.Delay(Random.Next(100)).ConfigureAwait(false);
            return true;
        }
    }
}
