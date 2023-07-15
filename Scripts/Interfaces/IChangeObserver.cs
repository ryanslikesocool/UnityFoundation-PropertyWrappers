using UnityEngine;

namespace Foundation {
    public interface IChangeObserver<Value> : IMutablePropertyWrapper<Value> where Value : struct {
        /// <summary>
        /// The `willSet` callback is called immediately before the wrapped value is set.
        /// </summary>
        /// <param name="oldValue">The old (current) wrapped value.</param>
        /// <param name="newValue">The new mutable wrapped value.</param>
        delegate void WillSetCallback(Value oldValue, ref Value newValue);

        /// <summary>
        /// The `willSet` callback is called immediately before the wrapped value is set.
        /// </summary>
        /// <param name="oldValue">The old (current) wrapped value.</param>
        /// <param name="newValue">The new immutable wrapped value.</param>
        delegate void ImmutableWillSetCallback(Value oldValue, Value newValue);

        /// <summary>
        /// The `didSet` callback is called immediately after the wrapped value is set.
        /// </summary>
        /// <param name="oldValue">The old wrapped value.</param>
        /// <param name="newValue">The new (current) wrapped value.</param>
        delegate void DidSetCallback(Value oldValue, Value newValue);
    }
}