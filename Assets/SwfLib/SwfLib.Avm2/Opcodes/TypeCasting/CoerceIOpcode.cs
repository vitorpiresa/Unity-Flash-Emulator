namespace SwfLib.Avm2.Opcodes.TypeCasting {
    /// <summary>
    /// Coerce a value to a signed int. Deprecated
    /// </summary>
    public class CoerceIOpcode : BaseAvm2Opcode {

        public override TResult AcceptVisitor<TArg, TResult>(IAvm2OpcodeVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

    }
}
