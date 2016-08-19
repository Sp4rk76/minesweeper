using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public enum CaseState
    {
        Hidden,
        Discovered
    };

    public enum CaseType
    {
        Normal,
        Mine
    };

    class Case
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CaseState caseState { get; set; }
        public Random random { get; set; }
        public CaseType caseType { get; set; }
        public int Value { get; set; }
        public bool flag { get; set; }

        public Case()
        {
            X = 0;
            Y = 0;
            caseState = CaseState.Hidden;
            //Génération du type de case (RANDOM) // @TODO : à améliorer
            random = new Random();
            caseType = (CaseType)Enum.GetValues(typeof(CaseType)).GetValue(random.Next(Enum.GetValues(typeof(CaseType)).Length));

            InitializeValues(); // giving values to Cases, excepted "Mine"s

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
    }
}
