namespace SwfLib.Avm2.Opcodes.Arithmetics {
    /// <summary>
    /// Add two integer values. 
    /// </summary>
    public class AddIOpcode : BaseAvm2Opcode {

        public override TResult AcceptVisitor<TArg, TResult>(IAvm2OpcodeVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

    }
}
