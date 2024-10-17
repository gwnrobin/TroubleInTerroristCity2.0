using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EntityDeath : PlayerComponent
	{
		[BHeader("Stuff To Disable On Death", true)]

		[SerializeField]
		private GameObject[] m_ObjectsToDisable;

		[SerializeField]
		private Behaviour[] m_BehavioursToDisable;

		[SerializeField]
		private Collider[] m_CollidersToDisable;

		//[BHeader("Player Head Hitbox")]

		//[SerializeField]
		//private Rigidbody m_Head = null;

		[BHeader("Respawn", true)]

		[SerializeField]
		private bool m_Respawn = true;

		[SerializeField]
		[EnableIf("m_Respawn", true)]
		private float m_RespawnDuration = 5f;

		//[SerializeField]
		//[EnableIf("m_Respawn", true)]
		//private bool m_RestartSceneOnRespawn = false;

		//private Transform m_CameraStartParent;
		//private Quaternion m_CameraStartRotation;
		//private Vector3 m_CameraStartPosition;

		//private Vector3 m_HeadStartPosition;
		//private Quaternion m_HeadStartRotation;

		[SerializeField] private GenericVitals _vitals;


        public override void OnEntityStart()
        {
			Entity.Health.AddChangeListener(OnChanged_Health);
			Entity.Dead.SetStartTryer(TryDead);
			
			Entity.Death.AddListener(() => StartCoroutine(C_OnDeath()));
			//m_Head.isKinematic = true;
			//m_Head.gameObject.SetActive(false);

			// Camera set up
			//m_CameraStartRotation = Player.Camera.transform.localRotation;
			//m_CameraStartPosition = Player.Camera.transform.localPosition;
			//m_CameraStartParent = Player.Camera.transform.parent;

			// Player head set up
			//m_HeadStartPosition = m_Head.transform.localPosition;
			//m_HeadStartRotation = m_Head.transform.localRotation;
        }

        public void DebugKill()
        {
	        OnChanged_Health(0);
        }

		private void OnChanged_Health(float health)
		{
			if (health <= 0f)
				Entity.Death.Send();
		}

		private IEnumerator C_OnDeath()
		{
			/*Player.DropItem.Try(Player.EquippedItem.Get());
*/
			yield return null;
			
			foreach (var obj in m_ObjectsToDisable)
			{
				if (obj != null)
					obj.SetActive(false);
				else
					Debug.LogWarning("Check out PlayerDeath for missing references, an object reference was found null!", this);
			}

			foreach (var behaviour in m_BehavioursToDisable)
			{
				if (behaviour != null)
					behaviour.enabled = false;
				else
					Debug.LogWarning("Check out PlayerDeath for missing references, a behaviour reference was found null!", this);
			}

			foreach (var collider in m_CollidersToDisable)
			{
				if (collider != null)
					collider.enabled = false;
				else
					Debug.LogWarning("Check out PlayerDeath for missing references, a collider reference was found null!", this);
			}
		
		
			/*
			//Player.Camera.transform.parent = m_Head.transform;

			//m_Head.gameObject.SetActive(true);
			//m_Head.isKinematic = false;
			//m_Head.AddForce(Vector3.ClampMagnitude(Player.Velocity.Get() * 0.5f, 10f), ForceMode.Force);
			//m_Head.AddRelativeTorque(new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) * 35, ForceMode.Force);

			

			if (m_Respawn)
			{
				yield return new WaitForSeconds(m_RespawnDuration);

				Respawn();
			}
			*/
		}

		private bool TryDead() => true;

		public void Respawn()
		{
			print("respawn");

			Entity.Respawn.Send();

			foreach (var obj in m_ObjectsToDisable)
				obj.SetActive(true);

			foreach (var behaviour in m_BehavioursToDisable)
				behaviour.enabled = true;

			foreach (var collider in m_CollidersToDisable)
				collider.enabled = true;

			_vitals.SetOriginalMaxHealth();
		}
	}
