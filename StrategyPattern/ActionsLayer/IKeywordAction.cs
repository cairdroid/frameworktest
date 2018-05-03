using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern.ActionsLayer
{
    interface IKeywordAction
    {
        void DoAction();//string actionType, string locator, string data);
    }
}
