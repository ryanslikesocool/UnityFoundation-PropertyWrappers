using System;
using UnityEngine;

namespace Foundation {
	/// <summary>
	/// A type that represents either a wrapped value or the absence of a value.
	/// </summary>
	[Serializable]
	public struct Optional<Value> : IMutablePropertyWrapper<Value>, IEquatable<Optional<Value>>, IEquatable<Value> where Value : struct {
		[SerializeField] private Value _value;
		[SerializeField] private bool _hasValue;

		public Value wrappedValue {
			get {
				if (!hasValue) {
					throw new System.InvalidOperationException("Serializable nullable object must have a value.");
				}
				return _value;
			}
			set {
				_hasValue = true;
				_value = value;
			}
		}

		/// <summary>
		/// Does the wrapper's underlying value exist?
		/// </summary>
		public readonly bool hasValue => _hasValue;

		public Optional(bool hasValue, Value value) {
			this._value = value;
			this._hasValue = hasValue;
		}

		public Optional(Value? value) {
			this._hasValue = value.HasValue;
			if (value.HasValue) {
				this._value = value.Value;
			} else {
				this._value = default;
			}
		}

		public Optional(Value value) {
			this._value = value;
			this._hasValue = true;
		}

		/// <summary>
		/// Attempt to retrieve the underlying value.
		/// </summary>
		/// <param name="value">The underlying value, if it exists; <see langword="default"/> otherwise.</param>
		/// <returns><see langword="true"/> if the underlying value exists; <see langword="false"/> otherwise.</returns>
		public readonly bool TryGetValue(out Value value) {
			value = this._value;
			return hasValue;
		}

		public readonly Value? system {
			get {
				if (!hasValue) {
					return null;
				} else {
					return _value;
				}
			}
		}

		public static implicit operator Optional<Value>(Value value)
			=> new Optional<Value>(value);

		public static implicit operator Optional<Value>(Value? value)
			=> new Optional<Value>(value);

		public static implicit operator Value?(Optional<Value> value)
			=> value.hasValue ? value.wrappedValue : null;

		public readonly override int GetHashCode() => (_hasValue, _value).GetHashCode();

		public readonly override bool Equals(object obj) {
			switch (obj) {
				case Optional<Value> other:
					return this.Equals(other);
				case Value other:
					return this.Equals(other);
				case null:
					return !_hasValue;
				default:
					return false;
			}
		}

		public readonly bool Equals(Optional<Value> other) {
			if (_hasValue == other._hasValue) {
				return _hasValue ? _value.GetHashCode() == other._value.GetHashCode() : true;
			} else {
				return false;
			}
		}

		public readonly bool Equals(Value other) {
			if (!hasValue) { return false; }
			return _value.GetHashCode() == other.GetHashCode();
		}
	}
}