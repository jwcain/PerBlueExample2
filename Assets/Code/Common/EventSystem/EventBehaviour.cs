using UnityEngine;
using System.Collections.Generic;

namespace Bestagon.Events {

				/// <summary>
				/// Calls System.Action when Unity MonoBehaviour Events occur
				/// </summary>
				public class EventBehaviour : MonoBehaviour {

								/// <summary>
								/// The supported Unity MonoBehaviour Events
								/// </summary>
								public enum ActionType { Destroy, Enable, Disable, Update, MouseEnter, MouseDown, MouseOver, MouseUp, MouseUpAsButton, MouseExit, MouseDrag, Start, Reset, Awake, LateUpdate, FixedUpdate }

								/// <summary>
								/// Internal data links for the actiontype to actions pair
								/// </summary>
								readonly Dictionary<ActionType, List<System.Action>> actionSet = new Dictionary<ActionType, List<System.Action>>();

								/// <summary>
								/// Invokes all actions in the set related to the provided action type
								/// </summary>
								/// <param name="type"></param>
								private void Invoke(ActionType type) {
												if (actionSet[type] != null) {
																foreach (var action in actionSet[type]) {
																				action?.Invoke();
																}
												}
								}

								/// <summary>
								/// Creates a new game object in the scene with an EventBehaviour attached to it
								/// </summary>
								/// <param name="name">The name of the generated game object</param>
								/// <param name="actions">The set of actions to connect to Unity Behaviour Events</param>
								/// <returns>The created EventBehaviour</returns>
								public static EventBehaviour CreateBehaviour(string name = "GeneratedComponent", params (ActionType type, System.Action action)[] actions) {
												//Create new Object
												GameObject gameObject = new GameObject(name);
												//Add this script to it
												return AddComponent(gameObject, actions);
								}

								/// <summary>
								/// Addes an EventBehaviour to the target gameobject
								/// </summary>
								/// <param name="gameObject">The Game Object to attatch to</param>
								/// <param name="actions">The set of actions to connect to Unity Behaviour Events</</param>
								/// <returns>The created EventBehaviour</returns>
								public static EventBehaviour AddComponent(GameObject gameObject, params (ActionType type, System.Action action)[] actions) {
												//Add the behaviour to the target
												EventBehaviour target = gameObject.AddComponent<EventBehaviour>();
												//Add all the actions
												foreach (var pair in actions) {
																//Create a new list if one does not exist
																if (target.actionSet.ContainsKey(pair.type) == false)
																				target.actionSet.Add(pair.type, new List<System.Action>());
																//Add it to the list
																target.actionSet[pair.type].Add(pair.action);
												}

												return target;
								}


								#region UnityHooks
								private void OnDestroy() => Invoke(ActionType.Destroy);
								private void OnEnable() => Invoke(ActionType.Enable);
								private void OnDisable() => Invoke(ActionType.Disable);
								private void Update() => Invoke(ActionType.Update);
								private void OnMouseEnter() => Invoke(ActionType.MouseEnter);
								private void OnMouseDown() => Invoke(ActionType.MouseDown);
								private void OnMouseOver() => Invoke(ActionType.MouseOver);
								private void OnMouseUp() => Invoke(ActionType.MouseUp);
								private void OnMouseUpAsButton() => Invoke(ActionType.MouseUpAsButton);
								private void OnMouseExit() => Invoke(ActionType.MouseExit);
								private void OnMouseDrag() => Invoke(ActionType.MouseDrag);
								private void Start() => Invoke(ActionType.Start);
								private void Reset() => Invoke(ActionType.Reset);
								private void Awake() => Invoke(ActionType.Awake);
								private void LateUpdate() => Invoke(ActionType.LateUpdate);
								private void FixedUpdate() => Invoke(ActionType.FixedUpdate);
								#endregion

				}
}