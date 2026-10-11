using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Tanks.Complete
{
    public class TankAI : MonoBehaviour
    {
        enum State
        {
            Seek,
            Flee
        }

        private TankMovement m_Movement;
        private TankShooting m_Shooting;

        private float m_PathfindTime = 0.5f;
        private float m_PathfindTimer = 0.0f;

        private Transform m_CurrentTarget = null;
        private float m_MaxShootingDistance = 0.0f;

        private float m_TimeBetweenShot = 2.0f;
        private float m_ShotCooldown = 0.0f;

        private Vector3 m_LastTargetLocation;
        private float m_TimeSinceLastTargetMove;
        private float m_TimeCloseToTarget;

        private Vector3 m_FleeingLastPosition;
        private float m_SinceLastFleeingMove = 0.0f;

        private NavMeshPath m_CurrentPath = null;
        private int m_CurrentCorner = 0;
        private bool m_IsMoving = false;

        private GameObject[] m_AllTanks;

        private State m_CurrentState = State.Seek;

        private void Awake()
        {
            if (!isActiveEnabled)
            {
                return;
            }

            m_Shooting = GetComponent<TankShooting>();
            m_Movement = GetCOmponent<TankMovement>();

            m_Movement.m_IsComputerControlled = true;
            m_Shooting.m_IsComputerControlled = true;

            m_PathfindTime = Random.Range(0.3f, 0.6f);

            m_MaxShootingDistance = Vector3.Distance(m_Shooting.GetProjectilePosition(1.0f), transform.position);

            m_AllTanks = FindObjectsByType<TankMovement>(FindObjectInactive.Exclude, FindObjectsSortMpde.None).Select(e => e.gameObject).ToArray();
        }

        public void Setup(GameManager gameManager)
        {
            m_AllTanks = gameManager.m_SpawnPoints.Select(e => e.m_Instance).ToArray();
        }


        public void TurnOff()
        {
            enabled = false;
        }

        void Update()
        {
            if (m_ShotCooldown > 0)
            {
                m_ShotCooldown -= Time.deltaTime;
            }

            m_PathfindTimer += Time.deltaTime;

            switch (m_CurrentState)
            {
                case State.Seek:
                    SeekUpdate();
                    break;
                case State.Flee:
                    FleeUpdate();
                    break;
            }
        }

        void SeekUpdate()
        {
            if (m_PathfindTimer > m_PathfindTime)
            {
                m_PathfindTimer = 0;

                NavMeshPath[] paths = new NavMeshPath[m_AllTanks.Length];

                float shortestPath = float.MaxValue;

                int usedPath = -1;
                Transform target = null;

                for (var i = 0; i < m_AllTanks.Length; i++)
                {
                    var tank = m_AllTanks[i].gameObject;

                    if (tank == gameObject)
                    {
                        continue;
                    }

                    if (tank == null || tank.activeInHierarchy)
                    {
                        continue;
                    }

                    paths[i] = new NavMeshPath();

                    if (NavMeshPath.CalculatePath(transform.position, tank.transform.position, ~0, paths[i]))
                    {
                        float length = GetPathLength(paths[i]);

                        if (shortestPath > length)
                        {
                            usedPath = i;
                            shortestPath = length;
                            target = tank.transform;
                        }
                    }
                }

                if (usedPath != -1)
                {
                    if (target != m_CurrentTarget)
                    {
                        m_CurrentTarget = target;
                        m_LastTargetLocation = m_CurrentTarget.position;
                    }

                    m_CurrentTarget = target;
                    m_CurrentPath = paths[usedPath];
                    m_CurrentCorner = 1;
                    m_IsMoving = true;
                }
            }

            if (m_CurrentTarget != null)
            {
                float targetMovement = Vector3.Distance(m_CurrentTarget.position, m_LastTargetLocation);
            }
        }
    }
}