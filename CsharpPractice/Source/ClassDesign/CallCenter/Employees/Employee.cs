using System;
using System.Threading.Tasks;

namespace CsharpPractice.Source.ClassDesign.CallCenter
{
    internal abstract class Employee
    {
        public string Name { get; }

        public bool IsBusy { get; private set; }

        protected static Random Random => new Random();

        public Employee(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Handles customer call.
        /// </summary>
        /// <returns>True if the call was completed; false otherwise</returns>
        public async virtual Task<bool> TakeCallAsync(Call call)
        {
            this.IsBusy = true;
            bool isCompleted = await this.TakeCallInternalAsync(call).ConfigureAwait(false);
            this.IsBusy = false;
            return isCompleted;
        }

        protected abstract Task<bool> TakeCallInternalAsync(Call call);
    }
}
