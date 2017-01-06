using UnityEngine;
using UnityEngine.UI;

namespace SudokuSolver
{
    public class SudokuObject : MonoBehaviour
    {
        public int row;
        public int col;
        public int value;
        public InputField InputField;

        public SudokuObject(int row,int col,int value,GameObject obj)
        {
            GameObject Sudoku = Instantiate(obj) as GameObject;
            this.InputField = Sudoku.GetComponent<InputField>();
            this.row = row;
            this.col = col;
            this.value = value;
        }


    }
}

