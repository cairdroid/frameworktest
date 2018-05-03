using System.Collections.Generic;
using StrategyPattern.Test;

namespace StrategyPattern.ActionsLayer
{
    class Context : TestStepsRunner
    {
        #region Actions     
        private static readonly Dictionary<EGlobalActions, IKeywordAction> _actions = 
            new Dictionary<EGlobalActions, IKeywordAction>();
        #endregion

        static Context()
        {
            _actions.Add(EGlobalActions.Click, new ClickKeywordAction());
            _actions.Add(EGlobalActions.Change, new ChangeKeywordAction());
            _actions.Add(EGlobalActions.Check, new CheckKeywordAction());
            _actions.Add(EGlobalActions.Go, new GoKeywordAction());
            _actions.Add(EGlobalActions.Mouse, new MouseKeywordAction());
            _actions.Add(EGlobalActions.Set, new SetKeywordAction());
            _actions.Add(EGlobalActions.Verify, new VerifyKeywordAction());
            _actions.Add(EGlobalActions.Wait, new WaitKeywordAction());
        }

        public static void DoAction(EGlobalActions action)
        {
            _actions[action].DoAction();
        }

    }
}
