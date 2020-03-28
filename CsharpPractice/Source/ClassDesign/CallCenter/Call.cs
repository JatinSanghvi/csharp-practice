namespace CsharpPractice.Source.ClassDesign.CallCenter
{
    internal sealed class Call
    {
        public Customer Customer { get; }

        public Rank Rank { get; private set; }

        public Call(Customer customer)
        {
            this.Customer = customer;
            this.Rank = Rank.Operator;
        }

        public void Escalate()
        {
            this.Rank = this.Rank == Rank.Operator ? Rank.Supervisor
                : this.Rank == Rank.Supervisor ? Rank.Director
                : throw new System.NotImplementedException();
        }
    }
}
