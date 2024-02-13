using System;
using UnityEngine;

namespace Foundation {
	/// <summary>
	/// A type that represents either a wrapped value or the absence of a value.
	/// </summary>
	[Serializable]
	public sealed class OptionalReference<Value> : IMutablePropertyWrapper<Value>, IEquatable<OptionalReference<Value>>, IEquatable<Value> where Value : class {
		[SerializeField] private Value _value;
		[SerializeField] private bool _hasValue;

		public Value wrappedValue {
			get {
				if (!hasValue) {
					throw new InvalidOperationException("Serializable nullable object must have a value.");
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
		public bool hasValue => _hasValue;

		public OptionalReference(bool hasValue, Value value) {
			this._value = value;
			this._hasValue = hasValue;
		}

		public OptionalReference(Value value) {
			this._hasValue = value != null;
			if (value != null) {
				this._value = value;
			} else {
				this._value = default;
			}
		}

		/// <summary>
		/// Attempt to retrieve the underlying value.
		/// </summary>
		/// <param name="value">The underlying value, if it exists; <see langword="default"/> otherwise.</param>
		/// <returns><see langword="true"/> if the underlying value exists; <see langword="false"/> otherwise.</returns>
		public bool TryGetValue(out Value value) {
			value = this._value;
			return hasValue;
		}

		public static implicit operator OptionalReference<Value>(Value value)
			=> new OptionalReference<Value>(value);

		public static implicit operator Value(OptionalReference<Value> value)
			=> value.hasValue ? value.wrappedValue : null;

		public override int GetHashCode() => (_hasValue, _value).GetHashCode();

		public override bool Equals(object obj) => obj switch {
			OptionalReference<Value> other => this.Equals(other),
			Value other => this.Equals(other),
			null => !_hasValue,
			_ => false,
		};

		public bool Equals(OptionalReference<Value> other) {
			if (_hasValue == other._hasValue) {
				return !_hasValue || _value.GetHashCode() == other._value.GetHashCode();
			} else {
				return false;
			}
		}

		public bool Equals(Value other) {
			if (!hasValue) {
				return false;
			}
			return _value.GetHashCode() == other.GetHashCode();
		}
	}
}