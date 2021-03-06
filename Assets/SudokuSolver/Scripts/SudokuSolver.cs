﻿using UnityEngine;
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
        public List<SudokuObject> sudokuList = new List<SudokuObject>();

        private List<SudokuObject> constantSudokuList = new List<SudokuObject>();

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
                        sudokuList.Add(new SudokuObject(id, j, i, 0, sudoku));
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
            for (int i = 0; i < sudokuList.Count; i++)
            {
                if (sudokuList[i].value > 0 && sudokuList[i].value <= 9)
                {
                    sudokuList[i].constant = true;
                    constantSudokuList.Add(sudokuList[i]);
                }
            }

            bool flag =true;

            foreach(SudokuObject sudoku in constantSudokuList)
            {
                if (!ValidateSudoku(sudoku))
                {
                    Result.text = "输入有误";
                    flag = false;
                    constantSudokuList.Clear();
                    break;
                }
            }

            Debug.Log(flag);
            if (flag)
            {
                StartCoroutine(CalculateCoroutine());
            }
        }

        /// <summary>
        /// 判断数独
        /// </summary>
        /// <param name="index">Index.</param>
        /// <param name="Sudoku">Sudoku.</param>
        public bool ValidateSudoku(SudokuObject Sudoku)
        {
            foreach (SudokuObject sudoku in sudokuList)
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
            for (int i = 0; i < sudokuList.Count; i++)
            {
                if (!sudokuList[i].constant)
                {
                    return sudokuList[i].id;
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
                if (!sudokuList[i].constant)
                {
                    if (sudokuList[i].value != 9)
                    {
                        return sudokuList[i].id;
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

            while (index < 81 && ( !result ))
            {
                if (sudokuList[index].constant)
                {
                    index++;
                }
                else
                {
                    sudokuList[index].ChangeValue();

                    if (ValidateSudoku(sudokuList[index]))
                    {
                        index++;
                        continue;
                    }

                    if (sudokuList[index].value >= 9)
                    {
                        if (index == GetFirstIndex())
                        {
                            result = true;
                            Debug.Log("Error");
                        }

                        sudokuList[index].ResetValue();
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