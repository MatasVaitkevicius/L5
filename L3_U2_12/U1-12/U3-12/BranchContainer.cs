using System.Linq;

namespace U3_12
{
    class BranchContainer
    {
        private const int MaxBranches = 50;
        private Branch[] Branches;
        public int Count { get; private set; }

        public BranchContainer()
        {
            Branches = new Branch[MaxBranches];
            Count = 0;
        }

        public void AddBranch(Branch branch)
        {
            Branches[Count++] = branch;
        }

        public void AddBranch(Branch branch, int index)
        {
            Branches[index] = branch;
        }

        public Branch GetBranch(int index)
        {
            return Branches[index];
        }

        public bool Contains(Branch branch)
        {
            return Branches.Contains(branch);
        }
    }
}
