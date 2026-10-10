using System;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.UIElements;

namespace Tanks.Complete
{
    public class TankManager
    {
        [HideInInspector] public Color m_PlayerColor;
        public Transform m_SpawnPoint;
        [HideInInspector] public int m_PlayerNumber;
        [HideInInspector] public string m_ColoredPlayerText;
        [HideInInspector] public GameObject m_Instance;
        [HideInInspector] public int m_Wins;
        [HideInInspector] public bool m_ComputerControlled;

        public int ControlIndex { get; set; } = 1;

        private TankMovement m_Movement;
        private TankMovement m_Shooting;
        private GameObject m_CanvasGameObject;
        private TankAI m_AI;
        private InputUser m_InputUser;

        public void Setup(GameManager manager)
        {
            m_Movement = m_Instance.GetComponent<TankMovement>();
            m_Shooting = m_Instance.GetComponent<TankShooting>();
            m_AI = m_Instance.GetComponent<TankAI>();
            m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

            var inputUser = m_Instance.GetComponent<TankInputUser>();
            inputUser.SetNewInputUser(m_InputUser);

            m_Movement.m_IsComputerControlled = m_ComputerControlled;
            m_Shooting.m_IsComputerControlled = m_ComputerControlled;

            m_Movement.m_PlayerNumber = m_PlayerNumber;
            m_Movement.ControlIndex = ControlIndex;

            if (m_ComputerControlled)
            {
                m_AI = m_Instance.AddComponent<TankAI>();
                m_AI.Setup(manager);
            }

            m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";
            MeshRenderer[] renderers = m_Instance.GetComponentInChildren<MeshRenderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                var renderer = renderers[i];
                for (int j = 0; j < renderers.material.Length; j++)
                {
                    if (renderer.materials[j].name.Contains("TankColor"))
                    {
                        renderer.materials[j].color = m_PlayerColor;
                    }
                }
            }
        }


        public void DisableControl()
        {
            m_Movement.enabled = false;
            m_Shooting.enabled = false;
            if (m_ComputerControlled)
            {
                m_AI.enabled = false;
            }

            m_CanvasGameObject.SetActive(false);
        }

        public void EnableControl()
        {
            m_Instance.transform.position = m_SpawnPoint.position;
            m_Instance.transform.rotation = m_SpawnPoint.rotation;

            m_Instance.SetActive(false);
            m_Instance.SetActive(true);
        }
    }

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(TankManager))]
    public class TankManagerDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property){
            var itemSlot = new PropertyField(propert.FindPropertyRelative(nameof(TankManager.m_SpawnPoint)));
            return itemSlot;
        }

    }
#endif
}
