using System;

namespace Minesweeper
{
    class Square
    {
        private Random result;
        public CaseState caseState { get; set; }
        public CaseType caseType { get; set; }
        public int Value { get; set; }
        public bool flag { get; set; }

        public Square()
        {
            caseState = CaseState.Hidden;
            // generate random Square types
            InitializeCaseType();

            InitializeValues(); // giving Square values

            flag = false;
        }

        private void InitializeValues()
        {
            if (caseType == CaseType.Mine) // Mine
            {
                Value = -1;
            }
            else
            {
                Value = 0;
            }
        }

        private void InitializeCaseType()
        {
            result = new Random(Guid.NewGuid().GetHashCode());
            if (result.Next(10) == 0)
            { // 10 %
                caseType = CaseType.Mine;
            }
            else
            {
                caseType = CaseType.Normal;
            }
        }

    }
}
