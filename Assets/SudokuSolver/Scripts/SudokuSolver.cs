using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace SudokuSolver
{
    public class SudokuSolver : MonoBehaviour
    {
        public Transform SudokuPanel;
        public Button StartButton;
        public Text Result;
        public List<SudokuObject> SudokuList = new List<SudokuObject>();

        void Awake()
        {
            Object sudokuPrefab = Resources.Load("Prefabs/Sudoku") as Object;

            if (sudokuPrefab != null)
            {
                int id = 0;

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        GameObject sudoku = Instantiate(sudokuPrefab) as GameObject;
                        sudoku.transform.SetParent(SudokuPanel, false);
                        SudokuList.Add(new SudokuObject(id, j, i, 0, sudoku));
                        id++;
                    }
                }
            }
            StartButton.onClick.AddListener(CalculateSudoku);
        }

        /// <summary>
        /// 开始解算
        /// </summary>
        public void CalculateSudoku()
        {
            StartCoroutine(CalculateCoroutine());
        }

        /// <summary>
        /// 判断数独
        /// </summary>
        /// <param name="index">Index.</param>
        /// <param name="Sudoku">Sudoku.</param>
        public bool ValidateSudoku(SudokuObject Sudoku)
        {
            foreach (SudokuObject sudoku in SudokuList)
            {
                //判断行列
                if (( Sudoku.row == sudoku.row ) || ( Sudoku.col == sudoku.col ))
                {
                    if (( Sudoku.id != sudoku.id ) && ( Sudoku.value == sudoku.value ))
                    {
                        return false;
                    }
                }
                //判断九宫格
                if (Sudoku.area == sudoku.area)
                {
                    if (( Sudoku.id != sudoku.id ) && ( Sudoku.value == sudoku.value ))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 无解判断
        /// </summary>
        /// <returns>The index.</returns>
        public int GetFirstIndex()
        {
            for (int i = 0; i < SudokuList.Count; i++)
            {
                if (!SudokuList[i].constant)
                {
                    return SudokuList[i].id;
                }
            }
            return 0;
        }

        /// <summary>
        /// 回溯
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
		public int Backdate(int n)
        {
            for (int i = n - 1; i >= 0; i--)
            {
                if (!SudokuList[i].constant)
                {
                    if (SudokuList[i].value != 9)
                    {
                        return SudokuList[i].id;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// 协程，计算
        /// </summary>
        /// <returns></returns>
        private IEnumerator CalculateCoroutine()
        {
            float timeStart = Time.realtimeSinceStartup;
            int index = 0;
            bool result = false;

            for (int i = 0; i < SudokuList.Count; i++)
            {
                if (SudokuList[i].value > 0 && SudokuList[i].value <= 9)
                {
                    SudokuList[i].constant = true;
                }
            }

            while (index < 81 && ( !result ))
            {
                if (SudokuList[index].constant)
                {
                    index++;
                }
                else
                {
                    SudokuList[index].ChangeValue();

                    if (ValidateSudoku(SudokuList[index]))
                    {
                        index++;
                        continue;
                    }

                    if (SudokuList[index].value >= 9)
                    {
                        if (index == GetFirstIndex())
                        {
                            result = true;
                            Debug.Log("Error");
                        }

                        SudokuList[index].ResetValue();
                        index = Backdate(index);
                    }
                }

                yield return 0;
            }

            if (!result)
            {
                Result.text = ( Time.realtimeSinceStartup - timeStart ).ToString();
            }
        }
    }
}