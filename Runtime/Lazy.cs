namespace Foundation {
    /// <summary>
    /// A property wrapper type that lazily creates the wrapped value the first time it's accessed.
    /// </summary>
    public sealed class Lazy<Value> : IMutablePropertyWrapper<Value> {
        public delegate Value Initializer();

        private bool _isInitialized = false;
        private Value _value = default;

        public Value wrappedValue {
            get {
                if (!_isInitialized) {
                    _value = initializer();
                    _isInitialized = true;
                }
                return _value;
            }
            set {
                _value = value;
                if (_value != null) {
                    _isInitialized = true;
                }
            }
        }

        /// <summary>
        /// Has the underlying value been initialized?
        /// </summary>
        /// <remarks>
        /// If the <see cref="wrappedValue"/> has been set to <see langword="null"/> after initialization, <see cref="isInitialized"/> is reset to <see langword="false"/>.
        /// </remarks>
        public bool isInitialized => _isInitialized;

        private readonly Initializer initializer;

        /// <summary>
        /// Create a new lazy property.
        /// </summary>
        /// <param name="initializer">The property's initializer.  This will be invoked the first time the wrapped value is accessed.</param>
        public Lazy(Initializer initializer) {
            if (initializer == null) {
                throw new System.NullReferenceException("initializer");
            }
            this.initializer = initializer;
        }

        public static implicit operator Value(Lazy<Value> v) => v.wrappedValue;
        public static implicit operator Lazy<Value>(Initializer init) => new Lazy<Value>(init);
    }
}