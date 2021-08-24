#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using StypeMachine;

namespace Helvest.Framework
{

	public class GameManager : TypeMachineController<GameModeBase>
	{

		#region Variables

		[Header("Configs")]
		[SerializeField]
		private bool _dontDestroyOnLoad = true;

		#endregion

		#region Init

		protected override void Awake()
		{
#if UNITY_EDITOR
			SL.useDebugLog = _useDebugForSL;
#endif

			if (_dontDestroyOnLoad)
			{
				DontDestroyOnLoad(gameObject);
			}

			base.Awake();

			var states = GetComponentsInChildren<GameModeBase>();

			foreach (var state in states)
			{
				AddState(state, state.EnterState, state.ExitState, state.UpdateState);
			}
		}

		protected override void OnEnable()
		{
			if (!SL.AddOrDestroy(this))
			{
				return;
			}

			base.OnEnable();
		}

		protected override void ToStartState()
		{
			if (!hasStarted)
			{
				return;
			}

			if (_startState == null)
			{
				Debug.LogError("startState is null, GameManger can't start", this);

#if UNITY_EDITOR
				EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
				return;
			}

			SetState(_startState);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			SL.Remove(this);
		}

		#endregion

		#region Update

		protected virtual void Update()
		{
			typeMachine.UpdateState();
		}

		#endregion

		#region Debug

		[Header("Debug")]
		[SerializeField]
		protected bool _useDebugForSL = false;

		#endregion

	}

}
