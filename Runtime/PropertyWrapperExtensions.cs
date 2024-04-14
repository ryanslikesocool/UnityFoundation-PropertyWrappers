namespace Foundation {
	public static partial class PropertyWrapperExtensions {
		public static void Mutate<Value>(this IMutablePropertyWrapper<Value> propertyWrapper, IMutablePropertyWrapper<Value>.MutateAction body)
			=> propertyWrapper.Mutate(body);
	}
}