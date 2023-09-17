using Lab5.Swf.Interfaces;

namespace Lab5.Swf.Tags
{
	public readonly struct RemoveObject : ITag
	{
		public ushort CharacterID { get; init; }
		public ushort Depth { get; init; }

		public RemoveObject(ushort characterID, ushort depth) => (CharacterID, Depth) = (characterID, depth);
	}
}