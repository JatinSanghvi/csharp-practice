using System.Threading.Tasks;

namespace CsharpPractice.Source.ClassDesign.CallCenter
{
    internal sealed class Operator : Employee
    {
        public Operator(string name) : base(name)
        {
        }

        protected override async Task<bool> TakeCallInternalAsync(Call call)
        {
            if (Random.Next(2) == 0)
            {
                return false;
            }

            await Task.Delay(Random.Next(100)).ConfigureAwait(false);
            return true;
        }
    }
}
