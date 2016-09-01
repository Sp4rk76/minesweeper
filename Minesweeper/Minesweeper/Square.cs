using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minesweeper
{
    public enum CaseState
    {
        Hidden,
        Discovered,
        Exploded
    };

    public enum CaseType
    {
        Normal,
        Mine
    };

    class Square
    {
        private int result;
        public CaseState caseState { get; set; }
        public Random random { get; set; }
        public CaseType caseType { get; set; }
        public int Value { get; set; }
        public bool flag { get; set; }

        public Square()
        {
            caseState = CaseState.Hidden;
            //Génération du type de case (RANDOM) // @TODO : à améliorer
            random = new Random();

            InitializeCaseType( );
            
            InitializeValues( ); // giving values to Cases, excepted "Mine"s

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

        private void InitializeCaseType( ) {
            result = new Random( ).Next( 10 );
            if(result == 0) { // 10 %
                caseType = CaseType.Mine;
            } else {
                caseType = CaseType.Normal;
            }
            Thread.Sleep( 20 );
        }

    }
}
