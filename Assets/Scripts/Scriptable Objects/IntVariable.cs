using UnityEngine;

namespace Scriptable_Objects
{
	[CreateAssetMenu]
	public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
	{
		public int InitialValue;

		//[NonSerialized]
		public int RunTimeValue;

		public void OnAfterDeserialize()
		{
			RunTimeValue = InitialValue;
		}

		public void OnBeforeSerialize() { }
	}
}
