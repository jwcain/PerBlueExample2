using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bestagon.Events {
				public class Monitor<T> {

								/// <summary>
								/// Internal event system for this T of monitor
								/// </summary>
								private static Internal.EventSystem<Monitor<T>, T, T> eventSystem = new Internal.EventSystem<Monitor<T>, T, T>();

								/// <summary>
								/// Internal tracked value
								/// </summary>
								private T internalValue;

								/// <summary>
								/// Tracked value. Invokes actions on value changing
								/// </summary>
								public T Value {
												get {
																return internalValue;
												}
												set {
																T oldVal = internalValue;
																internalValue = value;
																//Only notify for changes
																if (oldVal.Equals(internalValue) == false)
																				eventSystem.Handle(this, oldVal, internalValue);
												}
								}

								/// <summary>
								/// Destroy this key's actions on destroy
								/// </summary>
								~Monitor() {
												eventSystem.DumpKey(this);
								}

								/// <summary>
								/// Registers an action to occur when the value changes. Args: (old, new)
								/// </summary>
								/// <param name="onChangeAction"></param>
								public void Register(System.Func<T, T, object> onChangeAction) => eventSystem.Register(this, onChangeAction);

								/// <summary>
								/// Deregisters an action to occur when the value changes. Args: (old, new)
								/// </summary>
								/// <param name="onChangeAction"></param>
								public void Deregister(System.Func<T, T, object> onChangeAction) => eventSystem.Register(this, onChangeAction);
				}
}