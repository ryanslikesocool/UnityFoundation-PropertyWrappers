using System;
using UnityEngine;

namespace Foundation {
    /// <summary>
    /// A property wrapper type that can read and write a value, as well as observe changes to the underlying value.
    /// </summary>
    /// <remarks>
    /// <c>PropertyObserver</c> should be used as the single source of truth for a given value.
    /// </remarks>
    [Serializable]
    public sealed class PropertyObserver<Value> : IChangeObserver<Value> where Value : struct {
        [SerializeField, Tooltip("The underlying property value.  Changes made to this value will not invoke callbacks.")] private Value _value;

        public Value wrappedValue {
            get => _value;
            set {
                Value oldValue = _value;

                willSet?.Invoke(_value, value);
                _value = value;
                didSet?.Invoke(oldValue, _value);
            }
        }

        public event IChangeObserver<Value>.ImmutableWillSetCallback willSet;
        public event IChangeObserver<Value>.DidSetCallback didSet;

        /// <summary>
        /// Create a new property observer with no callbacks.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        public PropertyObserver(in Value initialValue) {
            this._value = initialValue;
        }

        public static implicit operator Value(PropertyObserver<Value> v) => v.wrappedValue;
    }
}