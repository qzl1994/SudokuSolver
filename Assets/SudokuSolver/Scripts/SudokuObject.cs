using UnityEngine;
using UnityEngine.UI;
using System;

namespace SudokuSolver
{
    [Serializable]
    public class SudokuObject
    {
        public int id;
        public int row;
        public int col;
        public int area;
        public int value;
        public bool constant;
        public InputField InputField;

        public SudokuObject(int id, int row, int col, int value, GameObject sudoku)
        {
            this.id = id;
            this.row = row;
            this.col = col;
            this.area = ( row / 3 ) + ( col / 3 ) * 3;
            this.value = value;
            this.constant = false;
            this.InputField = sudoku.GetComponent<InputField>();
            this.InputField.onValueChanged.AddListener(OnValueChanged);

            //Debug.Log(id + " " + row + " " + " " + col + " " + area);

            if (area % 2 == 0)
            {
                sudoku.GetComponent<Image>().color = Color.gray;
            }
        }

        public void OnValueChanged(string value)
        {
            if (this.constant)
            {
                this.InputField.text = this.value.ToString();
            }
            else
            {
                int result = 0;
                if (( int.TryParse(value, out result) && ( result <= 9 ) ) && ( result > 0 ))
                {
                    this.value = result;
                }
            }
        }

        public void ChangeValue()
        {
            this.value = Mathf.Clamp(this.value + 1, 0, 9);
            this.InputField.text = this.value.ToString();
        }

        public void ResetValue()
        {
            this.value = 0;
            this.InputField.text = 0.ToString();
        }
    }
}