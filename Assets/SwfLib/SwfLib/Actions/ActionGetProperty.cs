namespace SwfLib.Actions {
    /// <summary>
    /// Represents GetProperty action.
    /// </summary>
    public class ActionGetProperty : ActionBase {

        /// <summary>
        /// Gets code of action.
        /// </summary>
        public override ActionCode ActionCode {
            get { return ActionCode.GetProperty; }
        }

        public override TResult AcceptVisitor<TArg, TResult>(IActionVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

    }
}
