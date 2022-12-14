namespace SwfLib.Avm2.Opcodes.Arithmetics {
    /// <summary>
    /// Add two values. 
    /// </summary>
    public class AddOpcode : BaseAvm2Opcode {

        public override TResult AcceptVisitor<TArg, TResult>(IAvm2OpcodeVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

    }
}
