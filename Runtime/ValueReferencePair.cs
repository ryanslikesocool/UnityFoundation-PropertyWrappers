using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Foundation {
	[Serializable]
	public struct ValueReferencePair<Value, Reference> : IImmutablePropertyWrapper<Value> where Reference : IImmutablePropertyWrapper<Value> {
		[SerializeField] private Value _value;
		[SerializeField] private Reference _reference;

		public readonly Value wrappedValue
			=> _reference == null ? _value : _reference.wrappedValue;

		// MARK: - Lifecycle

		public ValueReferencePair(in Value value, in Reference reference) {
			this._value = value;
			this._reference = reference;
		}

		public ValueReferencePair(in Value value) : this(value, default) { }
		public ValueReferencePair(in Reference reference) : this(default, reference) { }


		// MARK: - Operators

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Value(in ValueReferencePair<Value, Reference> pair)
			=> pair.wrappedValue;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator ValueReferencePair<Value, Reference>(in Value value)
			=> new ValueReferencePair<Value, Reference>(value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator ValueReferencePair<Value, Reference>(in Reference reference)
			=> new ValueReferencePair<Value, Reference>(reference);
	}
}