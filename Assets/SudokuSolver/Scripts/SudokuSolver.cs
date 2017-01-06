using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 数独解算器
/// </summary>
namespace SudokuSolver
{
    public class SudokuSolver : MonoBehaviour
    {
        public GameObject SudokuPanel;
        public Button StartButton;

        private int[,] Sudoku;

        void Awake()
        {

            Object cellPrefab = Resources.Load("Prefabs/cell") as Object;

            if (cellPrefab != null)
            {
                for(int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        GameObject cell = Instantiate(cellPrefab) as GameObject;
                        cell.transform.SetParent(SudokuPanel.transform, false);
                    }
                }
            }
        }

        void Start()
        {
            //StartButton.onClick.AddListener(Show);
        }

        public void Show()
        {
            
        }

    }
}