using System;

namespace VitaliiPianykh.FileWall.Shared
{
    public struct RuleAction
    {
        public int Action { get; set; }

        #region Public Methods

        private RuleAction(int action)
                : this()
        {
            this.Action = action;
        }


        public bool ToBoolean()
        {
            return ToBoolean(this);
        }

        #endregion


        #region Public Operators

        public static bool operator ==(RuleAction a, RuleAction b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(RuleAction a, RuleAction b)
        {
            return !(a == b);
        }

        #endregion


        #region Object Overrides

        public override bool Equals(object obj)
        {
            if((obj is RuleAction) == false)
                return false;

            RuleAction ruleAction = (RuleAction) obj;

            return Action == ruleAction.Action ? true : false;
        }


        public bool Equals(RuleAction obj)
        {
            return obj.Action == Action;
        }


        public override int GetHashCode()
        {
            return Action;
        }


        public override string ToString()
        {
            if (this == Block)
                return "RuleAction.Block";
            if (this == Allow)
                return "RuleAction.Allow";
            if (this == Ask)
                return "RuleAction.Ask";

            throw new ArgumentException("This RuleAction cannot be converted to System.Sring.");
        }

        #endregion


        #region Public Static Properties

        public static RuleAction Block { get { return new RuleAction(0); } }
        public static RuleAction Allow { get { return new RuleAction(1); } }
        public static RuleAction Ask   { get { return new RuleAction(2); } }

        #endregion


        #region Public Static Methods

        public static RuleAction FromBoolean(bool allow)
        {
            return allow ? Allow : Block;
        }


        public static bool ToBoolean(RuleAction ruleAction)
        {
            if(ruleAction == Block)
                return false;
            if(ruleAction == Allow)
                return true;

            throw new ArgumentException("This RuleAction cannot be converted to System.Boolean.");
        }

        #endregion
    }
}
