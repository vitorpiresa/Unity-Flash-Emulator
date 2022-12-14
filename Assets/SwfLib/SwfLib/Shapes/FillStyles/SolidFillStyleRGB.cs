using SwfLib.Data;

namespace SwfLib.Shapes.FillStyles {
    public class SolidFillStyleRGB : FillStyleRGB {

        public SwfRGB Color { get; set; }

        /// <summary>
        /// Gets type of fill style.
        /// </summary>
        public override FillStyleType Type {
            get { return FillStyleType.SolidColor; }
        }

        public override TResult AcceptVisitor<TArg, TResult>(IFillStyleRGBVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }
    }
}
