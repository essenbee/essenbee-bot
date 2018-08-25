namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class If : IAction
    {
        private readonly bool _condition;
        private readonly IAction _thenClause;
        private readonly IAction _elseClause;

        public If(bool condition, IAction thenClause, IAction elseClause)
        {
            _condition = condition;
            _thenClause = thenClause;
            _elseClause = elseClause;
        }

        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            if (_condition)
            {
                return _thenClause.Do(player, item);
            }

            return _elseClause.Do(player, item);
        }
    }
}
