using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsharpPractice.Source.ClassDesign.CallCenter
{
    public sealed class CallCenter
    {
        private readonly ConcurrentBag<Operator> operators = new ConcurrentBag<Operator>();
        private readonly ConcurrentBag<Supervisor> supervisors = new ConcurrentBag<Supervisor>();
        private readonly ConcurrentBag<Director> directors = new ConcurrentBag<Director>();

        private readonly ConcurrentQueue<Call> calls = new ConcurrentQueue<Call>();

        public CallCenter()
        {
            this.ProcessQueueAsync().Start();
        }

        public void AddEmployee(Rank rank, string employeeName)
        {
            switch (rank)
            {
                case Rank.Operator:
                    this.operators.Add(new Operator(employeeName));
                    break;
                case Rank.Supervisor:
                    this.supervisors.Add(new Supervisor(employeeName));
                    break;
                case Rank.Director:
                    this.directors.Add(new Director(employeeName));
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void AddCall(string customerName)
        {
            var customer = new Customer(customerName);
            this.calls.Enqueue(new Call(customer));
        }

        private async Task ProcessQueueAsync()
        {
            while (true)
            {
                while (this.calls.TryDequeue(out Call call))
                {
                    this.TakeCallAsync(call).Start();
                }

                await Task.Delay(100).ConfigureAwait(false);
            }
        }

        private async Task TakeCallAsync(Call call)
        {
            while (true)
            {
                bool isCompleted =
                   !this.TryGetEmployee(call.Rank, out Employee employee) || await employee.TakeCallAsync(call).ConfigureAwait(false);

                if (isCompleted)
                {
                    break;
                }

                call.Escalate();
            }
        }

        private bool TryGetEmployee(Rank rank, out Employee employee)
        {
            switch (rank)
            {
                case Rank.Operator:
                    return this.TryGetEmployee(this.operators, out employee);
                case Rank.Supervisor:
                    return this.TryGetEmployee(this.supervisors, out employee);
                case Rank.Director:
                    return this.TryGetEmployee(this.directors, out employee);
                default:
                    throw new InvalidOperationException();
            }
        }

        private bool TryGetEmployee<T>(ConcurrentBag<T> employees, out Employee employee) where T : Employee
        {
            employee = employees.FirstOrDefault(e => !e.IsBusy);
            return employee != null;
        }
    }
}
