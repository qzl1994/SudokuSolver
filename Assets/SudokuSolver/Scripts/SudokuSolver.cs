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

        private int Length = 9;
        private int[,] Sudoku = new int[9, 9];
        private GameObject[,] Cell = new GameObject[9, 9];

        int[] query = new int[9] { 0, 0, 0, 3, 3, 3, 6, 6, 6 };

        void Awake()
        {

            Object cellPrefab = Resources.Load("Prefabs/cell") as Object;
            if (cellPrefab != null)
            {
                for(int i = 0; i < Length; i++)
                {
                    for(int j = 0; j < Length; j++)
                    {
                        GameObject cell = Instantiate(cellPrefab) as GameObject;
                        cell.transform.SetParent(SudokuPanel.transform, false);
                        Cell[i, j] = cell;
                    }
                }
            }
        }

        void Start()
        {
            StartButton.onClick.AddListener(GetSudoku);
        }

        /// <summary>
        /// 填补数独数组，开始解算
        /// </summary>
        public void GetSudoku()
        {
            for(int i = 0; i < Length; i++)
            {
                for(int j = 0; j < Length; j++)
                {
                    Sudoku[i,j] = int.Parse(Cell[i, j].GetComponent<InputField>().text);
                }
            }

            SudokuSolve(0);
        }

        public void SudokuSolve(int n)
        {
            if(n == 81)
            {
                Debug.Log("Over");
                return;
            }

            int i = n / 9, j = n % 9;

            if(Sudoku[i,j] != 0)
            {
                SudokuSolve(n + 1);

                return;
            }

            for(int k = 0; k < 9; k++)
            {
                Sudoku[i, j]++;
                Cell[i, j].GetComponent<InputField>().text = Sudoku[i, j].ToString();

                if (IsVaild(i, j))
                {
                    SudokuSolve(n + 1);
                }
            }

            Sudoku[i, j] = 0;
            return;
        }

        public bool IsVaild(int i, int j)
        {
            int num = Sudoku[i, j];

            int k, u;

            for (k = 0; k < Length; k++)
            {
                if (( k != i && Sudoku[k, j] == num ) || (k!=j && Sudoku[i,k] == num ))
                {
                    return false;
                }
            }

            //每个九宫格是否重复
            for (k = query[i]; k < query[i] + 3; k++)
            {
                for (u = query[j]; u < query[j] + 3; u++)
                {
                    if (( k != i || u != j ) && Sudoku[k, u] == num)
                    {
                        return false;
                    }
                        
                }
            }
            return true;
        }
    }
}